using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace epam_asp_task.Models
{
    public class Inquirer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Fill this input")]
        public string TextInput { get; set; }

        [Required(ErrorMessage = "Fill this input")]
        public string Radio { get; set; }
    }
}