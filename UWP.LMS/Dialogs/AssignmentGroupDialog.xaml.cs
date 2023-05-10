using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AssignmentGroupDialog : ContentDialog
    {
        private bool isEditMode;
        
        public AssignmentGroupDialog(ObservableCollection<AssignmentGroup> ags)
        {
            InitializeComponent();
            DataContext = new AssignmentGroup();
            CourseViewModel.selectedCourse.AssignmentGroups = ags;
            isEditMode = false;
        }

        public AssignmentGroupDialog(ObservableCollection<AssignmentGroup> ags, AssignmentGroup ag)
        {
            InitializeComponent();
            DataContext = ag;
            CourseViewModel.selectedCourse.AssignmentGroups = ags;
            isEditMode = true;
            Title = "Edit Assignment Group";
        }
        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                CourseViewModel.selectedCourse.Update(DataContext as AssignmentGroup);
            }
            else
            {
                CourseViewModel.selectedCourse.Add(DataContext as AssignmentGroup);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
