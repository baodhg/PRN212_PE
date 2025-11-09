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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChargingSessionManagement_SE193119
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public UserAccount? CurrentAccount { get; set; } = null;
        private VehicleService _vehicleService = new();
        private ChargingSessionService _sessionService = new();
        public bool IsView = false;

        public ChargingSession? Selected { get; set; } = null;

        public DetailWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ChargingSession s = new();

            if (IdTextBox.Text.IsNullOrEmpty() ||
                TitleTextBox.Text.IsNullOrEmpty() ||
                StationTextBox.Text.IsNullOrEmpty() ||
                BeginTextBox.Text.IsNullOrEmpty() ||
                FinishTextBox.Text.IsNullOrEmpty() ||
                VehicleComboBox.SelectedValue == null ||
                SessionFeeBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string title = TitleTextBox.Text;
            if (title.Length < 5 || title.Length > 200)
            {
                MessageBox.Show("Title must be between 5 and 200 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TimeOnly.Parse(BeginTextBox.Text) >= TimeOnly.Parse(FinishTextBox.Text))
            {
                MessageBox.Show("Begin Time must be earlier than Finish Time.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var regex = new Regex(@"^[a-zA-Z0-9 ]+$");
            if (!regex.IsMatch(title))
            {
                MessageBox.Show("Title cannot contain special characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!float.TryParse(SessionFeeBox.Text, out float fee))
            {
                MessageBox.Show("Fee must be a decimal number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (fee < 0)
            {
                MessageBox.Show("Fee must be higher than 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            s.SessionId = int.Parse(IdTextBox.Text);
            s.SessionTitle = TitleTextBox.Text;
            s.ChargingStation = StationTextBox.Text;
            s.BeginTime = TimeOnly.Parse(BeginTextBox.Text);
            s.FinishTime = TimeOnly.Parse(FinishTextBox.Text);
            s.VehicleId = int.Parse(VehicleComboBox.SelectedValue.ToString());
            s.ChargingFee = decimal.Parse(SessionFeeBox.Text);

            if (Selected != null)
            {
                s.SessionId = Selected.SessionId;
                _sessionService.UpdateSession(s);
                this.Close();
            }
            else
            {
                _sessionService.AddSession(s);
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

            if (CurrentAccount.Role == 3 || IsView == true)
            {
                SaveButton.IsEnabled = false;
                TitleTextBox.IsReadOnly = true;
                StationTextBox.IsReadOnly = true;
                BeginTextBox.IsReadOnly = true;
                FinishTextBox.IsReadOnly = true;
                VehicleComboBox.IsEnabled = false;
                SessionFeeBox.IsReadOnly = true;
            }
            else
            {
                SaveButton.IsEnabled = true;
            }
        }
        private void FillDataComboBox()
        {
            VehicleComboBox.ItemsSource = _vehicleService.GetAll();
            VehicleComboBox.DisplayMemberPath = "ModelName";
            VehicleComboBox.SelectedValuePath = "VehicleId";
        }

        private void FillDataToUpdate(ChargingSession s)
        {
            if (s == null) { return; }
            IdTextBox.Text = s.SessionId.ToString();
            IdTextBox.IsEnabled = false;

            TitleTextBox.Text = s.SessionTitle;
            StationTextBox.Text = s.ChargingStation;
            BeginTextBox.Text = s.BeginTime.ToString();
            FinishTextBox.Text = s.FinishTime.ToString();
            VehicleComboBox.SelectedValue = s.VehicleId;
            SessionFeeBox.Text = s.ChargingFee.ToString();
        }
    }
}
