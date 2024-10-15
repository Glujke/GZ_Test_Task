using GZ_Test_WPF_Application.Api;
using GZ_Test_WPF_Application.Models;
using GZ_Test_WPF_Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GZ_Test_WPF_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string URL_API = "http://localhost:5000";

        public ObservableCollection<Specialization> Specializations { get; set; }
        public ObservableCollection<Area> Areas { get; set; }
        public ObservableCollection<Cabinet> Cabinets { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            using (var httpClient = new HttpApi<Specialization>(URL_API))
            {
                Specializations = httpClient.GetItems();
                comboBoxSpecs.ItemsSource = Specializations;
            }
            using (var httpClient = new HttpApi<Area>(URL_API))
            {
                Areas = httpClient.GetItems();
                comboBoxAreas.ItemsSource = Areas;
            }
            using (var httpClient = new HttpApi<Cabinet>(URL_API))
            {
                Cabinets = httpClient.GetItems();
                comboBoxCabinet.ItemsSource = Cabinets;
            }

            DataContext = new AppViewModel();
        }

        private void comboBoxSpecs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var send = e.AddedItems[0] as Specialization;

        }
    }
}
