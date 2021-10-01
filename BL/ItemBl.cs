using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
   public class ItemBl
    {
       private ItemDal idal = new ItemDal();
       public Item SearchById(int id){
          return idal.SearchById(id);
       }
       public List<Item> SearchByName(string name){
          return idal.GetItems(ItemFilter.FILTER_BY_ITEM_NAME, new Item{item_name = name});
       }
       public List<Item> GetAll(){
          return idal.GetItems(ItemFilter.GET_ALL, null);
       }
    }
}