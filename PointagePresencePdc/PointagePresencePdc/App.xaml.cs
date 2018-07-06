using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PointagePresencePdc.ViewModel;

namespace PointagePresencePdc
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal ManagerVM ManagerGlobal { get; set; } = new ManagerVM();


    }
}
