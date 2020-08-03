using Bank.Core.Api.Interface;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Bank.Core.Api
{
    public class ExampleBank : IBank
    {


        public ExampleBank()
        {
            addFunds(999999, 121212, 1000);

        }

        /// <summary>
        /// Used to setup a bank with some values. 
        /// </summary>
        /// <param name="setupData"></param>
        public ExampleBank(IEnumerable<SetupData> setupData)
        {
            foreach(var val in setupData)
            {
                addFunds(val.BankAccount, val.SortCode, val.Value);
            }
        
        }





        private Dictionary<String, double> _bankAccountBalances = new Dictionary<String, double>();


        /// <summary>
        /// Returns the balance of the bank account in question.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="sortCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetBalance(long bankAccount, int sortCode)
            => _bankAccountBalances[Convert.ToString(bankAccount + sortCode)];


        /// <summary>
        /// Transfers money between two bank accounts.
        /// </summary>
        /// <param name="startingAccount"></param>
        /// <param name="startingSortCode"></param>
        /// <param name="endAccount"></param>
        /// <param name="endAccountSortCode"></param>
        /// <param name="fundsValue"></param>
        /// <returns></returns>
        public BankResponse TransferFunds(long startingAccount, int startingSortCode, long endAccount, int endAccountSortCode, double fundsValue)
        {
            Thread.Sleep(2); //Artifical delay

            //Try and take the cash from the bank account. If there's insufficient funds, return false;
            if (withdrawFunds(Convert.ToInt64(startingAccount), Convert.ToInt32(startingSortCode), fundsValue))
            {
                addFunds(endAccount, endAccountSortCode, fundsValue);
                return new BankResponse { UniqueIdentifier = new Guid().ToString(), PaymentStatus = BankPaymentStatus.Successfull };

            }
            else
            {
                return new BankResponse { UniqueIdentifier = new Guid().ToString(), PaymentStatus = BankPaymentStatus.Failed };
            }
        }

        /// <summary>
        ///  Adds funds to a given bank account.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="sortCode"></param>
        /// <param name="value"></param>
        private void addFunds(long bankAccount, int sortCode, double value)
        {
            var key = Convert.ToString(bankAccount + sortCode);
            if (_bankAccountBalances.ContainsKey(key))
                _bankAccountBalances[key] += value;
            else
                _bankAccountBalances.Add(key, value);
        }

        /// <summary>
        /// Withdraws funds from a given bank account. 
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="sortCode"></param>
        /// <param name="value"></param>
        /// <returns>Returns if the withdrawal was successful. If there is insufficient funds this will return false.</returns>
        private bool withdrawFunds(long bankAccount, int sortCode, double value)
        {
            if (!_bankAccountBalances.ContainsKey(Convert.ToString(bankAccount + sortCode)))
                return false;

            if (_bankAccountBalances[Convert.ToString(bankAccount + sortCode)] - value > 0)
            {
                _bankAccountBalances[Convert.ToString(bankAccount + sortCode)] -= value;
                return true;
            }
            else
            {
                return false;
            }

        }




    }



    public class SetupData
    {
        public long BankAccount { get; set; }
        public int SortCode { get; set; }
        public double Value { get; set; }
    }
}
