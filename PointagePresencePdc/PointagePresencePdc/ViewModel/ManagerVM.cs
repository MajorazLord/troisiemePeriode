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
        private GroupeVM selectedGroupe;

        public GroupeVM SelectedGroupe
        {
            get { return selectedGroupe; }
            set { SetProperty(ref selectedGroupe, value); }
        }

        public ObservableCollection<GroupeVM> LesGroupeVMs
        {
            get { return _lesGroupeVMs; }
        }
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
