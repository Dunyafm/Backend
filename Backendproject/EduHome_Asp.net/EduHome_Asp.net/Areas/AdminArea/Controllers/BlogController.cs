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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.AsNoTracking().ToListAsync();

            return View(blogs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await GetBlogById(id);
            if (blog is null) return NotFound();
            return View(blog);
        }

        private async Task<Blog> GetBlogById(int? id)
        {

            return await _context.Blogs.FindAsync(id);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM blogVM)
        {
            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();

            foreach (var photo in blogVM.Photos)
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

            foreach (var photo in blogVM.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string path = Helper.GetFilePath(_env.WebRootPath, "img/slider", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {

                    await photo.CopyToAsync(stream);
                }

                Blog Blog = new Blog
                {
                    Image = fileName
                };
                await _context.Blogs.AddAsync(Blog);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await GetBlogById(id);
            if (blog == null) return NotFound();
            string path = Helper.GetFilePath(_env.WebRootPath, "img", blog.Image);

            Helper.DeleteFile(path);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var blog = await GetBlogById(id);
        //    if (blog is null) return NotFound();
        //    return View(blog);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int? id, Blog blog)
        //{

        //    if (id == null) return NotFound();
        //    if (Blog == null) return NotFound();


        //    var dbBlog = await GetBlogById(id);
        //    //if (dbSlider == null) return NotFound();

        //    //if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

        //    if (!Blog.Photos.CheckFileType("image/"))
        //    {
        //        ModelState.AddModelError("Photo", "Image type is wrong");
        //        return View(dbBlog);
        //    }

        //    if (!Blog.Photos.CheckFileSize(2000))
        //    {
        //        ModelState.AddModelError("Photo", "Image size is wrong");
        //        return View(dbBlog);
        //    }

        //    string path = Helper.GetFilePath(_env.WebRootPath, "img", dbBlog.Image);

        //    Helper.DeleteFile(path);


        //    string fileName = Guid.NewGuid().ToString() + "_" + Blog.Photos.FileName;

        //    string newPath = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

        //    using (FileStream stream = new FileStream(newPath, FileMode.Create))
        //    {
        //        await Blog.Photos.CopyToAsync(stream);
        //    }

        //    dbBlog.Photos = fileName;

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}






    }
}
