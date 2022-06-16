using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindow MainWindowContext;
        public int CurrentUid;
        public MainPage(MainWindow window, int currentUID)
        {
            InitializeComponent();
            MainWindowContext = window;
            CurrentUid = currentUID;
        }

        enum GenreEnum
        {
            All,
            Horror,
            SciFi,
            Documentary,
            Romcom,
            Thriller,
            Comedy,
            Romance,
            Action
        }

        private async void OnMainPageLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_firstname.Content = (await RestHelper.GetClientWithIDAsync(CurrentUid)).firstname;
                cb_Genre.ItemsSource = Enum.GetValues(typeof(GenreEnum));
                cb_Genre.SelectedIndex = 0;
                tb_SearchMovie.Text = "Search...";
                foreach (Movie m in await RestHelper.GetAllMoviesAsync())
                {
                    lb_Movies.Items.Add(m);
                } 
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private async void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.MainFrame.Content = new UserSettingsPage(MainWindowContext, CurrentUid); 

        }

        private async void TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lb_Movies.Items.Clear();

                if (tb_SearchMovie.Text == "" || String.IsNullOrEmpty(tb_SearchMovie.Text) || tb_SearchMovie.Text == null && cb_Genre.Text != "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync("", cb_Genre.Text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }
                }

                else if (tb_SearchMovie.Text != "Search..." && cb_Genre.Text != "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(tb_SearchMovie.Text.ToString(),
                                 cb_Genre.Text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }
                else if (tb_SearchMovie.Text == "Search..." && cb_Genre.Text == "All")
                {
                    foreach (Movie m in await RestHelper.GetAllMoviesAsync())
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }
                else if (tb_SearchMovie.Text == "Search..." && cb_Genre.Text != "All")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync("", cb_Genre.Text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }

                else if (tb_SearchMovie.Text == "" ||
                         String.IsNullOrWhiteSpace(tb_SearchMovie.Text) && cb_Genre.Text != "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(title: "",
                                 genre: cb_Genre.Text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void Searchbox_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_SearchMovie.Text = "";
        }

        private async void GenreBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                var meow = (GenreEnum)cb_Genre.SelectedItem;
                string text = meow.ToString();
                lb_Movies.Items.Clear();

                if (tb_SearchMovie.Text != "Search..." && text != "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(tb_SearchMovie.Text.ToString(), text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }
                else if (tb_SearchMovie.Text == "Search..." && text == "All")
                {
                    foreach (Movie m in await RestHelper.GetAllMoviesAsync())
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }
                else if (tb_SearchMovie.Text == "Search..." && text != "All")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync("", text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }

                else if (tb_SearchMovie.Text == "" && text != "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync("", text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btn_Subscriptions_Click(object sender, RoutedEventArgs e)
        {
            MainWindowContext.MainFrame.Content = new SettingsPage(MainWindowContext, CurrentUid);

        }

        private void btn_watch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
