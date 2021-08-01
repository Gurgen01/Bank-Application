using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
   public  class DemandAccount:Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"New Demand Account was opened! Account's id: {this.Id}", this.Sum));
        }
    }
    
}
