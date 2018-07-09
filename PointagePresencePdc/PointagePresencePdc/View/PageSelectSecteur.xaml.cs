using System;
using System.Collections;
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
using PointagePresencePdc.UserControl;
using PointagePresencePdc.ViewModel;

namespace PointagePresencePdc.View
{
    /// <summary>
    /// Logique d'interaction pour Page2.xaml
    /// </summary>
    public partial class PageSelectSecteur : Page
    {
        ManagerVM Mngr
        {
            get
            {
                return (Application.Current as App)?.ManagerGlobal;
            }
        }

        private static bool IndividualChkBxUnCheckedFlag { get; set; }

        public PageSelectSecteur()
        {
            InitializeComponent();
            lstgrd.ItemsSource = Mngr.LesGroupeVMs;

            /*if (Mngr.SelectedGroupes.Count() != 0)
            {
                foreach (GroupeVM groupeVm in Mngr.SelectedGroupes)
                {
                    ListViewItem lvi = (ListViewItem)lstgrd.ItemContainerGenerator.ContainerFromItem(groupeVm);
                    //CheckBox chkBx = FindVisualChild<CheckBox>(lvi);
                    ToggleButton chkBx = FindVisualChild<ToggleButton>(lvi);
                }
            }*/
            /*chkWspSelectAll.On = new SolidColorBrush(Color.FromRgb(0, 209, 24));
            chkWspSelectAll.Off = new SolidColorBrush(Color.FromRgb(70,70,70));*/
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Lstgrd_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //------------
                GroupeVM grp = (GroupeVM)e.AddedItems[0];
                ListViewItem lvi = (ListViewItem)lstgrd.ItemContainerGenerator.ContainerFromItem(grp);
                //CheckBox chkBx = FindVisualChild<CheckBox>(lvi);
                ToggleButton chkBx = FindVisualChild<ToggleButton>(lvi);
                if (chkBx != null)
                {
                    chkBx.Toggled1 = true;
                    chkBx.Back.Fill = chkBx.On;
                    chkBx.Dot.Margin = chkBx.RightSide;
                    chkBx.Dot.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
                    ListBoxItem item = ItemsControl.ContainerFromElement(lstgrd, chkBx as DependencyObject) as ListBoxItem;
                    if (item != null)
                    {
                        item.IsSelected = true;
                    }
                }

                //------------               
            }
            else // Remove Select All chkBox
            {
                GroupeVM grp = (GroupeVM)e.RemovedItems[0];
                ListViewItem lvi = (ListViewItem)lstgrd.ItemContainerGenerator.ContainerFromItem(grp);
                ToggleButton chkBx = FindVisualChild<ToggleButton>(lvi);
                if (chkBx != null)
                {
                    chkBx.Toggled1 = false;
                    chkBx.Back.Fill = chkBx.Off;
                    chkBx.Dot.Margin = chkBx.LeftSide;
                    chkBx.Dot.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)); ;
                    ListBoxItem item = ItemsControl.ContainerFromElement(lstgrd, chkBx as DependencyObject) as ListBoxItem;
                    if (item != null)
                    {
                        item.IsSelected = false;
                    }
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
                    
                    if (child is ToggleButton)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }


        private void Lstgrd_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView cmd = (ListView)sender;
            //User selectedItem = (User)(((System.Data.DataRowView)(cmd.SelectedItem)).Row[0]);
            //CheckBox chk = myDataGrid.Columns[0].GetCellContent(e.Row) as CheckBox;
            //chk.IsChecked = false;
            //checkboxes.Add(chk);
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            //code here
        }

        public void ChkWspSelectAll_OnChecked(object sender, RoutedEventArgs e)
        {
            IndividualChkBxUnCheckedFlag = false;
            //=====================
            foreach (GroupeVM item in lstgrd.ItemsSource)
            {
                item.IsSelected = true;
                lstgrd.SelectedItems.Add(item);
            }
        }

        public void ChkWspSelectAll_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (!IndividualChkBxUnCheckedFlag)
            {
                foreach (GroupeVM grp in lstgrd.ItemsSource)
                {
                    grp.IsSelected = false;
                    //int itemIndex = items.IndexOf(item);
                    lstgrd.SelectedItems.Remove(grp);
                    //lstgrd.SelectedItems.Add(lstgrd.Items.GetItemAt(2));               
                }
            }
        }

        public void ChkWspSelect_OnChecked(object sender, RoutedEventArgs e)
        {
            var x = e.Source;
            var y = ((Ellipse)x).Parent;
            var z = ((Grid)y).Parent;
            var za = ((Viewbox)z).Parent;
            ListBoxItem item = ItemsControl.ContainerFromElement(lstgrd, za as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                item.IsSelected = true;
            }
        }

        public void ChkWspSelect_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var x = e.Source;
            var y = ((Ellipse)x).Parent;
            var z = ((Grid)y).Parent;
            var za = ((Viewbox)z).Parent;
            ListBoxItem item = ItemsControl.ContainerFromElement(lstgrd, za as DependencyObject) as ListBoxItem;
            if (item != null)
                item.IsSelected = false;

            IndividualChkBxUnCheckedFlag = true;
            ToggleButton headerChk = (ToggleButton)((GridView)lstgrd.View).Columns[0].Header;
            headerChk.Toggled1 = false;
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(lstgrd.SelectedItems.Count);

            ObservableCollection<GroupeVM> listTempo = new ObservableCollection<GroupeVM>();
            foreach (GroupeVM grpVM in lstgrd.SelectedItems)
            {
                listTempo.Add(grpVM);
            }

            Mngr.SelectedGroupes = listTempo;

            NavigationService?.Navigate(new PageSelectPdc());
        }

        private void PageSelectSecteur_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(Mngr.SelectedGroupes is null))
            {
                if (Mngr.SelectedGroupes.Count() != 0)
                {
                    foreach (GroupeVM mngrSelectedGroupe in Mngr.SelectedGroupes)
                    {
                        ListViewItem lvi =
                            (ListViewItem) lstgrd.ItemContainerGenerator.ContainerFromItem(mngrSelectedGroupe);
                        ToggleButton chkBx = FindVisualChild<ToggleButton>(lvi);
                        chkBx.Toggled1 = true;
                        chkBx.Back.Fill = chkBx.On;
                        chkBx.Dot.Margin = chkBx.RightSide;
                        ListBoxItem item = ItemsControl.ContainerFromElement(lstgrd, chkBx as DependencyObject) as ListBoxItem;
                        if (item != null)
                        {
                            item.IsSelected = true;
                        }
                    }
                }
            }
        }
    }
}

