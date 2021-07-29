using System;
using System.Text.Json.Serialization;

namespace BillSplitter.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        [JsonIgnore]
        public Bill Bill { get; set; }
        public int RoomMateId { get; set; }
        
        public RoomMate RoomMate { get; set; }
        public string Details { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public bool Confirmed { get; set; }
    }
}