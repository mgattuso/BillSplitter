using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models
{
    public class Bill
    {
        public int Id { get; set; }
        [Required, MaxLength(140)]
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public int PaidByRoomMateId { get; set; }
        public RoomMate PaidByRoomMate { get; set; }
        public List<Payment> Payments { get; set; }
        public int NumberOfSplits { get; set; }

        public decimal PaymentPerSplit
        {
            get
            {
                if (NumberOfSplits == 0) return 0;
                return Amount / NumberOfSplits;
            }
        }

        public decimal TotalDueRoommate
        {
            get
            {
                return Amount - PaymentPerSplit;
            }
        }

        public PaymentStatus PaymentStatus
        {
            get
            {
                if (Payments == null) return PaymentStatus.Unknown;

                if (Payments.Where(x => x.Confirmed).Sum(x => x.Amount) >= TotalDueRoommate)
                {
                    return PaymentStatus.Completed;
                }

                return PaymentStatus.Pending;
            }
        }
    }

    public enum PaymentStatus
    {
        Unknown,
        Pending,
        Completed
    }
}
