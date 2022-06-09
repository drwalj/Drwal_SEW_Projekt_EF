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



    }
}
