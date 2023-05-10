using Canvas.Library.Models;
using Canvas.Library.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml.Controls;
using Module = Canvas.Library.Models.Module;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModuleDialog : ContentDialog
    {
        private bool isEditMode;

        public ModuleDialog(ObservableCollection<Module> modules)
        {
            InitializeComponent();
            DataContext = new Module();
            CourseViewModel.selectedCourse.Modules = modules;
            isEditMode = false;
        }

        public ModuleDialog(ObservableCollection<Module> modules, Module module)
        {
            InitializeComponent();
            DataContext = module;
            CourseViewModel.selectedCourse.Modules = modules;
            isEditMode = true;
            Title = "Edit Module";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                CourseViewModel.selectedCourse.Update(DataContext as Module);
            }
            else
            {
                CourseViewModel.selectedCourse.Add(DataContext as Module);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
