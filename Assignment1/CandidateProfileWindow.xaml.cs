using BusinessObjects.Models;
using DAOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for CandidateProfileWindow.xaml
    /// </summary>
    public partial class CandidateProfileWindow : Window
    {
        private int menuClick;
        private readonly Hraccount hraccount;
        private readonly ICandidateProfileRepository profileRepository;
        private readonly IJobPostingRepository jobPostingRepository;
        public CandidateProfileWindow(Hraccount hraccount)
        {
            InitializeComponent();
            profileRepository = new CandidateProfileRepository();
            jobPostingRepository = new JobPostingRepository();
            DataContext = this;
            menuClick = 1;
            this.hraccount = hraccount;
            Window_Loaded();
        }


        private void Window_Loaded()
        {
            RoleRoute();
            dtgProfile.ItemsSource = profileRepository.GetCandidates().Select(cp => new
            {
                cp.CandidateId,
                cp.Fullname,
                cp.Posting.JobPostingTitle,
                cp.ProfileShortDescription
                
            });
            cbJobPosting.ItemsSource = jobPostingRepository.GetJobPostings();
            cbJobPosting.DisplayMemberPath = "JobPostingTitle";
            cbJobPosting.SelectedValuePath = "PostingId";
        }

        private void Select_Change(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow) dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row != null)
            {
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                if (cell != null)
                {
                    string id = ((TextBlock)cell.Content).Text;
                    CandidateProfile cp = profileRepository.GetCandidate(id);
                    if (cp != null)
                    {
                        SaveProfileToWindow(cp);
                    }
                }
            }
            
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (menuClick == 0)
            {
                MenuBorder.Visibility = Visibility.Collapsed;
                menuClick = 1;
            }
            else
            {
                MenuBorder.Visibility = Visibility.Visible;
                menuClick = 0;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addProfile = SaveProfileToDb();
            if (profileRepository.GetCandidate(addProfile.CandidateId) == null)
            {
                bool result = profileRepository.CreateCandidateProfile(addProfile);
                if (result)
                {
                    SaveProfileToWindow(addProfile);
                    Window_Loaded();
                    MessageBox.Show("Add successfully!!");
                   
                }
                else
                {
                    MessageBox.Show("Add failed. Please try again!");
                }
            }
            else
            {
                MessageBox.Show("Profile's exist!!");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var updateProfile = SaveProfileToDb();
            if (profileRepository.GetCandidate(updateProfile.CandidateId) != null)
            {
                bool result = profileRepository.UpdateCandidateProfile(updateProfile);
                if (result)
                {
                    Window_Loaded();
                    SaveProfileToWindow(updateProfile);
                    MessageBox.Show("Update successfully!!");
                    
                }
                else
                {
                    MessageBox.Show("Update failed. Please try again!");
                }
            }
            else
            {
                MessageBox.Show("No profile available!!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var deleteProfile = profileRepository.GetCandidate(txtCandidateId.Text);
            if (deleteProfile != null)
            {
                bool result = profileRepository.DeleteCandidateProfile(txtCandidateId.Text);
                if (result )
                {
                    ClearProfileData();
                    Window_Loaded();
                    MessageBox.Show("Delete successfully!!");
                }
                else
                {
                    MessageBox.Show("Delete failed!!");
                }
            }
            else
            {
                MessageBox.Show("No profile available!!");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoleRoute()
        {
            switch (hraccount.MemberRole)
            {
                //Case 1: admin
                //Case 2: Manager
                //Case 3: Staff
                // Staff can only read data
                case 3:
                    btnAdd.IsEnabled = false;
                    btnUpdate.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                    break;
            }
        }

        private void JobPosting_Click(object sender, RoutedEventArgs e)
        {
            JobPostingWindow jobPostingWindow = new JobPostingWindow(hraccount);
            jobPostingWindow.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private CandidateProfile SaveProfileToDb()
        {
            var profile = new CandidateProfile {
                CandidateId = txtCandidateId.Text,
                Fullname = txtFullName.Text,
                ProfileUrl = txtImageUrl.Text,
                Birthday = dpBirthDay.SelectedDate,
                PostingId = cbJobPosting.SelectedValue.ToString(),
                ProfileShortDescription = txtDescription.Text
            };
            return profile;
        }
        private void SaveProfileToWindow(CandidateProfile profile)
        {
            txtCandidateId.Text = profile.CandidateId;
            txtDescription.Text = profile.ProfileShortDescription;
            txtFullName.Text = profile.Fullname;
            txtImageUrl.Text = profile.ProfileUrl;
            cbJobPosting.SelectedValue = profile.PostingId;
            dpBirthDay.SelectedDate = profile.Birthday;
        }
        private void ClearProfileData()
        {
            txtCandidateId.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtImageUrl.Text = string.Empty;
            cbJobPosting.SelectedValue = string.Empty;
            dpBirthDay.SelectedDate = null;
        }
    }
}
