using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"New Deposit Account was opened! Account id: {this.Id}", this.Sum));
        }

        public override void Put(decimal sum)
        {
            if (_days % 30 == 0)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs("The account can only be credited after a 30-day period", 0));
        }

        public override decimal Withdraw(decimal sum)
        {
            if (_days % 30 == 0)
                return base.Withdraw(sum);
            else
                base.OnWithdrawed(new AccountEventArgs("You can withdraw funds only after a 30-day period", 0));
            return 0;
        }

        protected internal override void Calculate()
        {
            if (_days % 30 == 0)
                base.Calculate();
        }
    }
}
