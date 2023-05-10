using Canvas.Library.Models;
using Canvas.Library.Services;
using System;
using System.Collections.ObjectModel;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AssignmentTypeDialog : ContentDialog
    {

        public AssignmentTypeDialog(CourseDetailViewModel cdvm)
        {
            DataContext = cdvm;
            InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
            switch (AssignmentCombo.SelectedIndex)
            {
                case 0:
                    var agDiag = new AssignmentGroupDialog(CourseViewModel.selectedCourse.AssignmentGroups);
                    await agDiag.ShowAsync();
                    break;
                case 1:
                    var assignmentDiag = new AssignmentDialog();
                    await assignmentDiag.ShowAsync();
                    break;
            }
            (DataContext as CourseDetailViewModel).RefreshAssignments();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
