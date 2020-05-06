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
            Client.DefaultRequestHeaders.Add("tok", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI1OTdmMjhkNi1lNWVmLTQyYzYtYTdhYy04M2Q0ZjI3YjNjNmUiLCJuYmYiOjE1ODg3MDgxNzYsImV4cCI6MTU4ODcxNTM3NiwiaWF0IjoxNTg4NzA4MTc2fQ.TEJTjKStOHPIpN0jY8KXk-TnSo1AEzUJjOuRl2qfGBw"); 
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        public async static Task<IEnumerable<Template>> Temp_Get()
        {
            HttpResponseMessage Response = await Client.GetAsync("http://localhost:3306/api/Computers/GetTemplates/"+ Properties.Settings.Default.Id);
            return await Response.Content.ReadAsAsync<IEnumerable<Template>>();

        }
        public async static Task<IEnumerable<Computers>> Comp_Get()
        {
            HttpResponseMessage Response = await Client.GetAsync("http://localhost:3306/api/Computers/Get");
            return await Response.Content.ReadAsAsync<IEnumerable<Computers>>();
           
        }

        public async static Task <int> Comp_Post(Computers computer)
        {
            string temp = JsonConvert.SerializeObject(computer);
            StringContent content = new StringContent(temp,UnicodeEncoding.UTF8, "application/json"); 
            return  await (await Client.PostAsync("http://localhost:3306/api/Computers/Post",content)).Content.ReadAsAsync<int>();
            
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
          Properties.Settings.Default.Id = Result;
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
