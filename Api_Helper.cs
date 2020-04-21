using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
namespace Deamon
{
    public static class Api_Helper
    {
        static HttpClient Client = new HttpClient();

        static Api_Helper()
        {
            Client.BaseAddress = new Uri("http://localhost:3306/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }


        public async static Task<IEnumerable<Computers>> Comp_Get()
        {
            HttpResponseMessage Response = await Client.GetAsync("http://localhost:3306/api/Computers");
            return await Response.Content.ReadAsAsync<IEnumerable<Computers>>();
        }

        public async static Task <HttpResponseMessage> Comp_Post(Computers computer)
        {
            string temp = JsonConvert.SerializeObject(computer);
            StringContent content = new StringContent(temp,UnicodeEncoding.UTF8, "application/json"); 
            return  await Client.PostAsync("http://localhost:3306/api/Computers",content);
        }


        public static void Info_Register ()
        {
            Computers computers = new Computers();
            computers.MacAddress = (Properties.Settings.Default.MacAddres = NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault());
            computers.Isallowed = Properties.Settings.Default.Allowed;
            computers.IsActive = Properties.Settings.Default.Active;
            var Result = Comp_Post(computers).Result;
            Properties.Settings.Default.Id =  Comp_Get().Result.FirstOrDefault(a => a.MacAddress == computers.MacAddress).id; 
            Properties.Settings.Default.Save(); 

        }
       
        public static void Info_Get()
        {
            List<Computers> list = Comp_Get().Result.ToList();
            Computers computers = list.FirstOrDefault(a => a.id == Properties.Settings.Default.Id);
            Properties.Settings.Default.MacAddres = computers.MacAddress;
            Properties.Settings.Default.Active = computers.IsActive;
            Properties.Settings.Default.Allowed = computers.Isallowed;
            Properties.Settings.Default.Save(); 


        }
               

    }
}
