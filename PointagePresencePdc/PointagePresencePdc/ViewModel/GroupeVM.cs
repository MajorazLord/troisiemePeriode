using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointagePresencePdc.Model;

namespace PointagePresencePdc.ViewModel
{
    public class GroupeVM : BaseViewModel<Groupe>
    {
        public string IdGroupe
        {
            get { return Model.IdGroupe; }
            set { SetProperty(Model.IdGroupe, value, () => Model.IdGroupe = IdGroupe); }
        }

        public bool IsSelected { get; set; }

        public int NumberOfElem => LesPosteChargeVMs.Count();

        public ObservableCollection<PosteChargeVM> LesPosteChargeVMs
        {
            get { return _lesPosteChargeVMs; }
        }
        private ObservableCollection<PosteChargeVM> _lesPosteChargeVMs = new ObservableCollection<PosteChargeVM>();

        public GroupeVM(Groupe g)
        {
            Model = g;
            foreach (PosteCharge model in Model.LesPosteCharges)
            {
                _lesPosteChargeVMs.Add(new PosteChargeVM(model));
            }
        }
    }
}
