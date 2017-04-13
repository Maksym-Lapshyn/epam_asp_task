using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using epam_asp_task.Models;
using epam_asp_task.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace epam_asp_task.ViewModels
{
    public class InquirerViewModel
    {
        public const string EmptyInput = "Empty";

        public string Name { get; set; }

        public string TextQuestion { get; set; }

        [Required(ErrorMessage = "Fill in text input")]
        public string TextInput { get; set; }

        public string RadioQuestion { get; set; }

        [Required(ErrorMessage = "Select radio button")]
        public string RadioInput { get; set; }

        public string[] RadioOptions { get; set; }
    }
}