using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.DataAccess;
using Todo.Models;

namespace TodoWebAPI.Services
{
    public class TodoRepository : ITodoService
    {

        private readonly TododbContext _db;

        public TodoRepository(TododbContext db)
        {
            _db = db;
        }

        public void AddTodoList(TodoList todolist)
        {
            _db.Todo.Add(todolist);
            _db.SaveChanges(true);
        }

        public void DeleteTodoList(int? id)
        {
            var todolist = _db.Todo.Find(id);

            _db.Todo.Remove(todolist);
            _db.SaveChanges(true);
        }

        public IEnumerable<TodoList> GetTodolist()
        {
            return _db.Todo;
        }

        public TodoList GetTodoList(int? id)
        {
            var todolist = _db.Todo.Find(id);
            return todolist;
        }

        public void UpdateTodoList(TodoList todolist)
        {

            var todo = _db.Todo.Find(todolist.Todo_Id);

            todo.Name = todolist.Name;
            todo.Description = todolist.Description;
            todo.UpdatedDatetime = todolist.UpdatedDatetime;
            _db.Todo.Update(todo);
            _db.SaveChanges(true);
        }
    }
}
