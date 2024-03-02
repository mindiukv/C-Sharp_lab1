using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Mindiuk.Models
{
    class UserCandidate
    {
        private DateTime _birthDate;

        public DateTime BirthDate 
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

    }
}
