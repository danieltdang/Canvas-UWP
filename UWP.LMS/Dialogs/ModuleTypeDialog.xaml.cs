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
    public sealed partial class ModuleTypeDialog : ContentDialog
    {

        public ModuleTypeDialog(CourseDetailViewModel cdvm)
        {
            DataContext = cdvm;
            InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
            switch (ModuleCombo.SelectedIndex)
            {
                case 0:
                    var moduleDiag = new ModuleDialog(CourseViewModel.selectedCourse.Modules);
                    await moduleDiag.ShowAsync();
                    (DataContext as CourseDetailViewModel).RefreshModules();
                    break;
                case 1:
                    var contentDiag = new ContentTypeDialog(DataContext as CourseDetailViewModel);
                    await contentDiag.ShowAsync();
                    break;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
