using Deamon.Backupsa_algo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf; 

namespace Deamon
{
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<Service>(
                    y => { y.ConstructUsing(z => new Service());
                        y.WhenStarted(r => r.Start());
                        y.WhenStopped(s => s.Stop());
                    }) ;
                x.StartAutomatically(); 
            });
            //while (true)
            //{

            //    ConsoleKeyInfo info = Console.ReadKey(); //Register of info
            //    if (info.Key == ConsoleKey.NumPad1)
            //        Api_Helper.Info_Register();

            //    else if (info.Key == ConsoleKey.NumPad2)//Load of saved info
            //    {
            //        Api_Helper.Info_Get();
            //        Console.WriteLine(" ID:" + Properties.Settings.Default.Id + " Mac addres is : " + Properties.Settings.Default.MacAddres + " Is allowed:" + Properties.Settings.Default.Allowed + "Is active: " + Properties.Settings.Default.Active);
            //    }
            //    else if (info.Key == ConsoleKey.NumPad3)

            //    {
            //        Properties.Settings.Default.Id = 11; 
            //        ConfigurationManager.Update();
            //        foreach (var item in ConfigurationManager.Dictionary.Values)
            //        {
            //            Console.WriteLine(" "+item.id +" "+ item.Save_Options);
            //        }     
            //    }  
                
            //    else if (info.Key == ConsoleKey.NumPad4)
            //    {

            //        Properties.Settings.Default.Id = 11;
            //        ConfigurationManager.Update();
            //        foreach (var item in ConfigurationManager.Dictionary.Values)
            //        {
                       
            //        FullBackup New = new FullBackup(item.Source,@"C:\Andosis");

            //        New.Write();
            //        }

                    

            //    }

              //  Console.ReadLine(); 
                
               
            
            
            //}


        }
    }
}
