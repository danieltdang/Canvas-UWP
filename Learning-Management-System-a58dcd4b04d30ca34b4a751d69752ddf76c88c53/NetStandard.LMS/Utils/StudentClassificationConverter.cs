using NetStandard.LMS.Models;
using System;
using Windows.UI.Xaml.Data;

namespace NetStandard.LMS.Utils
{
    public class StudentClassificationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Student.StudentClassification)(int)value;
        }
    }
}