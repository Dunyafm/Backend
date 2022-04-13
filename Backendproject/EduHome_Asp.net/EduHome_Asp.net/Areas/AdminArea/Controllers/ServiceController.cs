using EduHome_Asp.net.Data;
using EduHome_Asp.net.Models;
using EduHome_Asp.net.Utilities.File;
using EduHome_Asp.net.Utilities.Helpers;
using EduHome_Asp.net.ViewModels.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]

    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> Services = await _context.Services.AsNoTracking().ToListAsync();

            return View(Services);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var service = await GetServiceById(id);
            if (service is null) return NotFound();
            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(serviceVM servicevm)
        {
            if (ModelState["photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in servicevm.Photos)
            {
                if (!photo.CheckFileType("image/")) //utilites yaranmalidi!!
                {
                    ModelState.AddModelError("photo", "image type is wrong");
                    return View();
                }

                if (!photo.CheckFileSize(800))
                {
                    ModelState.AddModelError("photo", "image size wrong");
                    return View();
                }
            }

            foreach (var photo in servicevm.Photos)
            {
                string filename = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string path = Helper.GetFilePath(_env.WebRootPath, "img", filename);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {

                    await photo.CopyToAsync(stream);
                }

                Service service = new Service
                {
                    Image = filename
                };
                await _context.Services.AddAsync(service);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Service service = await GetServiceById(id);
            if (service == null) return NotFound();
            string path = Helper.GetFilePath(_env.WebRootPath, "img", service.Image);

            Helper.DeleteFile(path);

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }




        private async Task<Service> GetServiceById(int? id)
        {

            return await _context.Services.FindAsync(id);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var service = await GetServiceById(id);
            if (service is null) return NotFound();
            return View(service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Service Service)
        {

            if (id == null) return NotFound();
            if (Service == null) return NotFound();


            var dbService = await GetServiceById(id);
            //if (dbSlider == null) return NotFound();

            //if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!Service.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbService);
            }

            if (!Service.Photos.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbService);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "img", dbService.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + Service.Photos.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await Service.Photos.CopyToAsync(stream);
            }

            dbService.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


}   }


