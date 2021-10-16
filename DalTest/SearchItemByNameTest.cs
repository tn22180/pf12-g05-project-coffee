using System;
using Xunit;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace DALTest
{
    public class SearchItemByNameTest
    {
        Item item = new Item();
        
        ItemDal idal = new ItemDal();
        [Fact]
        public void SearchByNameTest1()
        {   
            int itemFilter = 1;
            item.ItemName = "Milk Tea";
            List<Item> li = idal.GetItems(itemFilter,new Item{ItemName = item.ItemName});
            Assert.True(li != null);
        }
        [Theory]
       [InlineData(1,"M")]
       [InlineData(1,"1")]
       [InlineData(1,"!")]
       [InlineData(1,"")]
       [InlineData(1," ")]
       
       

       public void SearchByIdTest1(int itemFilter1 ,string name)
        {   
            int itemFilter = itemFilter1;
            item.ItemName = name;
            List<Item> li = idal.GetItems(itemFilter,new Item{ItemName = item.ItemName});
            Assert.True(li != null);
        }
        
    }
}