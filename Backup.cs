using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
   public class Backup
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public DateTime Made { get; set; }
        public bool Succesful { get; set; }
        public int Job { get; set; }

    }
}
