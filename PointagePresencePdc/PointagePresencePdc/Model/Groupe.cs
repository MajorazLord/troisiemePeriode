using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointagePresencePdc.Model
{
    public class Groupe
    {
        public string IdGroupe { get; set; }

        public List<PosteCharge> LesPosteCharges
        {
            get { return _lesPosteCharges; }
        }

        private List<PosteCharge> _lesPosteCharges;

        public Groupe()
        {

        }

        public Groupe(List<PosteCharge> lesPosteCharges, string idGroupe)
        {
            _lesPosteCharges = lesPosteCharges;
            IdGroupe = idGroupe;
        }

        public bool Equals(Groupe otherGroupe)
        {
            return (IdGroupe == otherGroupe.IdGroupe);
        }
    }
}
