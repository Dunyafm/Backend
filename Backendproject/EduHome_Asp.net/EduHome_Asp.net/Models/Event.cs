using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int EventDetailld { get; set; }
        public EventDetail EventDetails { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }
      


    }
}
