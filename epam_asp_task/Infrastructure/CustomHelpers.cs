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
            TagBuilder h1 = new TagBuilder("h1");
            Inquirer inquirer = inquirers.First();
            h1.InnerHtml += inquirer.Name;
            ul.InnerHtml += h1.ToString();
            Dictionary<string, int> textDictionary = new Dictionary<string, int>();
            if (inquirer.TextInput != null)
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
            if (inquirer.Radio != null)
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
            /*Dictionary<string, int> checkboxDictionary = new Dictionary<string, int>();
            if (inquirer.Checkboxes != null)
            {

                foreach (Inquirer singleResult in inquirers)
                {
                    string[] checkboxesValues = singleResult.Checkboxes.Split('+');
                    foreach (string value in checkboxesValues)
                    {
                        if (!checkboxDictionary.ContainsKey(value))
                        {
                            checkboxDictionary.Add(value, 1);
                        }
                        else
                        {
                            checkboxDictionary[value]++;
                        }
                    }
                }
                var checkboxDictionaryOrdered = checkboxDictionary.OrderByDescending(x => x.Value);
                foreach (var pair in checkboxDictionaryOrdered)
                {
                    TagBuilder li = new TagBuilder("li");
                    li.AddCssClass("list-group-item");
                    li.SetInnerText(string.Format("{0} - {1}", pair.Key, pair.Value));
                    ul.InnerHtml += li.ToString();
                }
                TagBuilder hr = new TagBuilder("hr");
                ul.InnerHtml += hr.ToString();
            }*/
            return new MvcHtmlString(ul.ToString());
        }

        public static MvcHtmlString RenderInquirer(this HtmlHelper html, Inquirer inquirer)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("list-group");
            if (inquirer.TextInput != null)
            {
                TagBuilder label = new TagBuilder("label");
                label.Attributes.Add("for", "TextInput");
                label.InnerHtml += inquirer.TextInput;
                div.InnerHtml += label.ToString();
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("name", "TextInput");
                input.Attributes.Add("type", "text");
                input.Attributes.Add("value", "Kharkiv");
                input.Attributes.Add("placeholder", inquirer.TextInput);
                input.AddCssClass("list-group-item");
                div.InnerHtml += input.ToString(TagRenderMode.SelfClosing);
                TagBuilder hr = new TagBuilder("hr");
                div.InnerHtml += hr.ToString();
            }
            if (inquirer.Radio != null)
            {
                string[] radios = inquirer.Radio.Split('+');
                TagBuilder label = new TagBuilder("label");
                label.InnerHtml += radios[0];
                div.InnerHtml += label.ToString();
                for (int i = 1; i < radios.Length; i++)
                {
                    TagBuilder p = new TagBuilder("p");
                    label = new TagBuilder("label");
                    label.Attributes.Add("for", radios[i]);
                    label.InnerHtml += radios[i];
                    TagBuilder radio = new TagBuilder("input");
                    radio.Attributes.Add("type", "radio");
                    radio.Attributes.Add("name", "Radio");
                    radio.Attributes.Add("id", radios[i]);
                    radio.Attributes.Add("value", radios[i]);
                    if(i == 1)
                    {
                        radio.Attributes.Add("checked", "checked");
                    }
                    p.InnerHtml += string.Format("{0}   {1}", label.ToString(), radio.ToString(TagRenderMode.SelfClosing));
                    p.AddCssClass("list-group-item");
                    div.InnerHtml += p.ToString();
                }
                TagBuilder hr = new TagBuilder("hr");
                div.InnerHtml += hr.ToString();
            }
            /*
            if (inquirer.Checkboxes != null)
            {
                string[] checkboxes = inquirer.Checkboxes.Split('+');
                TagBuilder label = new TagBuilder("label");
                label.InnerHtml += checkboxes[0];
                div.InnerHtml += label.ToString();
                for (int i = 1; i < checkboxes.Length; i++)
                {
                    TagBuilder p = new TagBuilder("p");
                    label = new TagBuilder("label");
                    label.Attributes.Add("for", checkboxes[i]);
                    label.InnerHtml += checkboxes[i];
                    TagBuilder checkbox = new TagBuilder("input");
                    checkbox.Attributes.Add("type", "checkbox");
                    checkbox.Attributes.Add("name", "Chekboxes");
                    checkbox.Attributes.Add("id", checkboxes[i]);
                    checkbox.Attributes.Add("value", checkboxes[i]);
                    p.InnerHtml += string.Format("{0}   {1}", label.ToString(), checkbox.ToString(TagRenderMode.SelfClosing));
                    p.AddCssClass("list-group-item");
                    div.InnerHtml += p.ToString();
                }
            }*/
            return new MvcHtmlString(div.ToString());
        }
    }
}