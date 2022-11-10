using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyAPI.Models;

namespace MyDB
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
          : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite($"Data Source=todo.db");
        //}

        public DbSet<TodoModel> Todos { get; set; }
    }

}