using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Models;
using epam_asp_task.ViewModels;

namespace epam_asp_task.Infrastructure
{
    public static class CustomHelpers
    {
        public static MvcHtmlString GenerateInquirerResults(this HtmlHelper html, IEnumerable<Inquirer> inquirers)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("list-group");
            TagBuilder h1 = new TagBuilder("h1");
            Inquirer inquirer = inquirers.First();
            h1.InnerHtml += inquirer.Name;
            ul.InnerHtml += h1.ToString();
            Dictionary<string, int> textDictionary = new Dictionary<string, int>();
            if (inquirer.TextInput != null && inquirer.TextInput != "empty")
            {
                foreach (Inquirer singleResult in inquirers)
                {
                    if (!textDictionary.ContainsKey(singleResult.TextInput))
                    {
                        textDictionary.Add(singleResult.TextInput, 1);
                    }
                    else
                    {
                        textDictionary[singleResult.TextInput]++;
                    }
                }
                var textDictionaryOrdered = textDictionary.OrderByDescending(x => x.Value);
                foreach (var pair in textDictionaryOrdered)
                {
                    TagBuilder li = new TagBuilder("li");
                    li.AddCssClass("list-group-item");
                    li.SetInnerText(string.Format("{0} - {1}", pair.Key, pair.Value));
                    ul.InnerHtml += li.ToString();
                }
                TagBuilder hr = new TagBuilder("hr");
                ul.InnerHtml += hr.ToString();
            }
            Dictionary<string, int> radioDictionary = new Dictionary<string, int>();
            if (inquirer.Radio != null && inquirer.Radio != "empty")
            {
                foreach (Inquirer singleResult in inquirers)
                {
                    if (!radioDictionary.ContainsKey(singleResult.Radio))
                    {
                        radioDictionary.Add(singleResult.Radio, 1);
                    }
                    else
                    {
                        radioDictionary[singleResult.Radio]++;
                    }
                }
                var radioDictionaryOrdered = radioDictionary.OrderByDescending(x => x.Value);
                foreach (var pair in radioDictionaryOrdered)
                {
                    TagBuilder li = new TagBuilder("li");
                    li.AddCssClass("list-group-item");
                    li.SetInnerText(string.Format("{0} - {1}", pair.Key, pair.Value));
                    ul.InnerHtml += li.ToString();
                }
                TagBuilder hr = new TagBuilder("hr");
                ul.InnerHtml += hr.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }

        public static MvcHtmlString RenderInquirer(this HtmlHelper html, InquirerViewModel inquirer)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("list-group");
            if (inquirer.TextInput != InquirerViewModel.EmptyInput)
            {
                TagBuilder label = new TagBuilder("label");
                label.Attributes.Add("for", "TextInput");
                label.InnerHtml += inquirer.TextQuestion;
                div.InnerHtml += label.ToString();
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("name", "TextInput");
                input.Attributes.Add("type", "text");
                input.Attributes.Add("placeholder", inquirer.TextInput);
                input.AddCssClass("list-group-item");
                div.InnerHtml += input.ToString(TagRenderMode.SelfClosing);
                TagBuilder hr = new TagBuilder("hr");
                div.InnerHtml += hr.ToString();
            }
            if (inquirer.RadioInput != InquirerViewModel.EmptyInput)
            {
                TagBuilder label = new TagBuilder("label");
                label.InnerHtml += inquirer.RadioQuestion;
                div.InnerHtml += label.ToString();
                foreach (string option in inquirer.RadioOptions)
                {
                    TagBuilder p = new TagBuilder("p");
                    label = new TagBuilder("label");
                    label.Attributes.Add("for", option);
                    label.InnerHtml += option;
                    TagBuilder radio = new TagBuilder("input");
                    radio.Attributes.Add("type", "radio");
                    radio.Attributes.Add("name", "RadioInput");
                    radio.Attributes.Add("id", option);
                    radio.Attributes.Add("value", option);
                    p.InnerHtml += string.Format("{0}   {1}", label.ToString(), radio.ToString(TagRenderMode.SelfClosing));
                    p.AddCssClass("list-group-item");
                    div.InnerHtml += p.ToString();
                }
                TagBuilder hr = new TagBuilder("hr");
                div.InnerHtml += hr.ToString();
            }
            return new MvcHtmlString(div.ToString());
        }
    }
}