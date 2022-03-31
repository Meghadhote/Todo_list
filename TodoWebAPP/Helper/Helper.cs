using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TodoWebAPP.Helper
{
    public class TodoAPI
    {
         public  HttpClient Intial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/");
            return client;
        }
    }
}
