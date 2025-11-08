using BLL.Services;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows;

namespace PE_PRN212_SU25_DoHoangGiaBao
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private MockTestService _mockTestService = new();
        private CandidateService _candidateService = new();
        public Jlptaccount? CurrentUser { get; set; } = null;
        public MockTest Selected { get; set; } = null;
        public Jlptaccount? CurrentAccount { get; set; } = null;
        public Boolean IsView = false;

        public DetailWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MockTest mt = new();

            if (TestIdTextBox.Text.IsNullOrEmpty() ||
                TitleTextBox.Text.IsNullOrEmpty() ||
                SkillAreaTextBox.Text.IsNullOrEmpty() ||
                StartTimeTextBox.Text.IsNullOrEmpty() ||
                EndTimeTextBox.Text.IsNullOrEmpty() ||
                CandidateComboBox.SelectedValue == null ||
                ScoreBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (TimeOnly.Parse(StartTimeTextBox.Text) >= TimeOnly.Parse(EndTimeTextBox.Text))
            {
                MessageBox.Show("Start Time must be earlier than End Time.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string title = TitleTextBox.Text;
            if (title.Length < 5 || title.Length > 150)
            {
                MessageBox.Show("TestTile must be between 5 and 150 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var regex = new Regex(@"^[a-zA-Z0-9 ]+$");
            if (!regex.IsMatch(title))
            {
                MessageBox.Show("TestTitle cannot contain special characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            float score = float.Parse(ScoreBox.Text);
            if (score < 0 || score > 180)
            {
                MessageBox.Show("Score must be higher than 0 and less than 180", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mt.TestId = int.Parse(TestIdTextBox.Text);
            mt.TestTitle = TitleTextBox.Text;
            mt.SkillArea = SkillAreaTextBox.Text;
            mt.StartTime = TimeOnly.Parse(StartTimeTextBox.Text);
            mt.EndTime = TimeOnly.Parse(EndTimeTextBox.Text);
            mt.CandidateId = int.Parse(CandidateComboBox.SelectedValue.ToString());
            mt.Score = float.Parse(ScoreBox.Text);

            if (Selected != null)
            {
                mt.TestId = Selected.TestId;
                _mockTestService.UpdateMockTest(mt);
                this.Close();
            }
            else
            {
                _mockTestService.AddMockTest(mt);
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
            if (Selected != null)
            {
                DetailPageLabel.Content = "VIEW | UPDATE MOCK TEST";
            }
            else
            {
                DetailPageLabel.Content = "CREATE NEW MOCK TEST";
            }

            if (CurrentAccount.Role == 3 || IsView == true)
            {
                SaveButton.IsEnabled = false;
                TitleTextBox.IsReadOnly = true;
                SkillAreaTextBox.IsReadOnly = true;
                StartTimeTextBox.IsReadOnly = true;
                EndTimeTextBox.IsReadOnly = true;
                CandidateComboBox.IsEnabled = false;
                ScoreBox.IsReadOnly = true;
            } else
            {
                SaveButton.IsEnabled = true;
            }
        }

        private void FillDataComboBox()
        {
            CandidateComboBox.ItemsSource = _candidateService.GetAll();
            CandidateComboBox.DisplayMemberPath = "FullName";
            CandidateComboBox.SelectedValuePath = "CandidateId";
        }

        private void FillDataToUpdate(MockTest mt)
        {
            if (mt == null) { return; }
            TestIdTextBox.Text = mt.TestId.ToString();
            TestIdTextBox.IsEnabled = false;

            TitleTextBox.Text = mt.TestTitle;
            SkillAreaTextBox.Text = mt.SkillArea;
            StartTimeTextBox.Text = mt.StartTime.ToString();
            EndTimeTextBox.Text = mt.EndTime.ToString();
            CandidateComboBox.SelectedValue = mt.CandidateId;
            ScoreBox.Text = mt.Score.ToString();
        }
    }
}
