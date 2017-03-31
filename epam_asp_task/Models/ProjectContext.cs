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
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Content { get; set; }

    }

    public class Feedback
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Enter feedback content")]
        public string Content { get; set; }
    }

    public class Inquirer
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        public string Author { get; set; }

        public string TextInput { get; set; }

        public string Checkboxes { get; set; }

        public string Radio { get; set; }
    }
}