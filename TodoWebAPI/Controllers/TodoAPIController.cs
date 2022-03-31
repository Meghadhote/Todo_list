using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Todo.Models;
using TodoWebAPI.Services;

namespace TodoWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoAPIController : Controller
    {

        
        private ITodoService todoRepository;
        public TodoAPIController(ITodoService _todoRepository)
        {
            todoRepository = _todoRepository;
        }
        [HttpGet]
        public IEnumerable<TodoList> get()
        {
            return todoRepository.GetTodolist();

        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int? id)
        {
            //var todolist = _db.Todo.Find(id);
            var todolist = todoRepository.GetTodoList(id);

            return Ok(todolist);
        }
        [HttpPost]
        public IActionResult Post([FromBody] TodoList todos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            todoRepository.AddTodoList(todos);
          

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public IActionResult Put(int? id, [FromBody] TodoList todos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                todoRepository.UpdateTodoList(todos);
              }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status204NoContent);
             
            }

           
            return StatusCode(StatusCodes.Status200OK);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {

            todoRepository.DeleteTodoList(id);
           
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
