using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using epam_asp_task.Infrastructure;
using epam_asp_task.Models;

namespace epam_asp_task.Infrastructure
{
    public class ArticleNameAttribute : ValidationAttribute
    {
        BusinessLogic repository = new BusinessLogic();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string articleName = value.ToString();
            Article sameArticle = repository.Articles.Where(a => a.Name == articleName).FirstOrDefault();
            if (sameArticle == null)
            {
                return null;
            }
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}