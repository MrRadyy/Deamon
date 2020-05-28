using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
    public class Template
    {
        public int id { get; set; }
        public string Template_Name { get; set; }
        public string Type_Of_Backup { get; set; }
        public string Source { get; set; }
        public string Save_Options { get; set; }
        public string Schedule { get; set; }
        public string Destination { get; set; }
    }
}
