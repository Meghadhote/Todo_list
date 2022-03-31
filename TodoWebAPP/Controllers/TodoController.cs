using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TodoWebAPP.Helper;


using Todo.Models;


namespace TodoWebAPP.Controllers
{
    public class TodoController : Controller
    {
        TodoAPI _todoApi = new TodoAPI();

        public async Task<IActionResult> Index()

           
        {
            
           
            List<Todo.Models.TodoList> ts = new List<Todo.Models.TodoList>();
            HttpClient client = _todoApi.Intial();
            HttpResponseMessage res = await client.GetAsync("api/Todo");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                ts = JsonConvert.DeserializeObject<List<Todo.Models.TodoList>>(results);
            }
            return View(ts);
        }


     


        public async Task<IActionResult> Upsert(int? id)
        {
            Todo.Models.TodoList cs = new Todo.Models.TodoList();
            if (id == null || id == 0)
            {

                return View(cs);
            }
            Todo.Models.TodoList ts = new Todo.Models.TodoList();
            HttpClient client = _todoApi.Intial();
            HttpResponseMessage res = await client.GetAsync($"api/Todo/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                ts = JsonConvert.DeserializeObject<Todo.Models.TodoList>(results);
            }
          //  return ts;
            return View(ts);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertAsync(Todo.Models.TodoList obj)
        {
            //check if the name is equal to description
            Todo.Models.TodoList cs = new Todo.Models.TodoList();
            if (obj.Name == obj.Description.ToString())
            {
                ModelState.AddModelError("Description", "The description cannot exactly match the Name");
            }
            //check the duplicate

            if (ModelState.IsValid)
            {
                Todo.Models.TodoList ts = new Todo.Models.TodoList();

                HttpClient client = _todoApi.Intial();
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                HttpResponseMessage response;
                if (obj.Todo_Id == 0)
                {


                    response = await client.PostAsync($"api/Todo/", new StringContent(jsonString, Encoding.UTF8, "application/json"));
                }

                else
                {

                    response = await client.PutAsync($"api/Todo/", new StringContent(jsonString, Encoding.UTF8, "application/json"));

                }
                if (response.IsSuccessStatusCode)
                {
                    var results = response.Content.ReadAsStringAsync().Result;
                    ts = JsonConvert.DeserializeObject<Todo.Models.TodoList>(results);
                }



                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
