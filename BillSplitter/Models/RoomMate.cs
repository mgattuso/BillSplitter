using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BillSplitter.Models
{
    public class RoomMate
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [JsonIgnore]
        public List<Bill> Bills { get; set; }
        [JsonIgnore]
        public List<Payment> Payments { get; set; }
    }
}