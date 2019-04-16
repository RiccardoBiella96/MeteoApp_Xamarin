using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.Models
{
    static class XAuthKey
    {
        
        public static string xauth { get; set; }
        public static async Task<bool> isValid()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Auth", xauth);
            var result = await httpClient.GetAsync("http://10.11.89.182:8080/protected/refresh-token");


            string resultcontent = await result.Content.ReadAsStringAsync();
            Console.WriteLine("RESULT: " + resultcontent);

            try
            {
                xauth = result.Headers.GetValues("X-Auth").FirstOrDefault();
                Console.WriteLine("HEADER: " + xauth);
            }catch(Exception e)
            {
                return false;
            }
            return true;
        }

    }
}
