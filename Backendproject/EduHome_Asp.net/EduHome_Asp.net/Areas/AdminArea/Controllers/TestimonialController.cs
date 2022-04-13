using EduHome_Asp.net.Data;
using EduHome_Asp.net.Models;
using EduHome_Asp.net.Utilities.File;
using EduHome_Asp.net.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TestimonialController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Testimonial> Testimonials = await _context.Testimonials.AsNoTracking().ToListAsync();

            return View(Testimonials);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var Testimonial = await GetTestimonialById(id);
            if (Testimonial is null) return NotFound();
            return View(Testimonial);
        }
        private async Task<Testimonial> GetTestimonialById(int? id)
        {

            return await _context.Testimonials.FindAsync(id);
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var Testimonial = await GetTestimonialById(id);
        //    if (Testimonial is null) return NotFound();
        //    return View(Testimonial);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int? id, Testimonial Testimonial)
        //{

        //    if (id == null) return NotFound();
        //    if (Testimonial == null) return NotFound();


        //    var TestimonialVM = await GetTestimonialById(id);
        //    //if (dbSlider == null) return NotFound();

        //    //if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

        //    if (!TestimonialVM.Info.CheckFileType("image/"))
        //    {
        //        ModelState.AddModelError("Photo", "Image type is wrong");
        //        return View(TestimonialVM);
        //    }

        //    if (!TestimonialVM.Info.CheckFileSize(2000))
        //    {
        //        ModelState.AddModelError("Photo", "Image size is wrong");
        //        return View(TestimonialVM);
        //    }

        //    string path = Helper.GetFilePath(_env.WebRootPath, "img", TestimonialVM.Info;

        //    Helper.DeleteFile(path);


        //    string fileName = Guid.NewGuid().ToString() + "_" + Testimonial.Info.FileName;

        //    string newPath = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

        //    using (FileStream stream = new FileStream(newPath, FileMode.Create))
        //    {
        //        await Testimonial.Info.CopyToAsync(stream);
        //    }

        //    TestimonialVM.Id = fileName;

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}


    }
}
