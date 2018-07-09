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
using PointagePresencePdc.ViewModel;

namespace PointagePresencePdc.View
{
    /// <summary>
    /// Logique d'interaction pour PageConnection.xaml
    /// </summary>
    public partial class PageConnection : Page
    {
        ManagerVM Mngr
        {
            get
            {
                return (Application.Current as App)?.ManagerGlobal;
            }
        }
        public PageConnection()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new PageSelectSecteur());
        }

        private void Btn_Conenction_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tb_Pointage.Text, out var pointage) && pointage >= 1 && pointage <= 9999)
            {
                Mngr.NoPointage = pointage;
                NavigationService?.Navigate(new PageSelectSecteur());
            }
            else
            {
                MessageBox.Show("Merci de vérifier votre numéro de pointage", "Erreur Pointage", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
