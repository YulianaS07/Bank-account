using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class AccountPlus : Account, IAccountWithLimit 
    {
        private decimal limit;
        public decimal OneTimeDebetLimit 
        {
            get => limit;
            set
            {
                if (!IsBlocked && value >= 0)
                {
                    limit = value;
                    ZmianaAvaiableFounds();
                }
            }
        }

        public decimal AvaibleFounds { get; private set; }
       

        public AccountPlus(string name, decimal initialLimit = 100, decimal initialBalance = 0) : base(name, initialBalance)
        {
            if (initialLimit < 0)
                initialLimit = 0;

            limit = initialLimit;
            ZmianaAvaiableFounds();
        }
        private void ZmianaAvaiableFounds()
        {
            AvaibleFounds = Balance + OneTimeDebetLimit;
        }
        public new bool Deposit(decimal amount)
        {
            if (base.Deposit(amount))
            {
                ZmianaAvaiableFounds();
                Unblock1();
                return true;
            }
            return false;
        }
        public new bool Withdrawal(decimal amount)
        {
            if (!IsBlocked && amount > 0 && AvaibleFounds >= amount)
            {
                base.Withdrawal(amount);
                ZmianaAvaiableFounds();
                if (Balance < 0)
                {
                    Block();
                }
                return true;
            }
            return false;
        }

        public new void Block()
        {
            base.Block();
            ZmianaAvaiableFounds();
        }

        public new void Unblock()
        {
            if (AvaibleFounds >= 0)
            {
                base.Unblock();
                ZmianaAvaiableFounds();
            }
        }

        private void Unblock1()
        {
            if (IsBlocked && AvaibleFounds >= 0)
            {
                base.Unblock();
                ZmianaAvaiableFounds();
            }
        }


        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}"
                        : $"Account name: {Name}, balance: {Balance:F2}, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}";


    }
}

/*
// kod C# 7.2 (kompilator Mono, Linux)
// file: AccountPlus.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class AccountPlus : Account, IAccountWithLimit 
    {
        private decimal limit;
        public decimal OneTimeDebetLimit 
        {
            get => limit;
            set
            {
                if (!IsBlocked)
                limit = Math.Round(value, PRECISION);
            }
        }

        public decimal AvaibleFounds 
        {
            get => Balance + OneTimeDebetLimit;
        }

        public AccountPlus(string name, decimal initialLimit = 100, decimal initialBalance = 0) : base(name, initialBalance)
        {
            if (initialLimit < 0)
                OneTimeDebetLimit = 0;
            else
                OneTimeDebetLimit =  Math.Round(initialLimit, PRECISION);
        }

        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}"
                        : $"Account name: {Name}, balance: {Balance:F2}, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}";


    }
}
*/
