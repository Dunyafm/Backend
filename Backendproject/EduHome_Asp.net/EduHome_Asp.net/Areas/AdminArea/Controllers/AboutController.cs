using EduHome_Asp.net.Data;
using EduHome_Asp.net.Models;
using EduHome_Asp.net.Utilities.File;
using EduHome_Asp.net.Utilities.Helpers;
using EduHome_Asp.net.ViewModels.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Areas.AdminArea.Controllers
{   
    [Area("AdminArea")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _context.Abouts.AsNoTracking().ToListAsync();

            return View(abouts);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutVM aboutVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in aboutVM.Photos)
            {
                if (!photo.CheckFileType("image/")) //utilites yaranmalidi!!
                {
                    ModelState.AddModelError("Photo", "Image type is wrong");
                    return View();
                }

                if (!photo.CheckFileSize(800))
                {
                    ModelState.AddModelError("Photo", "Image size wrong");
                    return View();
                }
            }

            foreach (var photo in aboutVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string path = Helper.GetFilePath(_env.WebRootPath, "img/slider", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {

                    await photo.CopyToAsync(stream);
                }

                About About = new About
                {
                    Header = fileName
                };
                await _context.Abouts.AddAsync(About);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            About About = await GetAboutById(id);
            if (About == null) return NotFound();
            string path = Helper.GetFilePath(_env.WebRootPath, "img", About.Description);

            Helper.DeleteFile(path);

            _context.Abouts.Remove(About);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
       

        public async Task<IActionResult> Edit(int id)
        {
            var about = await GetAboutById(id);
            if (about is null) return NotFound();
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, About about)
        {
            var dbAbout = await GetAboutById(id);
            if (dbAbout == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!about.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbAbout);
            }

            if (!about.Photos.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbAbout);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "img", dbAbout.Description);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + about.Photos.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await about.Photos.CopyToAsync(stream);
            }

            dbAbout.Description = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var about = await GetAboutById(id);
            if (about is null) return NotFound();
            return View(about);
        }



        private async Task<About> GetAboutById(int id)
        {

            return await _context.Abouts.FindAsync(id);
        }















    }
}
