using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chhipa_Motors.DTO;
using Chhipa_Motors.DL; 

namespace Chhipa_Motors.BL
{
    public class CarBL
    {
        CarDL _carDL;
        public CarBL()
        {
            _carDL = new CarDL();
        }
        public List<CarDTO> GetCarsByManufacturer(string manufacturer)
        {
            return _carDL.GetCarsByManufacturer(manufacturer);
        }

    }
}
