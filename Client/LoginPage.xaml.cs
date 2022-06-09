using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
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
using WPF_Client.Annotations;

namespace WPF_Client
{
    /// <summary>
    /// Interaktionslogik für LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public MainWindow MainWindowContext;
        public LoginPage(MainWindow window)
        {
            InitializeComponent();
            MainWindowContext = window;

        }


        private async void Einloggen_Button(object sender, RoutedEventArgs e)
        {
            //einloggdaten sind firstname+lastname da ich keine passwörter in meiner datenbank speicher aber ich ein einlogg- feature machen wollte
            var result = await RestHelper.GetClientWithNameAsync(tbFirstname.Text, tbLastname.Text);
            if (result.client_id != -404)
            {
                MainWindowContext.MainFrame.Content = new MainPage(MainWindowContext, result.client_id); // ich weiß, ich könnte den Client direkt einfach mitgeben, aber um REST kenntnisse zu beweisen, hole ich mir die Clientdaten dann durch die Id wenn ich sie brauche B)
            }
            else
            {
                MessageBox.Show("Firstname or Lastname wrong!");
            }

        }

        private async void Registrieren_Button(object sender, RoutedEventArgs e)
        {
            var result = await RestHelper.PostNewClientAsync(tbFirstname.Text, tbLastname.Text);

            if (result == "-404")
            {
                Console.WriteLine("Fehler beim Posten neues Clients");
            }

            else
            {
                Console.WriteLine("Successfully added new Client");
                MainWindowContext.MainFrame.Content = new MainPage(MainWindowContext, Int32.Parse(result));

            }

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            using (Process compiler = new Process())
            {
                compiler.StartInfo.FileName = e.Uri.AbsoluteUri;
                compiler.StartInfo.UseShellExecute = true;
                e.Handled = true;
                compiler.Start();
            }

        }
        
        private void tbFirstname_Focus(object sender, RoutedEventArgs e)
        {
            if (tbFirstname.Text=="Firstname")
            {
                tbFirstname.Text = String.Empty;
            }
        }
        
        private void tbLastname_Focus(object sender, RoutedEventArgs e)
        {
            if (tbLastname.Text == "Lastname")
            {
                tbLastname.Text = String.Empty;
            }

        }




    }
}
