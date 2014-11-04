using System.Windows;
using System.Windows.Input;

namespace TimeTrackR.Views
{
    public partial class TagSelection : Window
    {
        public TagSelection()
        {
            InitializeComponent();

            TextBoxTagEntry.Focus();

            PreviewKeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
