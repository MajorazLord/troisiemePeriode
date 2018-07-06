using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointagePresencePdc.Model
{
    public class Machine
    {
        public string IdMachine { get; set; }

        public Machine(string idMachine)
        {
            IdMachine = idMachine;
        }
    }
}
