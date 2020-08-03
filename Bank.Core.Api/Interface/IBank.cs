using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Core.Api.Interface
{
    public interface IBank
    {

        /// <summary>
        /// Returns the balance from a given bank account.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="sortCode"></param>
        /// <returns></returns>
        double GetBalance(long bankAccount, int sortCode);

        /// <summary>
        /// Transfers funds between two bank accounts. Returns true if its successful.
        /// </summary>
        /// <param name="startingAccount"></param>
        /// <param name="startingSortCode"></param>
        /// <param name="endAccount"></param>
        /// <param name="endAccountSortCode"></param>
        /// <param name="fundsValue"></param>
        /// <returns></returns>
        BankResponse TransferFunds(long startingAccount, int startingSortCode, long endAccount, int endAccountSortCode, double fundsValue);
    }
}
