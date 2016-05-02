using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TodoList.Controllers;
using TodoList.Models;
using Xunit;
using Moq;
using ToDoList.Models;

namespace TodoList.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ItemsControllerTest
    {
        EFItemRepository db = new EFItemRepository(new TestDbContext());
        Mock<IItemRepository> mock = new Mock<IItemRepository>();
        private void DbSetup()
        {
            mock.Setup(m => m.Items).Returns(new Item[]
            {
                new Item {ItemId = 1, CategoryId = 1, Description = "Wash the dog"},
                new Item {ItemId = 2, CategoryId = 1, Description = "Do the dishes"},
                new Item {ItemId = 3, CategoryId = 1, Description = "Sweep the floor"}
            }.AsQueryable());
        }

        [Fact]
        public void Mock_IndexListofItems_Test()
        {
            //Arrange 
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);

            //Act 
            var result = controller.Index();

            //Assert 
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Mock_IndexListOfItems_Test()
        {
            //Arrange 
            DbSetup();
            ViewResult indexView = new ItemsController(mock.Object).Index() as ViewResult;

            //Act 
            var result = indexView.ViewData.Model;

            //Assert 
            Assert.IsType<List<Item>>(result);
        }

        [Fact]
        public void Mock_ConfirmEntry_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            Item testItem = new Item();
            testItem.Description = "Wash the dog";
            testItem.ItemId = 1;
            testItem.CategoryId = 1;

            // Act
            
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }

        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            //Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";

            //Act 
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;
            //Assert
            Assert.Contains<Item>(testItem,collection);
        } 
    }
}
