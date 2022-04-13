using EduHome_Asp.net.Controllers;
using EduHome_Asp.net.Data;
using EduHome_Asp.net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
            //layoutService = layoutService;
            //_productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return (await Task.FromResult(View(blogs)));
        }








    }
}
