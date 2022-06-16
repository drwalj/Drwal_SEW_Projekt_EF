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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VodLib.models;

namespace WPF_Client
{
    /// <summary>
    /// Interaktionslogik für UserSettingsPage.xaml
    /// </summary>
    public partial class UserSettingsPage : Page
    {
        public MainWindow MainWindowContext;
        public int CurrentUid;
        public UserSettingsPage(MainWindow window, int currentUID)
        {
            InitializeComponent();
            CurrentUid = currentUID;
            MainWindowContext = window;
        }
        private async void UserSettings_OnLoaded(object sender, RoutedEventArgs e)
        {
            Client suspect = await RestHelper.GetClientWithIDAsync(CurrentUid);
            tb_firstname.Content = (await RestHelper.GetClientWithIDAsync(CurrentUid)).firstname;
            tb_Adresse.Text = suspect.address;
            tb_PLZ.Text = suspect.postalcode;
        }

        private void btn_Main_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.MainFrame.Content = new MainPage(MainWindowContext, CurrentUid);

        }

        /*private async void btn_Delete_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to permanently delete your Account?", "Pls dont", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var result = await RestHelper.DeleteAccountAsync(CurrentUid);
                if (result)
                    MainWindowContext.MainFrame.Content = new LoginPage(MainWindowContext);
            }

        }*/

        private async void btn_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            Client myClient = new Client();
            myClient.address = tb_Adresse.Text;
            myClient.postalcode = tb_PLZ.Text;
            if (dp_Birthdate.SelectedDate == null)
            {
                myClient.dateofbirth = (await RestHelper.GetClientWithIDAsync(CurrentUid)).dateofbirth;
            }
            else
            {
                myClient.dateofbirth = dp_Birthdate.SelectedDate;
            }
            if (await RestHelper.PatchClientAsync(myClient, CurrentUid))
            {
                MessageBox.Show("Changes Saved!");
            }
            else
            {
                MessageBox.Show("Failed!");

            }
            
        }


    }
}
