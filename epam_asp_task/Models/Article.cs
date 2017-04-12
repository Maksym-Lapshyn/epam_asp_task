using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using epam_asp_task.Infrastructure;

namespace epam_asp_task.Models
{
    public class Article
    {
        public int Id { get; set; }

        [ArticleName(ErrorMessage = "Article with the same name already exists")]
        [Required(ErrorMessage = "Enter name for the article")]
        public string Name { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Enter content for the article")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Select keywords for the article")]
        public string Keywords { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Article()
        {
            Comments = new List<Comment>();
        }
    }
}