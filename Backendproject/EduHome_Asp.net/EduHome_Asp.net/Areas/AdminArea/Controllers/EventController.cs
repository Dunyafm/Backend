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
using static System.Net.Mime.MediaTypeNames;
namespace EduHome_Asp.net.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _context.Events.AsNoTracking().ToListAsync();

            return View(events);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var Event = await GetEventById(id);
            if (Event is null) return NotFound();
            return View(Event);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Event Event = await GetEventById(id);
            if (Event == null) return NotFound();
            string path = Helper.GetFilePath(_env.WebRootPath, "img", Event.Image);

            Helper.DeleteFile(path);

            _context.Events.Remove(Event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int? id)
        {
            var levent = await GetEventById(id);
            if (levent is null) return NotFound();
            return View(levent);
        }

        //public IActionResult Create()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(EventVM EventVM)
        //{
        //    if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

        //    foreach (var photo in eventVM.Photos)
        //    {
        //        if (!photo.CheckFileType("image/")) //utilites yaranmalidi!!
        //        {
        //            ModelState.AddModelError("Photo", "Image type is wrong");
        //            return View();
        //        }

        //        if (!photo.CheckFileSize(2000))
        //        {
        //            ModelState.AddModelError("Photo", "Image size wrong");
        //            return View();
        //        }
        //    }

        //    foreach (var photo in eventVM.Photos)
        //    {
        //        string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
        //        string path = Helper.GetFilePath(_env.WebRootPath, "img/slider", fileName);
        //        using (FileStream stream = new FileStream(path, FileMode.Create))
        //        {

        //            await photo.CopyToAsync(stream);
        //        }

        //        Slider slider = new Slider
        //        {
        //            Image = fileName
        //        };
        //        await _context.Sliders.AddAsync(slider);
        //    }
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}




        private async Task<Event> GetEventById(int? id)
        {

            return await _context.Events.FindAsync(id);
        }





    }
}

