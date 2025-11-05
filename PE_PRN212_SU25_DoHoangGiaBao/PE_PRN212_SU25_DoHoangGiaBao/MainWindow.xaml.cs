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

namespace PE_PRN212_SU25_DoHoangGiaBao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MockTestService _service = new();
        public Jlptaccount? CurrentAccount { get; set; } = null;
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
            MockTestGrid.ItemsSource = null;
            MockTestGrid.ItemsSource = _service.GetAllMockTests();
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MockTestGrid.ItemsSource = null;
            MockTestGrid.ItemsSource = _service.SearchMockTests(SearchText.Text.Trim());
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.Role == 4 || CurrentAccount.Role == 3)
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
            if (CurrentAccount.Role == 4 || CurrentAccount.Role == 3)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MockTest? selected = MockTestGrid.SelectedItem as MockTest;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Mock Test to View or Update!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                DetailWindow w = new();
                w.Selected = selected;
                w.CurrentAccount = CurrentAccount;
                w.ShowDialog();
                FillDataGrid();
            }
        }

        private void MockTestGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.Role == 2 || CurrentAccount.Role == 3 || CurrentAccount.Role == 4)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MockTest? selected = MockTestGrid.SelectedItem as MockTest;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Mock Test to Delete!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Mock Test?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _service.DeleteMockTest(selected);
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

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.Role == 4)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MockTest? selected = MockTestGrid.SelectedItem as MockTest;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Mock Test to View or Update!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
    }
}