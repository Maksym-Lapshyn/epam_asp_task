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

        public string Text { get; set; }

        public bool HasText { get; set; }

        public string Radio { get; set; }

        public bool HasRadio { get; set; }
    }
}