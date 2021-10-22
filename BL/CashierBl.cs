using System;
using Persistence;
using DAL;

namespace BL
{
   public class CashierBl
    {
       private CashierDal dal = new CashierDal();
       public Cashier Login(string username, string pass){
          Cashier cashier =  dal.Login(username, pass);
           return cashier;
       }
       public Cashier GetCashierInfo(int id){
          return dal.GetCashierById(id);
       }
    }
}
