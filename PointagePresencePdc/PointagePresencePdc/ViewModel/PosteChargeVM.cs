using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointagePresencePdc.Model;

namespace PointagePresencePdc.ViewModel
{
    public class PosteChargeVM : BaseViewModel<PosteCharge>
    {
        public string IdPosteCharge
        {
            get { return Model.IdPosteCharge; }
            set { SetProperty(Model.IdPosteCharge, value, () => Model.IdPosteCharge = value); }
        }

        public ObservableCollection<MachineVM> LesMachineVMs
        {
            get { return _lesMachineVMs; }
        }
        private ObservableCollection<MachineVM> _lesMachineVMs ;

        public int Statut
        {
            get { return Model.Statut; }
            set { SetProperty(Model.Statut, value, () => Model.Statut = value); }
        }

        public bool Equals(PosteChargeVM otherPdcVm)
        {
            return (IdPosteCharge == otherPdcVm.IdPosteCharge);
        }

        public PosteChargeVM(PosteCharge pc)
        {
            Model = pc;
            _lesMachineVMs = new ObservableCollection<MachineVM>();
            foreach (Machine modelLesMachine in Model.LesMachines)
            {
                _lesMachineVMs.Add(new MachineVM(modelLesMachine));
            }
        }
        
    }
}
