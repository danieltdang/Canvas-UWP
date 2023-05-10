using NetStandard.LMS.Models;
using NetStandard.LMS.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml.Controls;
using Module = NetStandard.LMS.Models.Module;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileItemDialog : ContentDialog
    {
        private bool isEditMode;
        public ObservableCollection<Module> Modules { get; set; }
        public Module selectedModule { get; set; }

        public FileItemDialog()
        {
            InitializeComponent();
            DataContext = new FileItem();
            Modules = CourseViewModel.selectedCourse.Modules;
            isEditMode = false;
        }

        public FileItemDialog(Module module, FileItem fileItem)
        {
            InitializeComponent();
            DataContext = fileItem;
            Modules = CourseViewModel.selectedCourse.Modules;
            selectedModule = module;
            ModuleCombo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            isEditMode = true;
            Title = "Edit File Item";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                selectedModule.Update(DataContext as FileItem);
            }
            else
            {
                selectedModule.Add(DataContext as FileItem);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
