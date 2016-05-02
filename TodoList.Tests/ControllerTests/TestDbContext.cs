using System;
using Microsoft.Data.Entity;

namespace TodoList.Models
{
    public class TestDbContext : TodoListContext
    {
        public override DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=TodoListTest;integrated security = True");
        }
    }


}


