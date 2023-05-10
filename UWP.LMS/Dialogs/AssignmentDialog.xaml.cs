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
    public sealed partial class AssignmentDialog : ContentDialog
    {
        private bool isEditMode;
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public AssignmentGroup selectedAssignmentGroup { get; set; }
        public string TotalAvailablePoints { get; set; }

        public AssignmentDialog()
        {
            InitializeComponent();
            DataContext = new Assignment();
            TotalAvailablePoints = (DataContext as Assignment).TotalAvailablePoints.ToString();
            AssignmentGroups = CourseViewModel.selectedCourse.AssignmentGroups;
            isEditMode = false;
        }

        public AssignmentDialog(AssignmentGroup ag, Assignment assignment)
        {
            InitializeComponent();
            DataContext = assignment;
            TotalAvailablePoints = (DataContext as Assignment).TotalAvailablePoints.ToString();
            AssignmentGroups = CourseViewModel.selectedCourse.AssignmentGroups;
            selectedAssignmentGroup = ag;
            AgCombo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            isEditMode = true;
            Title = "Edit Assignment";
        }
        
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                selectedAssignmentGroup.Update(DataContext as Assignment);
            }
            else
            {
                selectedAssignmentGroup.Add(DataContext as Assignment);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
