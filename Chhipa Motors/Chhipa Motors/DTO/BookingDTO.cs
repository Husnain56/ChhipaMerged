using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chhipa_Motors.DTO
{
    public class BookingDTO
    {
        public string BookingID { get; set; }
        public string UserID { get; set; }
        public string CarID { get; set; }
        public string Status { get; set; }
        public string AdminNote { get; set; }
    }
}
