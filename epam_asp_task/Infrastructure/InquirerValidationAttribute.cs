using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using epam_asp_task.ViewModels;

namespace epam_asp_task.Infrastructure
{
    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InquirerValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            InquirerViewModel viewModel = value as InquirerViewModel;
            if (viewModel.HasRadio && viewModel.RadioInput == null)
            {
                return false;
            }

            if (viewModel.HasText && viewModel.TextInput == null)
            {
                return false;
            }

            return true;
        }

        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string articleName = value.ToString();
            Article sameArticle = repository.Articles.Where(a => a.Name == articleName).FirstOrDefault();
            if (sameArticle == null)
            {
                return null;
            }
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }*/
    }
}