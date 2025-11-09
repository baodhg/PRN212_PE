using BLL.Services;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PerfumeManagement_SE193119
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private readonly PerfumeInfomationService _perfumeService = new();
        private readonly ProductionCompanyService _companyService = new();
        public Psaccount? CurrentAccount { get; set; } = null;
        public bool IsView { get; set; } = false;
        public PerfumeInformation? Selected { get; set; } = null;

        public DetailWindow()
        {
            InitializeComponent();
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
                NameTextBox.IsReadOnly = true;
                IngredientsTextBox.IsReadOnly = true;
                LongevityTextBox.IsReadOnly = true;
                ReleaseDateTextBox.IsReadOnly = true;
                ConcentrationTextBox.IsReadOnly = true;
                CompanyComboBox.IsEnabled = false;
            }
            else
            {
                SaveButton.IsEnabled = true;
            }
        }

        private void FillDataComboBox()
        {
            CompanyComboBox.ItemsSource = _companyService.GetAll();
            CompanyComboBox.DisplayMemberPath = "ProductionCompanyName";
            CompanyComboBox.SelectedValuePath = "ProductionCompanyId";
        }

        private void FillDataToUpdate(PerfumeInformation p)
        {
            if (p == null) { return; }
            IdTextBox.Text = p.PerfumeId.ToString();
            IdTextBox.IsEnabled = false;

            NameTextBox.Text = p.PerfumeName.ToString();
            IngredientsTextBox.Text = p.Ingredients.ToString();
            ReleaseDateTextBox.Text = p.ReleaseDate.ToString();
            LongevityTextBox.Text = p.Longevity.ToString();
            ConcentrationTextBox.Text = p.Concentration.ToString();
            CompanyComboBox.SelectedValue = p.ProductionCompanyId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PerfumeInformation p = new();

            if (IdTextBox.Text.IsNullOrEmpty() ||
                NameTextBox.Text.IsNullOrEmpty() ||
                IngredientsTextBox.Text.IsNullOrEmpty() ||
                ReleaseDateTextBox.Text.IsNullOrEmpty() ||
                ConcentrationTextBox.Text.IsNullOrEmpty() ||
                LongevityTextBox.Text.IsNullOrEmpty() ||
                CompanyComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var IdRegex = new Regex(@"^[a-zA-Z0-9]+$");
            if (!IdRegex.IsMatch(IdTextBox.Text))
            {
                MessageBox.Show("ID cannot contain special characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IdTextBox.Text.Length < 0 || IdTextBox.Text.Length > 30)
            {
                MessageBox.Show("ID must be a string without space.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NameTextBox.Text.Length < 5 || NameTextBox.Text.Length > 90)
            {
                MessageBox.Show("Customer Name must be between 1 and 50 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var NameRegex = new Regex(@"^[A-Z1-9][A-Za-z0-9]*( [A-Z1-9][A-Za-z0-9]*)*$");
            if (!NameRegex.IsMatch(NameTextBox.Text))
            {
                MessageBox.Show("Perfume Name cannot contain special characters and each word must be capitalized first letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateTime.TryParse(ReleaseDateTextBox.Text, out DateTime releaseDate) == false)
            {
                MessageBox.Show("Please enter valid dates in the format yyyy-dd-mm hh:mm:ss.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            p.PerfumeId = IdTextBox.Text;
            p.PerfumeName = NameTextBox.Text;
            p.Ingredients = IngredientsTextBox.Text;
            p.ReleaseDate = DateTime.Parse(ReleaseDateTextBox.Text);
            p.Concentration = ConcentrationTextBox.Text;
            p.Longevity = LongevityTextBox.Text;
            p.ProductionCompanyId = CompanyComboBox.SelectedValue.ToString();

            if (Selected != null)
            {
                p.PerfumeId = Selected.PerfumeId;
                _perfumeService.UpdatePerfume(p);
                this.Close();
            }
            else
            {
                _perfumeService.AddPerfume(p);
                this.Close();
            }
        }
    }
}
