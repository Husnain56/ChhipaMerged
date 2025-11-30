using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chhipa_Motors.DL;
using Chhipa_Motors.DTO;

namespace Chhipa_Motors.BL
{
    public class CustomerBL
    {
        private CustomerDL _customerDL;

        public CustomerBL()
        {
            _customerDL = new CustomerDL();
        }

        public DataTable fetchBookings(UserDTO userDTO)
        {
            return _customerDL.FetchBookings(userDTO);
        }
        public DataTable fetchPurchasedCars(UserDTO userDTO)
        {
            return _customerDL.FetchPurchasedCars(userDTO);
        }
        public int cancelBooking(BookingDTO bookDTO)
        {
            return _customerDL.cancelBooking(bookDTO);
        }
        public int addorUpdateCustomer(CustomerDTO custDTO)
        {
            if (_customerDL.customerExists(custDTO))
            {
                return _customerDL.updateCustomer(custDTO);
            }
            else
            {
                return _customerDL.addCustomer(custDTO);
            }
        }
        public bool IsCustomer(string userId)
        {
            return _customerDL.IsCustomer(userId);
        }

        public CustomerDTO GetCustomerInfo(string userId)
        {
            return _customerDL.GetCustomerInfo(userId);
        }

        public int UpdateEmail(string userId, string newEmail)
        {
            return _customerDL.UpdateEmail(userId, newEmail);
        }

        public int UpdatePhone(string userId, string newPhone)
        {
            return _customerDL.UpdatePhone(userId, newPhone);
        }
    }
}
