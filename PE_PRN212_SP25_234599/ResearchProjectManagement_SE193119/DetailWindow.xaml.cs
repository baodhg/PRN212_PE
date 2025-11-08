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

namespace ResearchProjectManagement_SE193119
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private ResearcherService _researcherService = new();
        private ResearchProjectService _researchProjectService = new();
        public UserAccount? CurrentAccount { get; set; } = null;
        public ResearchProject? Selected { get; set; } = null;
        public Boolean IsView = false;

        public DetailWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ResearchProject rp = new();

            if (IdTextBox.Text.IsNullOrEmpty() ||
                TitleTextBox.Text.IsNullOrEmpty() ||
                FieldTextBox.Text.IsNullOrEmpty() ||
                StartDateTextBox.Text.IsNullOrEmpty() ||
                EndDateTextBox.Text.IsNullOrEmpty() ||
                ResearcherComboBox.SelectedValue == null ||
                BudgetTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (int.TryParse(IdTextBox.Text, out int id) == false)
            {
                MessageBox.Show("ID must be an integer number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string title = TitleTextBox.Text;
            if (title.Length < 5 || title.Length > 100)
            {
                MessageBox.Show("Tile must be between 5 and 100 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var regex = new Regex(@"^[A-Z1-9][A-Za-z0-9]*( [A-Z1-9][A-Za-z0-9]*)*$");
            if (!regex.IsMatch(title))
            {
                MessageBox.Show("Title cannot contain special characters and must follow format: [Xin Chao]", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateOnly.TryParse(StartDateTextBox.Text, out DateOnly startDate) == false ||
                DateOnly.TryParse(EndDateTextBox.Text, out DateOnly endDate) == false)
            {
                MessageBox.Show("Please enter valid dates in the format MM/DD/YYYY.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateOnly.Parse(StartDateTextBox.Text) >= DateOnly.Parse(StartDateTextBox.Text))
            {
                MessageBox.Show("Start Date must be earlier than End Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!float.TryParse(BudgetTextBox.Text, out float budget))
            {
                MessageBox.Show("Budget must be a decimal number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (budget < 0)
            {
                MessageBox.Show("Budget must be higher than 0 and less than 180", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            rp.ProjectId = int.Parse(IdTextBox.Text);
            rp.ProjectTitle = TitleTextBox.Text;
            rp.ResearchField = FieldTextBox.Text;
            rp.StartDate = DateOnly.Parse(StartDateTextBox.Text);
            rp.EndDate = DateOnly.Parse(StartDateTextBox.Text);
            rp.LeadResearcherId = int.Parse(ResearcherComboBox.SelectedValue.ToString());
            rp.Budget = decimal.Parse(BudgetTextBox.Text);

            if (Selected != null)
            {
                rp.ProjectId = Selected.ProjectId;
                _researchProjectService.UpdateProject(rp);
                this.Close();
            }
            else
            {
                _researchProjectService.AddProject(rp);
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataComboBox();
            FillDataToUpdate(Selected);

            if (CurrentAccount.Role == 3 || IsView == true)
            {
                SaveButton.IsEnabled = false;
                TitleTextBox.IsReadOnly = true;
                FieldTextBox.IsReadOnly = true;
                StartDateTextBox.IsReadOnly = true;
                EndDateTextBox.IsReadOnly = true;
                ResearcherComboBox.IsEnabled = false;
                BudgetTextBox.IsReadOnly = true;
            }
            else
            {
                SaveButton.IsEnabled = true;
            }
        }

        private void FillDataComboBox()
        {
            ResearcherComboBox.ItemsSource = _researcherService.GetAll();
            ResearcherComboBox.DisplayMemberPath = "FullName";
            ResearcherComboBox.SelectedValuePath = "ResearcherId";
        }

        private void FillDataToUpdate(ResearchProject rp)
        {
            if (rp == null) { return; }
            IdTextBox.Text = rp.ProjectId.ToString();
            IdTextBox.IsEnabled = false;

            TitleTextBox.Text = rp.ProjectTitle;
            FieldTextBox.Text = rp.ResearchField;
            StartDateTextBox.Text = rp.StartDate.ToString();
            EndDateTextBox.Text = rp.EndDate.ToString();
            ResearcherComboBox.SelectedValue = rp.LeadResearcherId;
            BudgetTextBox.Text = rp.Budget.ToString();
        }
    }
}
