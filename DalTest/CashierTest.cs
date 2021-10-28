using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class CashierDalTest
    {
        private Cashier cashier = new Cashier();
        private CashierDal dal = new CashierDal();
        [Fact]
        public void LoginTest1()
        {
            cashier.Username = "tuanpf12";
            cashier.Password = "tuan2001";
            Cashier cas = dal.Login(cashier.Username,cashier.Password);
            Assert.NotNull(cas);
        }

        [Theory]
        [InlineData("tuanpf12",  "tuan2001")]
        [InlineData("hapf12",  "tuan2001")]
        [InlineData("tuanpf122",  "tuan2001")]
        [InlineData("tuanpf12",  "tuan2001dsfg")]
        [InlineData("hapf122",  "tuan2001")]
        [InlineData("hapf12",  "tuan2001dsfg")]
        
        
        public void LoginTest2(string userName, string pass)
        {
            cashier.Username = userName;
            cashier.Password = pass;
            Cashier cas = dal.Login(cashier.Username,cashier.Password);
            Assert.NotNull(cas);
        }
    }
}
