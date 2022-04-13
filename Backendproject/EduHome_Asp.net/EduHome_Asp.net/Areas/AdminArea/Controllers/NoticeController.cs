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
    public class NoticeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public NoticeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Notice> notices = await _context.Notices.AsNoTracking().ToListAsync();

            return View(notices);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(NoticeVM noticeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = _context.Notices.Any(m => m.Description.ToLower().Trim() == noticeVM.Description.ToLower().Trim());

            if (isExist)
            {
                ModelState.AddModelError("Name", "bu artiq movcuddur");
                return View();
            }

            Notice notice = new Notice
            {
                Name = noticeVM.Name
            };
            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Notice notice = await GetNoticeById(id);
            if (notice == null) return NotFound();
            string path = Helper.GetFilePath(_env.WebRootPath, "img", notice.Header);

            Helper.DeleteFile(path);

            _context.Notices.Remove(notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var Notice = await GetNoticeById(id);
        //    if (Notice is null) return NotFound();
        //    return View(Notice);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Notice notice)
        //{
        //    var dbnotice = await GetNoticeById(id);
        //    if (dbnotice == null) return NotFound();

        //    if (ModelState["photo"].ValidationState == ModelValidationState.Invalid) return View();

        //    if (!notice.Photos.CheckFileType("image/"))
        //    {
        //        ModelState.AddModelError("photo", "image type is wrong");
        //        return View();
        //    }

        //    if (!notice.Photos.CheckFileSize(200))
        //    {
        //        ModelState.AddModelError("photo", "image size is wrong");
        //        return View();
        //    }

        //    string path = Helper.GetFilePath(_env.WebRootPath, "img", dbnotice.Header);

        //    Helper.DeleteFile(path);


        //    string FileName = Guid.NewGuid().ToString() + "_" + notice.Photos.FileName;

        //    string newpath = Helper.GetFilePath(_env.WebRootPath, "img", FileName);

        //    using (FileStream stream = new FileStream(newpath, FileMode.Create))
        //    {
        //        await notice.Photos.CopyToAsync(stream);
        //    }

        //    dbnotice.Header = FileName;

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

            private async Task<Notice> GetNoticeById(int id)
            {

                return await _context.Notices.FindAsync(id);
            }

            public async Task<IActionResult> Detail(int id)
            {
                var notice = await GetNoticeById(id);
                if (notice is null) return NotFound();
                return View(notice);
            }



        
    }
}
