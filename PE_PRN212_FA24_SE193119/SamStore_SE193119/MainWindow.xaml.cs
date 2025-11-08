using BLL.Services;
using DAL.Entities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SamStore_SE193119
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SamPreOrderService _service = new();
        public SamPreOrder? Selected { get; set; } = null;


        public Member? CurrentAccount { get; set; } = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = _service.GetAllProjects();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.RoleId == 3)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = _service.SearchPreOrder(SearchText.Text.Trim());
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.RoleId == 3)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SamPreOrder? selected = DataGrid.SelectedItem as SamPreOrder;
                if (selected == null)
                {
                    MessageBox.Show("Please select a PreOrder to View or Update!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                DetailWindow w = new();
                w.IsView = true;
                w.Selected = selected;
                w.CurrentAccount = CurrentAccount;
                w.ShowDialog();
                FillDataGrid();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.RoleId == 2 || CurrentAccount.RoleId == 3)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DetailWindow w = new();
                w.CurrentAccount = CurrentAccount;
                w.ShowDialog();
                FillDataGrid();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.RoleId != 1)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SamPreOrder? selected = DataGrid.SelectedItem as SamPreOrder;
                if (selected == null)
                {
                    MessageBox.Show("Please select a PreOrder to View or Update!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                DetailWindow w = new();
                w.Selected = selected;
                w.CurrentAccount = CurrentAccount;
                w.ShowDialog();
                FillDataGrid();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.RoleId == 2 || CurrentAccount.RoleId == 3)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                SamPreOrder? selected = DataGrid.SelectedItem as SamPreOrder;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Order to Delete!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this one?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _service.DeletePreOrder(selected);
                    FillDataGrid();
                }
                else
                {
                    return;
                }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}