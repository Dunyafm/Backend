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
    [Area("AdminArea")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context) //servicesleri icinde yaz
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail details = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();

            CoursesVM courseVM = new CoursesVM
            {
                Sliders = sliders,
                //Details = details,
                Categories = categories,

            };

            return View(courseVM);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Blog blog = await _context.Blogs.Where(n => n.Id == id).FirstOrDefaultAsync();
            return View(blog);

        }
    }
}
