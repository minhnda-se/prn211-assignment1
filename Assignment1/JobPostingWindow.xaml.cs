using BusinessObjects.Models;
using Repositories;
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

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for JobPostingWindow.xaml
    /// </summary>
    public partial class JobPostingWindow : Window
    {
        private readonly Hraccount hraccount;
        private int menuClick;
        private readonly IJobPostingRepository jobPostingRepository;
        public JobPostingWindow(Hraccount hraccount)
        {
            InitializeComponent();
            jobPostingRepository = new JobPostingRepository();
            this.hraccount = hraccount;
            DataContext = this;
            menuClick = 1;
            Window_Loaded();
        }

        private void Window_Loaded()
        {
            dtgJpbPosting.ItemsSource = jobPostingRepository.GetJobPostings().Select(jp => new { 
                jp.PostingId,
                jp.JobPostingTitle,
                jp.PostedDate,
            });
            RoleRoute();
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


        private void Select_Change(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row != null)
            {
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                if (cell != null)
                {
                    string id = ((TextBlock)cell.Content).Text;
                    JobPosting jp = jobPostingRepository.GetJobPosting(id);
                    if (jp != null)
                    {
                        SaveDataToWindow(jp);
                    }
                }
            }

        }
    

        private void SaveDataToWindow(JobPosting jp)
        {
            txtDescription.Text = jp.Description;
            txtPostingId.Text = jp.PostingId;
            txtTitle.Text = jp.JobPostingTitle;
            dpDate.SelectedDate = jp.PostedDate;
        }

        private JobPosting SaveDataToDb()
        {
            return new JobPosting
            {
                JobPostingTitle = txtTitle.Text,
                PostedDate = dpDate.SelectedDate,
                PostingId = txtPostingId.Text,
                Description = txtDescription.Text,
            };
        }

        private void ClearData()
        {
            txtPostingId.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtTitle.Text= string.Empty;
            dpDate.SelectedDate = null;

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

        private void CandidateProfile_Click(object sender, RoutedEventArgs e)
        {
            CandidateProfileWindow candidateProfileWindow = new CandidateProfileWindow(hraccount);
            candidateProfileWindow.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var jp = SaveDataToDb();
            if (jobPostingRepository.GetJobPosting(jp.PostingId) == null)
            {
                bool result = jobPostingRepository.CreateJobPosting(jp);
                if (result)
                {
                    Window_Loaded();
                    MessageBox.Show("Create successfully!!");
                }
                else
                {
                    MessageBox.Show("Create failed!!");
                }
            }
            else
            {
                MessageBox.Show("Job is exist!!");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var jp = SaveDataToDb();
            if (jobPostingRepository.GetJobPosting(jp.PostingId) != null)
            {
                bool result = jobPostingRepository.UpdateJobPosting(jp);
                if (result)
                {
                    Window_Loaded();
                    MessageBox.Show("Update successfully!!");
                    
                }
                else
                {
                    MessageBox.Show("Update failed!!");
                }
            }
            else
            {
                MessageBox.Show("No job available!!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool result = jobPostingRepository.DeleteJobPosting(txtPostingId.Text);
            if (result)
            {
                Window_Loaded();
                ClearData();
                MessageBox.Show("Delete successfully!!");
            }
            else
            {
                MessageBox.Show("Delete failed!!");
            }
        }
    }
}
