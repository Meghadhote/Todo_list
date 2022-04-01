using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo_WebApplication.Helper;
using Todo.Models;
using System.Diagnostics;

namespace Todo_WebApplication.Controllers
{
    [Area("Todo")]
    public class TodoController : Controller
    {
        TodoAPI _todoApi = new TodoAPI();

        public async Task<IActionResult> Index()

           
        {
            
           
            List<TodoList> ts = new List<TodoList>();
            try
            {
                HttpClient client = _todoApi.Intial();
                HttpResponseMessage res = await client.GetAsync("api/Todo");
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    ts = JsonConvert.DeserializeObject<List<TodoList>>(results);
                }
                return View(ts);
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





        public async Task<IActionResult> Upsert(int? id)
        {
            TodoList cs = new TodoList();
            if (id == null || id == 0)
            {

                return View(cs);
            }
            TodoList ts = new TodoList();

            try
            {
                HttpClient client = _todoApi.Intial();
                HttpResponseMessage res = await client.GetAsync($"api/Todo/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    ts = JsonConvert.DeserializeObject<TodoList>(results);
                }
                //  return ts;
                return View(ts);
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpsertAsync(TodoList obj)
        {
            //check if the name is equal to description
           TodoList cs = new TodoList();
            if (obj.Name == obj.Description.ToString())
            {
                ModelState.AddModelError("Description", "The description cannot exactly match the Name");
            }
            //check the duplicate
             


            if (ModelState.IsValid)
            {
               TodoList ts = new TodoList();
                try
                {
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
                        ts = JsonConvert.DeserializeObject<TodoList>(results);
                    }



                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return View("Error");
                }
            }
            return View(obj);
        }





        public async Task<IActionResult> Delete(int? id)
        {
            //TodoList cs = new TodoList();
            
            TodoList ts = new TodoList();
            try
            {
                HttpClient client = _todoApi.Intial();
                HttpResponseMessage res = await client.GetAsync($"api/Todo/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
                    ts = JsonConvert.DeserializeObject<TodoList>(results);
                }

                return View(ts);
            }
            catch (Exception e)
            {
                return View("Error");
            }

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(TodoList Obj)
        {
            //check if the name is equal to description
            TodoList cs = new TodoList();

            try
            {
                HttpClient client = _todoApi.Intial();


                HttpResponseMessage response = await client.DeleteAsync($"api/Todo/{Obj.Todo_Id}");



                if (response.IsSuccessStatusCode)
                {
                    var results = response.Content.ReadAsStringAsync().Result;
                    cs = JsonConvert.DeserializeObject<TodoList>(results);
                }



                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error");
            }


        }



    }
}
