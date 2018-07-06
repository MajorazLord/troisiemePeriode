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
using System.Windows.Shapes;
using PointagePresencePdc.ViewModel;

namespace PointagePresencePdc.View
{
    /// <summary>
    /// Logique d'interaction pour AccueilWindow.xaml
    /// </summary>
    public partial class AccueilWindow : Window
    {
        ManagerVM Mngr
        {
            get
            {
                return (Application.Current as App)?.ManagerGlobal;
            }
        }

        public AccueilWindow()
        {
            InitializeComponent();
            this.DataContext = this;


        }
    }
}
