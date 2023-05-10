using NetStandard.LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace NetStandard.LMS.Utils
{
    public class AssignmentDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate agTemplate { get; set; }
        public DataTemplate assignmentTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            FrameworkElement element = container as FrameworkElement;

            if (item is AssignmentGroup)
                return agTemplate;
            else if (item is Assignment)
                return assignmentTemplate;
            else
                return null;
        }
    }
}
