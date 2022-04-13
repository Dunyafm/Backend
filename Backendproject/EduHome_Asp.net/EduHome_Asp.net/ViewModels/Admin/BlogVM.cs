using EduHome_Asp.net.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.ViewModels.Admin
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }
        public string Header { get; set; }
        public string Location { get; set; }
        public string PhotoDb { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Detail> Details { get; set; }
        [Required, NotMapped]
        public List<IFormFile> Photos { get; set; }
    }
}
