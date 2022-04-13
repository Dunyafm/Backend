using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int count { get; set; }
        public string Description { get; set; }
        public int TeacherDetailId { get; set; }
        public SliderDetail SliderDetail { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }

    }
}
