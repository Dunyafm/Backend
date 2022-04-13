using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Image { get; set; }

        //[NotMapped]

        //public IFormFile[] Photos { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        [NotMapped]
        [Required]
        public List<IFormFile> Photos { get; set; }



    }
    
}
