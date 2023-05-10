using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Library.Models
{
    public class Module : CourseDetail
    {
        public string Description { get; set; }
        public ObservableCollection<ContentItem> Content { get; set; }

        public Module() 
        { 
            Content = new ObservableCollection<ContentItem>();
        }
        public override string ToString()
        {
            return $"{Name}:" +
                   $"\n{Description}";
        }

        public string ShortString()
        {
            return $"{Name}";
        }

        public void Add(ContentItem contentItem)
        {
            Content.Add(contentItem);
        }

        public void Remove(ContentItem contentItem)
        {
            Content.Remove(contentItem);
        }

        public void Update(ContentItem contentItem)
        {
            var contentIndex = Content.IndexOf(contentItem);
            Content.RemoveAt(contentIndex);
            Content.Insert(contentIndex, contentItem);
        }
    }
}