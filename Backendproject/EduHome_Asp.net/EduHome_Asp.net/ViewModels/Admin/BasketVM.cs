using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.ViewModels.Admin
{
    public class BasketVM
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [Required, NotMapped]
        public List<IFormFile> Photos { get; set; }

    }
}
