using MySql.Data.MySqlClient;
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

namespace databeaseDefault
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Service db = new Service();
        public MainWindow()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                dgTagok.ItemsSource = db.GetCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadEntries()
        {
            try
            {
                dgBelepesek.ItemsSource = db.GetEntries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                dgStatisztika.ItemsSource = db.GetStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUjNev.Text) || dpSzuletes.SelectedDate == null)
                MessageBox.Show("Érvénytelen adatok!");
            else
                try
                {
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mentési hiba: " + ex.Message);
                }
        }

        private void tcMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tcMenu.SelectedIndex == 0)
            {
                LoadCustomers();
            }
            else if(tcMenu.SelectedIndex == 1)
            {
                LoadEntries();
            }
            else if(tcMenu.SelectedIndex == 2)
            {
                LoadStatistics();   
            }
        }
    }
}
