using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using epam_asp_task.Models;
using epam_asp_task.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace epam_asp_task.ViewModels
{
    [InquirerValidation(ErrorMessage = "Fill in all inputs")]
    public class InquirerViewModel
    {
        public string Name { get; set; }

        public string TextQuestion { get; set; }

        public string TextInput { get; set; }

        public bool HasText { get; set; }

        public string RadioQuestion { get; set; }

        public string RadioInput { get; set; }

        public bool HasRadio { get; set; }

        public string[] RadioOptions { get; set; }

        public InquirerViewModel()
        {
            HasText = true;
            HasRadio = true;
        }
    }
}