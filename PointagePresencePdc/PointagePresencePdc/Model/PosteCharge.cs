using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointagePresencePdc.Model
{
    public class PosteCharge
    {
        public string IdPosteCharge { get; set; }

        public List<Machine> LesMachines
        {
            get
            {
                return _lesMachines;
            }
        }
        private List<Machine> _lesMachines;

        public int Statut { get; set; }

        public PosteCharge() { }

        public PosteCharge(string idPosteCharge)
        {
            IdPosteCharge = idPosteCharge;
            _lesMachines = new List<Machine>();
            Statut = -1;
        }

        public PosteCharge(string idPosteCharge, List<Machine> lesMachines, int statut)
        {
            IdPosteCharge = idPosteCharge;
            _lesMachines = lesMachines;
            Statut = statut;
        }

        public bool Equals(PosteCharge otherPdc)
        {
            return (IdPosteCharge == otherPdc.IdPosteCharge);
        }

    }
}
