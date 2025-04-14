using System;
using System.Windows;

namespace PersonApp
{
    public partial class EditUserWindow : Window
    {
        public EditUserWindow()
        {
            InitializeComponent();
            Loaded += EditUserWindow_Loaded;
        }

        private void EditUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditUserViewModel viewModel)
            {
                viewModel.CloseAction = (result) =>
                {
                    DialogResult = result;
                    Close();
                };
            }
        }

        
        public Person EditedPerson => (DataContext as EditUserViewModel)?.EditedPerson;
    }
}
