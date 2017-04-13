using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace epam_asp_task.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Enter comment content")]
        public string Content { get; set; }

        public DateTime PublicationDate { get; set; }

        public bool ParentIsArticle { get; set; }

        public int ParentId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Comment()
        {
            Comments = new List<Comment>();
        }
    }
}