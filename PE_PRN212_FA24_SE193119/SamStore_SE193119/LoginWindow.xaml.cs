using BLL.Services;
using DAL.Entities;
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

namespace SamStore_SE193119
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly MemberService _service = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (email.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var account = _service.Authenticate(email, password);

            if (account == null)
            {
                MessageBox.Show("Invalid Email or Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (account.RoleId == 3)
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
