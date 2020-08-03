using Payment.Gateway.Api.ExtensionMethods;
using Payment.Gateway.Api.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.DTO
{
    /// <summary>
    /// Payment data model. Please note that there is validation for all required fields.
    /// </summary>
    public class PaymentData : IPaymentData
    {

        [Key]
        public string PaymentId { get; set; }




        [Required(ErrorMessage = "Bank Account is required")]
        [RegularExpression(@"^\d{16,16}$", ErrorMessage = "Please enter a 16 digit account number")]
        public long? BankAccountNumber { get; set; }




        [Required(ErrorMessage = "Sort Code is required")]
        [RegularExpression(@"^\d{6,6}$", ErrorMessage = "Please enter a 6 digit sort code")]

        public int? SortCode { get; set; }





        [Required(ErrorMessage = "CCV  is required")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Please enter a 3 to 4 digit ccv")]

        public int? CcvNumber { get; set; }


        public DateTime TransactionDate { get; set; }
        public PaymentStatus Status { get; set; }
        
        [Required(ErrorMessage = "A payment value  is required")]
        [Range(1.0,999999.99, ErrorMessage = "Only values between 1 and 1million")]
        public double? PaymentValue { get; set; }


        [Required(ErrorMessage = "A card holder value  is required")]
        public string CardHolderName { get; set; }


       

    }







    public enum PaymentStatus
    {
        Submitted,
        InProgresss,
        Successful,   
        Errored
    }
}
