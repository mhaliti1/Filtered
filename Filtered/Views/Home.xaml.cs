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

namespace Filtered.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            this.ProjectList.SelectedItem = button.DataContext;

            Switcher.Switch(new Views.Details());


        }

        private void OpenFilter(object sender, RoutedEventArgs e)
        {
            Filter _Filter = new Filter();
            _Filter.DataContext =MainWindow.vm;
            _Filter.ShowDialog();
        }
    }
}
