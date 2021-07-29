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
    }
}
