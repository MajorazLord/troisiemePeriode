using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointagePresencePdc.Model;

namespace PointagePresencePdc.ViewModel
{
    public class MachineVM : BaseViewModel<Machine>
    {
        public string IdMachine
        {
            get { return Model.IdMachine; }
            set { SetProperty(Model.IdMachine, value, () => Model.IdMachine = value); }
        }

        public MachineVM(Machine m)
        {
            Model = m;
        }

        public MachineVM(string idMachine)
        {
            Model = new Machine(idMachine);
        }
    }
}
