using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using epam_asp_task.Models;
using System.Text;

namespace epam_asp_task.Infrastructure
{
    public class ProjectRepository
    {
        ProjectContext pc = new ProjectContext();
        public IEnumerable<Article> Articles { get { return pc.Articles; } }
        public IEnumerable<Feedback> Feedbacks { get { return pc.Feedbacks; } }
        public IEnumerable<Inquirer> Inquirers { get { return pc.Inquirers; } }

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
            pc.Feedbacks.Add(feedback);
            pc.SaveChanges();
        }

        /*public Feedback RemoveFeedback(int feedbackToRemoveId)
        {
            Feedback feedbackToRemove = pc.Feedbacks.Where(a => a.Id == feedbackToRemoveId).FirstOrDefault();
            if (feedbackToRemove != null)
            {
                pc.Feedbacks.Remove(feedbackToRemove);
                pc.SaveChanges();
            }
            return feedbackToRemove;
        }*/

        public void AddInquirer(Inquirer inquirer)
        {
            pc.Inquirers.Add(inquirer);
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
            int count = 1;
            foreach (var pair in sorted)
            {
                result.Add(pair.Key);
                if(count == 20)
                {
                    break;
                }
                count++;
            }
            return result;
        }
    }
}