namespace epam_asp_task.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class ProjectContext : DbContext
    {
        // Your context has been configured to use a 'ProjectContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'epam_asp_task.Models.ProjectContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProjectContext' 
        // connection string in the application configuration file.
        public ProjectContext()
            : base("name=ProjectContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Inquirer> Inquirers { get; set; }
    }

    public class Article
    {
        public int ArticleId { get; set; }

        public string ArticleName { get; set; }

        public DateTime ArticlePublicationDate { get; set; }

        public string ArticleContent { get; set; }

    }

    public class Feedback
    {
        public int FeedbackId { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string FeedbackAuthor { get; set; }

        public DateTime FeedbackPublicationDate { get; set; }

        [Required(ErrorMessage = "Enter feedback content")]
        public string FeedbackContent { get; set; }
    }

    public class Inquirer
    {
        public int InquirerId { get; set; }
        
        public string InquirerName { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string InquirerAuthor { get; set; }

        public string InquirerTextInput { get; set; }

        public string InquirerCheckboxes { get; set; }

        public string InquirerRadio { get; set; }
    }
}