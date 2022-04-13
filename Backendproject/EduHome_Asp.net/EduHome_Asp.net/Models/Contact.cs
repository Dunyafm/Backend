using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Hidden { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }

        [StringLength(260), MinLength(5)]

        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }

    }
}
