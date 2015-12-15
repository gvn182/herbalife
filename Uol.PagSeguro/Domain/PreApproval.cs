using System;
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents an address location, typically for shipping or charging purposes.
    /// </summary>
    public class PreApproval
    {
        /// <summary>
        /// Initializes a new instance of the Address class
        /// </summary>

        public string Charge { get; set; }
        public string Name { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public decimal MaxAmountPerPeriod { get; set; }
        public decimal MaxTotalAmount { get; set; }
        public string Period { get; set; }
        public string DayOfMonth { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfYear { get; set; }
        public decimal AmountPerPayment { get; set; }
        public string Details { get; set; }
        public string ReviewUrl { get; set; }
    }
}
