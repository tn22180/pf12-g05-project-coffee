using System;
using Xunit;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace DALTest
{
    public class ItemTest
    {
        Item item = new Item();
        List<Item> lis = new List<Item>();
        ItemDal idal = new ItemDal();
        [Fact]
        public void SearchByIdTest1()
        {   
            
            item.ItemId = 1;    
            Item i = idal.SearchById(item.ItemId);
            Assert.True(i!= null);
        }
        [Fact]
        public void SearchByNameTest1()
        {   
            int itemFilter = 1;
            item.ItemName = "Milk Tea";
            lis = idal.GetItems(itemFilter,new Item{ItemName = item.ItemName});
            Assert.True(lis != null);
        }
        [Theory]
       [InlineData(2)]
       [InlineData(3)]
       [InlineData(4)]
       [InlineData(20)]
       [InlineData(7)]
       public void SearchByIdTest3(int id)
        {
            item.ItemId = id;
            Item it = idal.SearchById(item.ItemId);
             Assert.True(it != null);
        }
        [Theory]
       [InlineData(1,"Red Bull")]
       [InlineData(1,"Brown Coffee")]
       [InlineData(1,"Sting")]
       [InlineData(1,"Green Tea Latte")]
       [InlineData(1,"Coffee Latte")]
       public void SearchByNameTest2(int itemFilter1 ,string name)
        {   
            int itemFilter = itemFilter1;
            item.ItemName = name;
            lis = idal.GetItems(itemFilter,new Item{ItemName = item.ItemName});
            Assert.True(lis != null);
        }
    }
}