﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using epam_asp_task.Models;

namespace epam_asp_task.Models
{
    public class ProjectRepository
    {
        ProjectContext pc = new ProjectContext();
        public IEnumerable<Article> Articles { get{ return pc.Articles; } }
        public IEnumerable<Feedback> Feedbacks { get { return pc.Feedbacks; } }
        public IEnumerable<QuizResult> QuizResults { get { return pc.QuizResults; } }

        public void AddArticle(Article newArticle)
        {
            pc.Articles.Add(newArticle);
            pc.SaveChanges();
        }

        public Article RemoveArticle(int articleToRemoveId)
        {
            Article articleToRemove = pc.Articles.Where(a => a.ArticleId == articleToRemoveId).FirstOrDefault();
            if (articleToRemove != null)
            {
                pc.Articles.Remove(articleToRemove);
                pc.SaveChanges();
            }
            return articleToRemove;
        }

        public void AddFeedback(Feedback newFeedback)
        {
            pc.Feedbacks.Add(newFeedback);
            pc.SaveChanges();
        }

        public Feedback RemoveFeedback(int feedbackToRemoveId)
        {
            Feedback feedbackToRemove = pc.Feedbacks.Where(a => a.FeedbackId == feedbackToRemoveId).FirstOrDefault();
            if (feedbackToRemove != null)
            {
                pc.Feedbacks.Remove(feedbackToRemove);
                pc.SaveChanges();
            }
            return feedbackToRemove;
        }
    }
}