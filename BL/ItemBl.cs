using System;
using Persistence;
using DAL;

namespace BL
{
   public class ItemBl
    {
       private ItemDal dal = new ItemDal();
       public void SearchById(Item item){
          dal.SearchById(item);
       }
       public void SearchByName(Item item){
          dal.SearchByName(item);
       }
    }
}