using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            
        public void Remember ()
        {


        }
        
    }
}
