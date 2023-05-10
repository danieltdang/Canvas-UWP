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
    public sealed partial class PageItemDialog : ContentDialog
    {
        private bool isEditMode;
        public ObservableCollection<Module> Modules { get; set; }
        public Module selectedModule { get; set; }

        public PageItemDialog()
        {
            InitializeComponent();
            DataContext = new PageItem();
            Modules = CourseViewModel.selectedCourse.Modules;
            isEditMode = false;
        }

        public PageItemDialog(Module module, PageItem pageItem)
        {
            InitializeComponent();
            DataContext = pageItem;
            Modules = CourseViewModel.selectedCourse.Modules;
            selectedModule = module;
            ModuleCombo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            isEditMode = true;
            Title = "Edit Page Item";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (isEditMode)
            {
                selectedModule.Update(DataContext as PageItem);
            }
            else
            {
                selectedModule.Add(DataContext as PageItem);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
