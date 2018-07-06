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
using PointagePresencePdc.View;

namespace PointagePresencePdc.UserControl
{
    /// <summary>
    /// Logique d'interaction pour ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : System.Windows.Controls.UserControl
    {
        public Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        public Thickness RightSide = new Thickness(0, 0, -39, 0);
        public SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(70, 70, 70));
        public SolidColorBrush On = new SolidColorBrush(Color.FromRgb(0, 209, 24));
        private bool Toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        public bool Toggled1
        {
            get { return Toggled; }
            set
            {
                Toggled = value;

            }
        }

        public void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled1)//si off -> OnCheck
            {
                if (Name == "chkWspSelectAll")
                {
                    var parent1 = VisualTreeHelper.GetParent(this);
                    while (!(parent1 is PageSelectSecteur))
                    {
                        parent1 = VisualTreeHelper.GetParent(parent1);
                    }
                    (parent1 as PageSelectSecteur).ChkWspSelectAll_OnChecked(sender, e);
                }
                /*Back.Fill = On;
                Toggled1 = true;
                Dot.Margin = RightSide;
                Dot.Fill = new SolidColorBrush(Color.FromRgb(255,255,255));

                var parent = VisualTreeHelper.GetParent(this);
                while (!(parent is Page2))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                (parent as Page2).ChkWspSelect_OnChecked(sender, e);*/

            }
            else//si on -> OnUnChecked
            {
                if (Name == "chkWspSelectAll")
                {
                    var parent1 = VisualTreeHelper.GetParent(this);
                    while (!(parent1 is PageSelectSecteur))
                    {
                        parent1 = VisualTreeHelper.GetParent(parent1);
                    }
                    (parent1 as PageSelectSecteur).ChkWspSelectAll_OnUnchecked(sender, e);
                }
                /*Back.Fill = Off;
                Toggled1 = false;
                Dot.Margin = LeftSide;
                Dot.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                var parent = VisualTreeHelper.GetParent(this);
                while (!(parent is Page2))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                (parent as Page2).ChkWspSelect_OnUnchecked(sender, e);*/
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*if (!Toggled1)//si off -> OnCheck
            {
                if (Name == "chkWspSelectAll")
                {
                    var parent1 = VisualTreeHelper.GetParent(this);
                    while (!(parent1 is Page2))
                    {
                        parent1 = VisualTreeHelper.GetParent(parent1);
                    }
                    (parent1 as Page2).ChkWspSelectAll_OnChecked(sender, e);
                }
                Back.Fill = On;
                Toggled1 = true;
                Dot.Margin = RightSide;
                Dot.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));

                var parent = VisualTreeHelper.GetParent(this);
                while (!(parent is Page2))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                (parent as Page2).ChkWspSelect_OnChecked(sender, e);

            }
            else//si on -> OnUnChecked
            {
                if (Name == "chkWspSelectAll")
                {
                    var parent1 = VisualTreeHelper.GetParent(this);
                    while (!(parent1 is Page2))
                    {
                        parent1 = VisualTreeHelper.GetParent(parent1);
                    }
                    (parent1 as Page2).ChkWspSelectAll_OnUnchecked(sender, e);
                }
                Back.Fill = Off;
                Toggled1 = false;
                Dot.Margin = LeftSide;
                Dot.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                var parent = VisualTreeHelper.GetParent(this);
                while (!(parent is Page2))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                (parent as Page2).ChkWspSelect_OnUnchecked(sender, e);
            }*/

        }
    }
}

