﻿using EduHome_Asp.net.Data;
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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();

            return View(sliders);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var slider = await GetSliderById(id);
            if (slider is null) return NotFound();
            return View(slider);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderVM sliderVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in sliderVM.Photos)
            {
                if (!photo.CheckFileType("image/")) //utilites yaranmalidi!!
                {
                    ModelState.AddModelError("Photo", "Image type is wrong");
                    return View();
                }

                if (!photo.CheckFileSize(2000))
                {
                    ModelState.AddModelError("Photo", "Image size wrong");
                    return View();
                }
            }

            foreach (var photo in sliderVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string path = Helper.GetFilePath(_env.WebRootPath, "img/slider", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {

                    await photo.CopyToAsync(stream);
                }

                Slider slider = new Slider
                {
                    Image =fileName
                };
                await _context.Sliders.AddAsync(slider);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await GetSliderById(id);
            if (slider == null) return NotFound();
            string path = Helper.GetFilePath(_env.WebRootPath, "img", slider.Image);

            Helper.DeleteFile(path);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           

        }




        private async Task<Slider> GetSliderById(int? id)
        {

            return await _context.Sliders.FindAsync(id);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var slider = await GetSliderById(id);
            if (slider is null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {

            if (id == null) return NotFound();
            if (slider == null) return NotFound();


            var dbSlider = await GetSliderById(id);
            //if (dbSlider == null) return NotFound();

            //if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
           
            if (!slider.Photos.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View(dbSlider);
            }

            if (!slider.Photos.CheckFileSize(2000))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View(dbSlider);
            }

            string path = Helper.GetFilePath(_env.WebRootPath, "img", dbSlider.Image);

            Helper.DeleteFile(path);


            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photos.FileName;

            string newPath = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await slider.Photos.CopyToAsync(stream);
            }

            dbSlider.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }







    }
}



        






         
  








