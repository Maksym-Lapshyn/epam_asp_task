using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace epam_asp_task.Infrastructure
{
    public class CommentContentAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string content = value.ToString();
            if (content.Contains("<iframe>") || content.Contains("<object>") || content.Contains("<script>") ||
                content.Contains("</iframe>") || content.Contains("</object>") || content.Contains("</script>"))
            {
                return false;
            }
            return true;
        }
    }
}