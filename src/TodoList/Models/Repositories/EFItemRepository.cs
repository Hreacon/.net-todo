using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;

namespace ToDoList.Models
{
    public class EFItemRepository : IItemRepository
    {
        public EFItemRepository(TodoListContext connection = null)
        {
            if (connection == null)
            {
                this.db = new TodoListContext();
            }
            else
            {
                this.db = connection;
            }
        }
        TodoListContext db = new TodoListContext();

        public IQueryable<Item> Items
        { get { return db.Items; } }

        public Item Save(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return item;
        }

        public Item Edit(Item item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void Remove(Item item)
        {
            db.Items.Remove(item);
            db.SaveChanges();
        }
    }
}