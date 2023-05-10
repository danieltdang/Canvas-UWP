using Canvas.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Canvas.Library.Utils
{
    public class ModuleDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate moduleTemplate { get; set; }
        public DataTemplate contentTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            FrameworkElement element = container as FrameworkElement;

            if (item is Module)
                return moduleTemplate;
            else if (item is ContentItem)
                return contentTemplate;
            else
                return null;
        }
    }
}
