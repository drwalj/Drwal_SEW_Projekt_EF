using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
            tbUserId.Text = currentUID.ToString();

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
            foreach (Movie m in await RestHelper.GetAllMoviesAsync())
            {
                lb_Movies.Items.Add(m.ToString());
            }
            cb_Genre.ItemsSource = Enum.GetValues(typeof(GenreEnum));
            cb_Genre.SelectedIndex = 0;
            tb_SearchMovie.Text = "Search...";
        }

        private async void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            if (tb_SearchMovie.Text != "" && tb_SearchMovie.Text != "Search...")
            {
                lb_Movies.Items.Clear();
                foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(tb_SearchMovie.Text.ToString(),cb_Genre.Text))
                {
                    lb_Movies.Items.Add(m.ToString());
                }
            }
        }

        private async void TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lb_Movies.Items.Clear();

                if (tb_SearchMovie.Text != "Search...")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(tb_SearchMovie.Text.ToString(), cb_Genre.Text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }

                else if (tb_SearchMovie.Text == "Search..." || tb_SearchMovie.Text == "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(title: "", genre: cb_Genre.Text))
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
                lb_Movies.Items.Clear();

                if (tb_SearchMovie.Text != "Search...")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(tb_SearchMovie.Text.ToString(), cb_Genre.Text))
                    {
                        lb_Movies.Items.Add(m.ToString());
                    }

                }

                else if (tb_SearchMovie.Text == "Search..." || tb_SearchMovie.Text == "")
                {
                    foreach (Movie m in await RestHelper.GetMoviesWithGenreAndTitleAsync(title:"",genre: cb_Genre.Text))
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
    }
}
