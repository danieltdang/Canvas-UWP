using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Library.Models
{
    public class PageItem : ContentItem
    {
        public string HTMLBody { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                   $"\n\t{HTMLBody}";
        }
    }
}