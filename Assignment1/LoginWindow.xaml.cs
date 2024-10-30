using BusinessObjects.Models;
using Microsoft.IdentityModel.Tokens;
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
    
    public partial class LoginWindow : Window
    {
        private readonly IHraccountRepository hraccountRepository;
        public LoginWindow()
        {
            InitializeComponent();
            hraccountRepository = new HraccountRepository();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Hraccount account = hraccountRepository.GetHrAccount(txtEmail.Text);
            if (account != null && !txtPassword.Password.IsNullOrEmpty() && txtPassword.Password.Equals(account.Password))
            {
                CandidateProfileWindow candidateProfileWindow = new CandidateProfileWindow(account);
                candidateProfileWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Email or Password!!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
