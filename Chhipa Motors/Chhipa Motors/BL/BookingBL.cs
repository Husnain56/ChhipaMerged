using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chhipa_Motors.DTO;
using Chhipa_Motors.DL;

namespace Chhipa_Motors.BL
{
    public class BookingBL
    {   
        private BookingDL _bookingDL;
        public BookingBL()
        {
            _bookingDL = new BookingDL();
        }
        public int CreateBooking(BookingDTO bookingDTO)
        {
            return _bookingDL.addBooking(bookingDTO);
        }
    }
}
