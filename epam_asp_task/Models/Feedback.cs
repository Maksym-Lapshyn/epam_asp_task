using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace epam_asp_task.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Enter feedback content")]
        public string Content { get; set; }
    }
}