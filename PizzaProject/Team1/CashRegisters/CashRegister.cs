using PizzaProject.Team1.TemporaryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Team1.CashRegisters
{
    internal class CashRegister : ICashRegister
    {
        private IMenu _menu;
        public IMenu GetMenu { get => _menu; }

        HashSet<Wallet> _localWallets;
        public HashSet<Wallet.MoneyCategory> PaymentMethod { get => _localWallets.Select(x => x.Category).ToHashSet(); }

        List<object> _customersInQueue;
        public int CustomersInQueue { get => _customersInQueue.Count; }



        public event OrderApperiance NewOrderApperiance;



        public HashSet<object> CheckCustomerStatus(object customer)
        {
            throw new NotImplementedException();
        }
        public void AddToQueue(object customer)
        {
            _customersInQueue.Add(customer);
        }
        public void DeleteFromQueue(object customer)
        {
            _customersInQueue.Remove(customer);
        }
        public bool Pay(Wallet customerWallet, object order)
        {
            /*
             
                if(_localWallets.Any(lw => lw.Category == customerWallet.Category))
                {
                    if(customerWallet.MoneyCount >= order.GetTotalPrice())
                    {
                        customerWallet.Pay(order.GetTotalPrice(),_localWallets.First(lw => lw.Category == customerWallet.Category));
                        NewOrderApperiance(order);
                        return true;
                    }
                }
                return false;

             */
            throw new NotImplementedException();
        }
    }
}
