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
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;

        public SubscribeController(AppDbContext context) //servicesleri icinde yaz
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {

            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail detail = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();

            SubscribeVM SubscribeVM = new SubscribeVM
            {
                Sliders = sliders,
                Detail = detail,
                Categories = categories,

            };

            return View(SubscribeVM);


        }


    }
}
