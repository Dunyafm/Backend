using EduHome_Asp.net.Data;
using EduHome_Asp.net.Models;
using EduHome_Asp.net.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.ViewComponents
{
        public class HeaderViewComponent : ViewComponent
        {
            private readonly AppDbContext _context;
            public HeaderViewComponent(AppDbContext context)
            {
                _context = context;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                int productCount = 0;
                if (Request.Cookies["basket"] != null)
                {
                    List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                    productCount = basket.Sum(m => m.Count);
                }
                else
                {
                    productCount = 0;
                }

                ViewBag.count = productCount;

               

                return (await Task.FromResult(View()));
            }
        }
    
}

