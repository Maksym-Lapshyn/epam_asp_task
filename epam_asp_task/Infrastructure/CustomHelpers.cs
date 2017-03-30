using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Models;

namespace epam_asp_task.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString GenerateInquirerResults(this HtmlHelper html, IEnumerable<Inquirer> inquirers)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("list-group");
            Inquirer inquirer = inquirers.FirstOrDefault();
            Dictionary<string, int> resultDictionary = new Dictionary<string, int>();
            if (inquirer.InquirerTextInput != null)
            {
                foreach (Inquirer singleResult in inquirers)
                {
                    if(!resultDictionary.ContainsKey(singleResult.InquirerTextInput))
                    {
                        resultDictionary.Add(singleResult.InquirerTextInput, 1);
                    }
                    else
                    {
                        resultDictionary[singleResult.InquirerTextInput]++;
                    }
                }
            }
            else if (inquirer.InquirerRadio != null)
            {
                foreach (Inquirer singleResult in inquirers)
                {
                    if (!resultDictionary.ContainsKey(singleResult.InquirerRadio))
                    {
                        resultDictionary.Add(singleResult.InquirerRadio, 1);
                    }
                    else
                    {
                        resultDictionary[singleResult.InquirerRadio]++;
                    }
                }
            }
            else if (inquirer.InquirerCheckboxes != null)
            {
                
                foreach (Inquirer singleResult in inquirers)
                {
                    string[] checkboxesValues = singleResult.InquirerCheckboxes.Split('+');
                    foreach (string value in checkboxesValues)
                    {
                        if (!resultDictionary.ContainsKey(value))
                        {
                            resultDictionary.Add(value, 1);
                        }
                        else
                        {
                            resultDictionary[value]++;
                        }
                    }
                }
            }
            var ordered = resultDictionary.OrderByDescending(x => x.Value);
            foreach (var pair in ordered)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("list-group-item");
                li.SetInnerText(string.Format("{0} - {1}", pair.Key, pair.Value));
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }
    }
}