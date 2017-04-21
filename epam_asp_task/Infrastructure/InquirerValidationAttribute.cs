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
    }
}