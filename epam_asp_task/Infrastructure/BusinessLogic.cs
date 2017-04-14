using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using epam_asp_task.Models;
using System.Text;
using epam_asp_task.ViewModels;

namespace epam_asp_task.Infrastructure
{
    public class BusinessLogic
    {
        ProjectContext pc = new ProjectContext();
        public IEnumerable<Article> Articles { get { return pc.Articles; } }
        public IEnumerable<Feedback> Feedback { get { return pc.Feedback; } }
        public IEnumerable<Inquirer> Inquirers { get { return pc.Inquirers; } }
        public IEnumerable<Comment> Comments { get; set; }

        private const int NumberOfTags = 20;

        private const int NumberOfSimilarArticles = 5;

        public void AddComment(Comment comment)
        {
            comment.PublicationDate = DateTime.UtcNow;
            pc.Comments.Add(comment);
            if (comment.ParentIsArticle)
            {
                Article parent = pc.Articles.Where(a => a.Id == comment.ParentId).First();
                parent.Comments.Add(comment);
            }
            else
            {
                Comment parent = pc.Comments.Where(c => c.Id == comment.ParentId).First();
                parent.Comments.Add(comment);
            }

            pc.SaveChanges();
        }

        public void AddCommentForComment(Comment comment, int commentId)
        {
            pc.Comments.Add(comment);
            Comment parent = pc.Comments.Where(a => a.Id == commentId).First();
            parent.Comments.Add(comment);
            pc.SaveChanges();
        }

        public Dictionary<int,string> GetSimilarArticles(int articleId)
        {
            Article articleToAnalyze = pc.Articles.Where(a => a.Id == articleId).First();
            string[] tagsToAnalyze = articleToAnalyze.Keywords.Split(' ');
            Dictionary<int, string> similarArticles = new Dictionary<int, string>();
            int articlesCount = 0;
            foreach (Article article in pc.Articles)
            {
                if (article.Id == articleToAnalyze.Id)
                {
                    continue;
                }
                string[] tags = article.Keywords.Split(' ');
                foreach (string tag in tagsToAnalyze)
                {
                    if (tags.Contains(tag))
                    {
                        similarArticles.Add(article.Id, article.Name);
                        if (articlesCount == NumberOfSimilarArticles)
                        {
                            return similarArticles;
                        }
                        articlesCount++;
                        break;
                    }
                }
            }
            return similarArticles;
        }

        public List<Article> GetSmallArticles()
        {
            List<Article> smallArticles = new List<Article>();
            foreach (Article article in pc.Articles)
            {
                StringBuilder content = new StringBuilder();
                Article smallArticle = new Article() {
                    Name = article.Name,
                    Keywords = article.Keywords,
                    Id = article.Id,
                    PublicationDate = article.PublicationDate
                };
                if (article.Content.Length >= 200)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        content.Append(article.Content[i]);
                    }
                    content.Append("...");
                    smallArticle.Content = content.ToString();
                }
                else
                {
                    smallArticle.Content = article.Content;
                }
                smallArticles.Add(smallArticle);
            }
            return smallArticles;
        }

        public void SaveArticle(Article article)
        {
            if(article.Id == 0)
            {
                article.PublicationDate = DateTime.UtcNow;
                pc.Articles.Add(article);
            }
            else
            {
                Article newArticle = pc.Articles.Find(article.Id);
                if(newArticle != null)
                {
                    newArticle.Name = article.Name;
                    newArticle.Content = article.Content;
                    newArticle.PublicationDate = article.PublicationDate;
                    newArticle.Keywords = article.Keywords;
                }
            }
            pc.SaveChanges();
        }

        public Article RemoveArticle(int articleToRemoveId)
        {
            Article articleToRemove = pc.Articles.Where(a => a.Id == articleToRemoveId).First();
            if (articleToRemove != null)
            {
                pc.Articles.Remove(articleToRemove);
                pc.SaveChanges();
            }
            return articleToRemove;
        }

        public void AddFeedback(Feedback feedback)
        {
            feedback.PublicationDate = DateTime.UtcNow;
            pc.Feedback.Add(feedback);
            pc.SaveChanges();
        }

        public void SaveInquirer(InquirerViewModel viewModel)
        {
            Inquirer inquirerForSave = new Inquirer();
            inquirerForSave.Name = viewModel.Name;
            inquirerForSave.Text = viewModel.TextInput;
            inquirerForSave.HasText = viewModel.HasText;
            inquirerForSave.HasRadio = viewModel.HasRadio;
            inquirerForSave.Radio = viewModel.RadioInput;
            pc.Inquirers.Add(inquirerForSave);
            pc.SaveChanges();
        }

        public IEnumerable<Inquirer> GetInquirerResults(string inquirerName)
        {
            IEnumerable<Inquirer> inquirersToAnalyze = pc.Inquirers.Where(i => i.Name.Equals(inquirerName));
            return inquirersToAnalyze;
        }

        public List<string> GetPopularTags()
        {
            Dictionary<string, int> tagDictionary = new Dictionary<string, int>();
            IEnumerable<Article> articles = pc.Articles;
            foreach (Article article in articles)
            {
                string[] tags = article.Keywords.Split(' ');
                foreach (string tag in tags)
                {
                    if (tagDictionary.ContainsKey(tag))
                    {
                        tagDictionary[tag]++;
                    }
                    else
                    {
                        tagDictionary.Add(tag, 1);
                    }
                }
            }
            var sorted = tagDictionary.OrderByDescending(x => x.Value);
            List<string> result = new List<string>();
            int tagCount = 1;
            foreach (var pair in sorted)
            {
                result.Add(pair.Key);
                if(tagCount == NumberOfTags)
                {
                    break;
                }
                tagCount++;
            }
            return result;
        }
    }
}