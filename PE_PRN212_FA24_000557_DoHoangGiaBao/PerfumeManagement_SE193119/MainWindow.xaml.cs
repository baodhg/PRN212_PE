using BLL.Services;
using DAL.Entities;
using System.Windows;
using System.Windows.Controls;

namespace PerfumeManagement_SE193119
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PerfumeInfomationService _perfumeService = new();
        public Psaccount? CurrentAccount { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.Role == 3)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = _perfumeService.SearchPerfume(SearchText.Text.Trim());
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAccount.Role == 1 || CurrentAccount.Role == 4)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                PerfumeInformation? selected = DataGrid.SelectedItem as PerfumeInformation;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Perfume to View or Update!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            if (CurrentAccount.Role != 2)
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
            if (CurrentAccount.Role != 2)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                PerfumeInformation? selected = DataGrid.SelectedItem as PerfumeInformation;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Perfume to View or Update!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            if (CurrentAccount.Role != 2)
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                PerfumeInformation? selected = DataGrid.SelectedItem as PerfumeInformation;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Perfume to Delete!", "Select a row", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this one?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _perfumeService.DeletePerfume(selected);
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

        public void FillDataGrid()
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = _perfumeService.GetAllPerfumes();
        }

        
    }
}