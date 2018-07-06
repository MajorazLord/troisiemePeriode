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
    /// Logique d'interaction pour PageSelectPdc.xaml
    /// </summary>
    public partial class PageSelectPdc : Page
    {
        List<PosteChargeVM> allPdcs = new List<PosteChargeVM>();

        public PageSelectPdc()
        {
            InitializeComponent();
        }

        public PageSelectPdc(List<GroupeVM> selectedGroupe)
        {
            InitializeComponent();
            
            selectedGroupe.Sort((a, b) => string.Compare(a.IdGroupe, b.IdGroupe, StringComparison.Ordinal));

            foreach (GroupeVM grpVM in selectedGroupe)
            {
                allPdcs.AddRange(grpVM.LesPosteChargeVMs);
            }

            lvPdcs.ItemsSource = allPdcs;
        }

        private void LvPdcs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnValidPdcs_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO check les elements pas saisie

        }
    }
}
