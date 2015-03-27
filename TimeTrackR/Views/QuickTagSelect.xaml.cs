using System;
using System.Windows;
using System.Windows.Input;
using TimeTrackR.ViewModels;

namespace TimeTrackR.Views
{
    public partial class QuickTagSelect : Window
    {
        public QuickTagSelect()
        {
            InitializeComponent();

            PreviewKeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                var viewModel = DataContext as QuickTagSelectViewModel;

                if(viewModel != null)
                {
                    viewModel.Cancelled = true;
                }

                Close();
            }
        }

        private void QuickTagSelect_OnDeactivated(object sender, EventArgs e)
        {
            Close();
        }

        private void QuickTagSelect_OnLoaded(object sender, RoutedEventArgs e)
        {
            Activate();
        }
    }
}
