using BLL.Services;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows;

namespace SamStore_SE193119
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private readonly SamPreOrderService _preOrderservice = new();
        private readonly SamProductService _productService = new();
        public SamPreOrder? Selected { get; set; } = null;
        public Member? CurrentAccount { get; set; } = null;
        public bool IsView { get; set; } = false;
        public DetailWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SamPreOrder p = new();

            if (IdTextBox.Text.IsNullOrEmpty() ||
                NoTextBox.Text.IsNullOrEmpty() ||
                DepositTextBox.Text.IsNullOrEmpty() ||
                TotalTextBox.Text.IsNullOrEmpty() ||
                NameTextBox.Text.IsNullOrEmpty() ||
                AddressTextBox.Text.IsNullOrEmpty() ||
                PhoneTextBox.Text.IsNullOrEmpty() ||
                StatusTextBox.Text.IsNullOrEmpty() ||
                CreatedAtTextBox.Text.IsNullOrEmpty() ||
                UpdatedAtTextBox.Text.IsNullOrEmpty() ||
                ProductComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(IdTextBox.Text, out int id) == false)
            {
                MessageBox.Show("ID must be an integer number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NoTextBox.Text.Length < 1 || NoTextBox.Text.Length > 10)
            {
                MessageBox.Show("PreOrder No must be between 1 and 10 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(DepositTextBox.Text, out int deposit) == false)
            {
                MessageBox.Show("Deposit Amount must be an integer number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(TotalTextBox.Text, out int total) == false)
            {
                MessageBox.Show("Total Amount must be an integer number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NameTextBox.Text.Length < 1 || NameTextBox.Text.Length > 50)
            {
                MessageBox.Show("Customer Name must be between 1 and 50 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var regex = new Regex(@"^[A-Z1-9][A-Za-z0-9]*( [A-Z1-9][A-Za-z0-9]*)*$");
            if (!regex.IsMatch(NameTextBox.Text))
            {
                MessageBox.Show("Customer Name cannot contain special characters and must follow format: [Tran Van A]", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (AddressTextBox.Text.Length < 1 || AddressTextBox.Text.Length > 150)
            {
                MessageBox.Show("Address must be between 1 and 50 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            if (DateOnly.TryParse(CreatedAtTextBox.Text, out DateOnly createdAt) == false ||
                DateOnly.TryParse(UpdatedAtTextBox.Text, out DateOnly updatedAt) == false)
            {
                MessageBox.Show("Please enter valid dates in the format yyyy-dd-mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (createdAt > updatedAt)
            {
                MessageBox.Show("Created At date cannot be later than Updated At date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            p.ProductId = int.Parse(IdTextBox.Text);
            p.PreOrderNo = NoTextBox.Text;
            p.DepositAmount = int.Parse(DepositTextBox.Text);
            p.TotalAmount = int.Parse(TotalTextBox.Text);
            p.CustomerName = NameTextBox.Text;
            p.CustomerAddress = AddressTextBox.Text;
            p.CustomerPhone = PhoneTextBox.Text;
            p.Status = StatusTextBox.Text;
            p.CreatedAt = DateOnly.Parse(CreatedAtTextBox.Text);
            p.UpdatedAt = DateOnly.Parse(UpdatedAtTextBox.Text);
            p.ProductId = int.Parse(ProductComboBox.SelectedValue.ToString());

            if (Selected != null)
            {
                p.PreOrderId = Selected.PreOrderId;
                _preOrderservice.UpdateOrder(p);
                this.Close();
            }
            else
            {
                _preOrderservice.AddOrder(p);
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataComboBox();
            FillDataToUpdate(Selected);

            if (IsView == true)
            {
                SaveButton.IsEnabled = false;
                IdTextBox.IsReadOnly = true;
                NoTextBox.IsReadOnly = true;
                DepositTextBox.IsReadOnly = true;
                TotalTextBox.IsReadOnly = true;
                NameTextBox.IsReadOnly = true;
                AddressTextBox.IsReadOnly = true;
                PhoneTextBox.IsReadOnly = true;
                StatusTextBox.IsReadOnly = true;
                CreatedAtTextBox.IsReadOnly = true;
                UpdatedAtTextBox.IsReadOnly = true;
                ProductComboBox.IsEnabled = false;
            }
            else
            {
                SaveButton.IsEnabled = true;
            }
        }

        private void FillDataComboBox()
        {
            ProductComboBox.ItemsSource = _productService.GetAll();
            ProductComboBox.DisplayMemberPath = "ProductName";
            ProductComboBox.SelectedValuePath = "ProductId";
        }

        private void FillDataToUpdate(SamPreOrder p)
        {
            if (p == null) { return; }
            IdTextBox.Text = p.PreOrderId.ToString();
            IdTextBox.IsEnabled = false;

            NoTextBox.Text = p.PreOrderNo;
            DepositTextBox.Text = p.DepositAmount.ToString();
            TotalTextBox.Text = p.TotalAmount.ToString();
            NameTextBox.Text = p.CustomerName;
            AddressTextBox.Text = p.CustomerAddress;
            PhoneTextBox.Text = p.CustomerPhone;
            StatusTextBox.Text = p.Status;
            CreatedAtTextBox.Text = p.CreatedAt.ToString();
            UpdatedAtTextBox.Text = p.UpdatedAt.ToString();
            ProductComboBox.SelectedValue = p.ProductId;
        }
    }
}
