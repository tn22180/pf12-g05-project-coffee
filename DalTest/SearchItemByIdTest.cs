using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class SearchItemByIdTest
    {
        Item item = new Item();
        ItemDal idal = new ItemDal();
        [Fact]
        public void SearchByIdTest1()
        {
            item.ItemId = 1;
            
            Item i = idal.SearchById(item.ItemId);
            Assert.True(i != null);
        }
        [Theory]
       [InlineData(2)]
       [InlineData(0)]
       [InlineData(-1)]
       [InlineData(100)]
       
       

       public void SearchByIdTest2(int id)
        {
            item.ItemId = id;
            Item i = idal.SearchById(item.ItemId);
            Assert.True(i == item);
        }
        
    }
}