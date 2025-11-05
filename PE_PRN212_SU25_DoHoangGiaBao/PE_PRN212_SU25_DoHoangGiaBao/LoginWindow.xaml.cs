using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PE_PRN212_SU25_DoHoangGiaBao
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private JLPTAccountService _jlptAccountService = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (email.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var account = _jlptAccountService.Authenticate(email, password);

            if (account == null)
            {
                MessageBox.Show("Invalid Email or Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else
            {
                if (account.Role == 4)
                {
                    MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MainWindow mw = new();
                mw.CurrentAccount = account;
                mw.ShowDialog();
                this.Hide();
            }
        }
    }
}
