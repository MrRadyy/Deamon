using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deamon
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.NumPad1)
                    Api_Helper.Info_Register();

                else if (info.Key == ConsoleKey.NumPad2)
                {    Api_Helper.Info_Get();
                Console.WriteLine(" ID:"+ Properties.Settings.Default.Id + " Mac addres is : " + Properties.Settings.Default.MacAddres + " Is allowed:" + Properties.Settings.Default.Allowed);
                     }
                Console.ReadLine(); 
            }


        }
    }
}
