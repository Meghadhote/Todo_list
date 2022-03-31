using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;

namespace TodoWebAPI.Services
{
   public interface ITodoService
    {

        IEnumerable<TodoList> GetTodolist();
        TodoList GetTodoList(int? id);
        void AddTodoList(TodoList todolist);
        void UpdateTodoList(TodoList todolist);
        void DeleteTodoList(int? id);
    }
}
