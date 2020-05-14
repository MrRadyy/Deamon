using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
   public class Config
    {
      public  List<ModelSnapshot> snapshots = new List<ModelSnapshot>();

       

         public string Serialize()
        {
            string  json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

        public void Write(string snaptxt)
        {
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(snaptxt, true))
            {
                file.WriteLine(Serialize());
            }

        }


    }
}
