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
    /// Interaktionslogik für SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public MainWindow MainWindowContext;
        public int CurrentUid;
        public string SelectedPlan ="";
        public SettingsPage(MainWindow window, int currentUID)
        {
            InitializeComponent();
            CurrentUid = currentUID;
            MainWindowContext = window;

        }

        private async void OnMainPageLoaded(object sender, RoutedEventArgs e)
        {
            tb_firstname.Content = (await RestHelper.GetClientWithIDAsync(CurrentUid)).firstname;
            cb_Payment.ItemsSource = await RestHelper.GetAllPaymentMethodsAsync();
            cb_Payment.SelectedIndex = 1;
            cb_Delivery.ItemsSource = await RestHelper.GetAllDeliveryMethodsAsync();
            cb_Delivery.SelectedIndex = 1;

        }


        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.MainFrame.Content = new MainPage(MainWindowContext, CurrentUid);
        }

        private async void btn_buy_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPlan == "")
                MessageBox.Show("Please Select A Subsciption by clicking on the image");
            Order myOrder = new Order();
            if (SelectedPlan == "Standard") { myOrder.subscription_id = 1; }
            if (SelectedPlan == "Mega") { myOrder.subscription_id = 2; }
            if (SelectedPlan == "Ultra") { myOrder.subscription_id = 3; }
            myOrder.shipment = cb_Delivery.SelectedItem.ToString();
            myOrder.payment = cb_Delivery.SelectedItem.ToString();
            myOrder.client_id = CurrentUid;

            if (await RestHelper.PostNewOrderAsync(myOrder, CurrentUid))
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Failed!");

            }

        }

        private void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.MainFrame.Content = new UserSettingsPage(MainWindowContext, CurrentUid);

        }

        private void btn_Standard_Click(object sender, RoutedEventArgs e)
        {
            SelectedPlan = "Standard";
            tb_standard.Foreground = new SolidColorBrush(Colors.Red);
            tb_ultra.Foreground = new SolidColorBrush(Colors.White);
            tb_mega.Foreground = new SolidColorBrush(Colors.White);

        }

        private void btn_Mega_Click(object sender, RoutedEventArgs e)
        {
            SelectedPlan = "Mega";
            tb_standard.Foreground = new SolidColorBrush(Colors.White);
            tb_ultra.Foreground = new SolidColorBrush(Colors.White);
            tb_mega.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void btn_Ultra_Click(object sender, RoutedEventArgs e)
        {
            SelectedPlan = "Ultra";
            tb_standard.Foreground = new SolidColorBrush(Colors.White);
            tb_ultra.Foreground = new SolidColorBrush(Colors.Red);
            tb_mega.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
