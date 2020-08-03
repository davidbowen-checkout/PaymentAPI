using Bank.Core.Api;
using Bank.Core.Api.Interface;
using Newtonsoft.Json;
using Payment.Gateway.Api.Database;
using Payment.Gateway.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Gateway.Agent
{
    public class PaymentProcessingTask
    {


        private  static string BASE_URL = $"https://localhost:{44360}/Tasks";
        private readonly HttpClient _client;

        public PaymentProcessingTask(HttpClient client)
        {
            _client = client;
        }


        /// <summary>
        /// Checks to see if there's any payments that require processing.
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentData> checkForJobs()
        {
            var httpResponse = await _client.GetAsync(BASE_URL);

            if (!httpResponse.IsSuccessStatusCode)           
                throw new Exception("Unable to access API");


            var content = await httpResponse.Content.ReadAsStringAsync();
            var paymentToProcess = JsonConvert.DeserializeObject<PaymentData>(content);

          
            return paymentToProcess;

           
        }

        /// <summary>
        /// Carries out a payment between two bank accounts.
        /// </summary>
        /// <param name="bank"></param>
        /// <param name="destinationSortCode"></param>
        /// <param name="destinationAccount"></param>
        /// <param name="paymentToProcess"></param>
        /// <returns></returns>
        public async Task<PaymentData> enactPayment(IBank bank, int destinationSortCode, long destinationAccount, PaymentData paymentToProcess)
        {
            var success = await Task.Run(() => bank.TransferFunds(Convert.ToInt64( paymentToProcess.BankAccountNumber), Convert.ToInt32(paymentToProcess.SortCode), destinationAccount, destinationSortCode, Convert.ToDouble(paymentToProcess.PaymentValue))); // Transfer money from the client to the company account.
            paymentToProcess.Status = success.PaymentStatus == BankPaymentStatus.Successfull ? PaymentStatus.Successful : PaymentStatus.Errored; //If the money was taken, then its successfull. Otherwise not.
            return paymentToProcess;
        }


        /// <summary>
        /// Updates a payment with with the contents of a payment. Note that this should be secured.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePayment(PaymentData payment)
        {

            var httpResponse = await _client.PostAsync(BASE_URL, new StringContent(JsonConvert.SerializeObject(    payment),    Encoding.UTF8, "application/json"));

            if(httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }


    }
}
