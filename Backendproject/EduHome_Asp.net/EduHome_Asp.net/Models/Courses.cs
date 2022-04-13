using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImageTitle { get; set; }
        public string Description { get; set; }
        public string AboutDescription { get; set; }
        public string ApplyDescription { get; set; }
        public string CertificationDescription { get; set; }
        public int? CourseFeaturesId { get; set; }

        public CoursesFeatures CoursesFeatures { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }





    }
}
