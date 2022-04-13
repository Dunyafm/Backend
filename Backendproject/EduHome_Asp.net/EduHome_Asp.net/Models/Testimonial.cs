using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Testimonial
    {
        public int Id { get; set; } //add-mig et
        public string Title { get; set; }
        public string Info { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }
    }
}
