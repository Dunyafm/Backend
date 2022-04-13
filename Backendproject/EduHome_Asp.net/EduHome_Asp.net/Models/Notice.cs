using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Notice
    {
        public int Id { get; set; }
        //public string Image { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }


        //public string Title { get; set; }
        public int Date { get; set; }
        public string Description { get; set; }
        [NotMapped,Required]
        public List<IFormFile> Photos { get; set; }
    }
}
