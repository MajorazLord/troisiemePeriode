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
using PointagePresencePdc.UserControl;
using PointagePresencePdc.ViewModel;

namespace PointagePresencePdc.View
{
    /// <summary>
    /// Logique d'interaction pour PageSelectPdc.xaml
    /// </summary>
    public partial class PageSelectPdc : Page
    {
        ManagerVM Mngr
        {
            get
            {
                return (Application.Current as App)?.ManagerGlobal;
            }
        }

        List<PosteChargeVM> allPdcs = new List<PosteChargeVM>();

        public PageSelectPdc()
        {
            InitializeComponent();

            var listT = Mngr.SelectedGroupes.ToList();

            listT.Sort((a, b) => string.Compare(a.IdGroupe, b.IdGroupe, StringComparison.Ordinal));

            foreach (GroupeVM grpVM in listT)
            {
                allPdcs.AddRange(grpVM.LesPosteChargeVMs);
            }

            lvPdcs.ItemsSource = allPdcs;
        }

        private void LvPdcs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //------------
                PosteChargeVM grp = (PosteChargeVM)e.AddedItems[0];
                ListViewItem lvi = (ListViewItem)lvPdcs.ItemContainerGenerator.ContainerFromItem(grp);
                ThreeStateToggleButton chkBx = FindVisualChild<ThreeStateToggleButton>(lvi);
                if (chkBx != null)
                {
                    chkBx.Dot_MouseLeftButtonDown(sender, null);
                    ListBoxItem item = ItemsControl.ContainerFromElement(lvPdcs, chkBx as DependencyObject) as ListBoxItem;

                    NotifyStatutToMngr(grp, chkBx.Toggled1);

                }           
            }
            else // Remove Select All chkBox
            {
                PosteChargeVM grp = (PosteChargeVM)e.RemovedItems[0];
                ListViewItem lvi = (ListViewItem)lvPdcs.ItemContainerGenerator.ContainerFromItem(grp);
                ThreeStateToggleButton chkBx = FindVisualChild<ThreeStateToggleButton>(lvi);
                if (chkBx != null)
                {
                    chkBx.Dot_MouseLeftButtonDown(sender, null);
                    NotifyStatutToMngr(grp, chkBx.Toggled1);
                    
                    /*chkBx.Toggled1 = false;
                    chkBx.Back.Fill = chkBx.Off;
                    chkBx.Dot.Margin = chkBx.LeftSide;
                    chkBx.Dot.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));*/
                    ListBoxItem item = ItemsControl.ContainerFromElement(lvPdcs, chkBx as DependencyObject) as ListBoxItem;
                    /*if (item != null)
                    {
                        item.IsSelected = false;
                    }*/
                }
            }
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child is ThreeStateToggleButton)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }

        private void BtnValidPdcs_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO check les elements pas saisie
            int _nbElemNoSaisie = CountElementNoSaisie();
            if (_nbElemNoSaisie != 0)
            {
                MessageBoxResult res = MessageBox.Show(String.Format("{0} éléments pas saisi, voulez-vous quand même terminer la saisie ?", _nbElemNoSaisie), "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                {
                    //TODO update dans la BDD
                    return;
                }
            }
            





        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void NotifyStatutToMngr(PosteChargeVM pcvm , ThreeStateToggleButton.Statut state)
        {
            int indexOfGroupeVm = Mngr.LesGroupeVMs.IndexOf(FindGroupeVmFromPdcVm(pcvm));
            int indexOfPdcVm = Mngr.LesGroupeVMs[indexOfGroupeVm].LesPosteChargeVMs.IndexOf(pcvm);
            Mngr.LesGroupeVMs[indexOfGroupeVm].LesPosteChargeVMs[indexOfPdcVm].Statut = state;
        }

        private GroupeVM FindGroupeVmFromPdcVm(PosteChargeVM pcvm)
        {
            foreach (GroupeVM groupeVm in Mngr.SelectedGroupes)
            {
                if (groupeVm.LesPosteChargeVMs.Contains(pcvm))
                {
                    return groupeVm;
                }
            }
            return null;
        }

        private int CountElementNoSaisie()
        {
            int compteur = 0;
            foreach (GroupeVM groupeVm in Mngr.SelectedGroupes)
            {
                foreach (PosteChargeVM posteChargeVm in groupeVm.LesPosteChargeVMs)
                {
                    if (posteChargeVm.Statut == ThreeStateToggleButton.Statut.NoPicked)
                    {
                        compteur++;
                    }
                }
            }
            return compteur;
        }
    }
}
