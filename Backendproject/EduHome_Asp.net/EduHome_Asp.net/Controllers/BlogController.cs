using EduHome_Asp.net.Data;
using EduHome_Asp.net.Models;
using EduHome_Asp.net.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context) //servicesleri icinde yaz
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail details = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();

            BlogVM blogVM = new BlogVM
            {
                Sliders = sliders,
                //Details = details,
                Categories = categories,

            };

            return View(blogVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Blog blog = await _context.Blogs.Where(n => n.Id == id).FirstOrDefaultAsync();
            return View(blog);

        }
    }
}
