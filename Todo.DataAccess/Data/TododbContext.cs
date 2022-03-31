using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Microsoft.EntityFrameworkCore;
namespace Todo.DataAccess
{
    public class TododbContext:DbContext

    {
        public TododbContext(DbContextOptions<TododbContext> options) : base(options)
        {

        }


        public DbSet<TodoList> Todo { get; set; }
    }
}
