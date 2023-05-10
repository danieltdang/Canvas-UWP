using NetStandard.LMS.Models;
using NetStandard.LMS.Services;
using System;
using System.Collections.ObjectModel;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContentTypeDialog : ContentDialog
    {

        public ContentTypeDialog(CourseDetailViewModel cdvm)
        {
            DataContext = cdvm;
            InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
            switch (ContentCombo.SelectedIndex)
            {
                case 0:
                    var pageDiag = new PageItemDialog();
                    await pageDiag.ShowAsync();
                    break;
                case 1:
                    var fileDiag = new FileItemDialog();
                    await fileDiag.ShowAsync();
                    break;
                case 2:
                    var assignmentDiag = new AssignmentItemDialog();
                    await assignmentDiag.ShowAsync();
                    break;
            }
            (DataContext as CourseDetailViewModel).RefreshModules();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
