using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;

        protected internal event AccountStateHandler Added;

        protected internal event AccountStateHandler Closed;

        protected internal event AccountStateHandler Opened;

        protected internal event AccountStateHandler Calculated;
        static int counter = 0;
        protected int _days = 0;
        public decimal Sum { get; private set; }
       
        public int Percentage { get; private set; }
        
        public int Id { get; private set; }
        public Account(decimal _sum,int _percentage)
        {
            Sum = _sum;
            Percentage = _percentage;
            Id = ++counter;

        }
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("On Account was added" + sum, sum));
        }
        
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Sum {sum} was withdrawn from Account id: {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Not enough money on Account id: {Id}", 0));
            }
            return result;
        }
        
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"New Account was opened! Account's id: {Id}", Sum));
        }
        
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Account {Id} was closed.  Finall amount of money: {Sum}", Sum));
        }

        protected internal void IncrementDays()
        {
            _days++;
        }
        
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Increment accrued in the amount of: {increment}", increment));
        }

    }
}
