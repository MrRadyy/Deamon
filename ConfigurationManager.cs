using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace Deamon
{
  public static  class ConfigurationManager
    {


        public static string PATH = @"C:\Users\Radyy\Desktop\PROJEKT\json.json";
    public static Dictionary<int, Template> Dictionary = new Dictionary<int, Template>();
        static ConfigurationManager()
        {
            Dictionary = Read();
                       
        }
        
      static Dictionary<int, Template> Read()
        {
            if (File.Exists(PATH) )
            {
                StreamReader reader = new StreamReader(PATH);
                string temp = reader.ReadToEnd();
                reader.Dispose (); 
               return  JsonConvert.DeserializeObject<Dictionary <int,Template>>(temp);  
            }
                        
            return new Dictionary<int,Template>(); 
        }

        public static void Update()
        {
           var templates = Api_Helper.Temp_Get().Result;
            foreach (var item in templates)
            {
                Dictionary[item.id] = item; 
                
            }
            List<Template> temp = Dictionary.Values.Except(templates).ToList();  
            foreach (var item in temp)
            {
                Dictionary.Remove(item.id);
                
            }
            Save(); 
        }


       static void Save()
        {
            string temp = JsonConvert.SerializeObject(Dictionary);
            if (File.Exists(PATH))
            {
                File.Delete(PATH); 
            }
            StreamWriter writer = new StreamWriter(PATH);
                writer.Write(temp);
                writer.Dispose();
            
         
        }
        
    }
}
