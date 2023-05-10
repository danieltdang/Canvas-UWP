using NetStandard.LMS.Models;
using NetStandard.LMS.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AssignmentGradeDialog : ContentDialog
    {
        public Assignment selectedAssignment { get; set; }
        public string Grade { get; set; }

        public AssignmentGradeDialog(Assignment assignment, Student student)
        {
            InitializeComponent();
            DataContext = student;
            selectedAssignment = assignment;
            Grade = (DataContext as Student).Grades[selectedAssignment.Id].ToString();
            Title = $"Grade for {(DataContext as Student).Name}";
            GradeBox.Header = $"Grade out of {selectedAssignment.TotalAvailablePoints}";
        }
        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as Student).Grade(selectedAssignment, decimal.Parse(Grade));
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
