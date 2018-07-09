using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointagePresencePdc.Model;

namespace PointagePresencePdc.ViewModel
{
    class ManagerVM : BaseViewModel<Manager>
    {
        public int NoPointage { get; set; }

        private ObservableCollection<GroupeVM> _selectedGroupe;

        public ObservableCollection<GroupeVM> SelectedGroupes
        {
            get => _selectedGroupe;
            set => SetProperty(ref _selectedGroupe, value);
        }

        public ObservableCollection<GroupeVM> LesGroupeVMs => _lesGroupeVMs;
        private ObservableCollection<GroupeVM> _lesGroupeVMs = new ObservableCollection<GroupeVM>();

        public ManagerVM()
        {
            Model = new Manager();
            foreach (Groupe modelGroupe in Model.LesGroupes)
            {
                _lesGroupeVMs.Add(new GroupeVM(modelGroupe));
            }
        }

    }
}
