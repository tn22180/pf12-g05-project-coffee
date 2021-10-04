using System;
using Persistence;
using DAL;

namespace BL
{
   public class CashierBl
    {
       private CashierDal dal = new CashierDal();
       public int Login(Cashier cashier){
           return dal.Login(cashier);
       }
       public Cashier GetCashierInfo(int id){
          return dal.GetCashierById(id);
       }
    }
}
