using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class EventDetail
    {
        public int Id { get; set; }
        public string DetailImage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }




    }
}
