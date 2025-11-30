using Chhipa_Motors.BL;
using System.Drawing;

namespace Chhipa_Motors.Factory
{
    public class CarProduct
    {
        public int CarID { get; set; }
        public string CarName { get; set; }
        public string Manufacturer { get; set; }
        public string ModelYear { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
        public Image CarImage { get; set; }

        public CarProduct(int carId, string carName, string manufacturer, string modelYear,
                         decimal price, int stock, bool active, Image carImage)
        {
            CarID = carId;
            CarName = carName;
            Manufacturer = manufacturer;
            ModelYear = modelYear;
            Price = price;
            Stock = stock;
            Active = active;
            CarImage = carImage;
        }
    }


        public abstract class CarCreator
        {
            public abstract CarProduct CreateCar(string carName, Image carImage);

            public CarProduct GetCarWithImage(string carName, Image carImage)
            {
                return CreateCar(carName, carImage);
            }
        }
    

        public class PorscheCreator : CarCreator
        {
            private CarBL _carBL;

            public PorscheCreator()
            {
                _carBL = new CarBL();
            }

            public override CarProduct CreateCar(string carName, Image carImage)
            {
                try
                {
                    var cars = _carBL.GetCarsByManufacturer("Porsche");
                    var car = cars.FirstOrDefault(c => c.CarName == carName);

                    if (car == null)
                        throw new Exception($"Car not found: {carName}");

                    return new CarProduct(
                        carId: int.Parse(car.CarID),
                        carName: car.CarName,
                        manufacturer: car.Make,
                        modelYear: car.ModelYear,
                        price: decimal.Parse(car.Price),
                        stock: int.Parse(car.Stock),
                        active: ParseActiveStatus(car.Status),
                        carImage: carImage
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating Porsche car: {ex.Message}");
                }
            }

            private bool ParseActiveStatus(string status)
            {
                if (string.IsNullOrEmpty(status)) return false;
                string s = status.Trim().ToLower();
                return s == "active" || s == "1" || s == "true";
            }
        }
 
        public class NissanCreator : CarCreator
        {
            private CarBL _carBL;

            public NissanCreator()
            {
                _carBL = new CarBL();
            }

            public override CarProduct CreateCar(string carName, Image carImage)
            {
                try
                {
                    var cars = _carBL.GetCarsByManufacturer("Nissan");
                    var car = cars.FirstOrDefault(c => c.CarName == carName);

                    if (car == null)
                        throw new Exception($"Car not found: {carName}");

                    return new CarProduct(
                        carId: int.Parse(car.CarID),
                        carName: car.CarName,
                        manufacturer: car.Make,
                        modelYear: car.ModelYear,
                        price: decimal.Parse(car.Price),
                        stock: int.Parse(car.Stock),
                        active: ParseActiveStatus(car.Status),
                        carImage: carImage
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating Nissan car: {ex.Message}");
                }
            }

            private bool ParseActiveStatus(string status)
            {
                if (string.IsNullOrEmpty(status)) return false;
                string s = status.Trim().ToLower();
                return s == "active" || s == "1" || s == "true";
            }
        }
    
        public class LamborghiniCreator : CarCreator
        {
            private CarBL _carBL;

            public LamborghiniCreator()
            {
                _carBL = new CarBL();
            }

            public override CarProduct CreateCar(string carName, Image carImage)
            {
                try
                {
                    var cars = _carBL.GetCarsByManufacturer("Lamborghini");
                    var car = cars.FirstOrDefault(c => c.CarName == carName);

                    if (car == null)
                        throw new Exception($"Car not found: {carName}");

                    return new CarProduct(
                        carId: int.Parse(car.CarID),
                        carName: car.CarName,
                        manufacturer: car.Make,
                        modelYear: car.ModelYear,
                        price: decimal.Parse(car.Price),
                        stock: int.Parse(car.Stock),
                        active: ParseActiveStatus(car.Status),
                        carImage: carImage
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating Lamborghini car: {ex.Message}");
                }
            }

            private bool ParseActiveStatus(string status)
            {
                if (string.IsNullOrEmpty(status)) return false;
                string s = status.Trim().ToLower();
                return s == "active" || s == "1" || s == "true";
            }
        }

        public class McLarenCreator : CarCreator
        {
            private CarBL _carBL;

            public McLarenCreator()
            {
                _carBL = new CarBL();
            }

            public override CarProduct CreateCar(string carName, Image carImage)
            {
                try
                {
                    var cars = _carBL.GetCarsByManufacturer("McLaren");
                    var car = cars.FirstOrDefault(c => c.CarName == carName);

                    if (car == null)
                        throw new Exception($"Car not found: {carName}");

                    return new CarProduct(
                        carId: int.Parse(car.CarID),
                        carName: car.CarName,
                        manufacturer: car.Make,
                        modelYear: car.ModelYear,
                        price: decimal.Parse(car.Price),
                        stock: int.Parse(car.Stock),
                        active: ParseActiveStatus(car.Status),
                        carImage: carImage
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating McLaren car: {ex.Message}");
                }
            }

            private bool ParseActiveStatus(string status)
            {
                if (string.IsNullOrEmpty(status)) return false;
                string s = status.Trim().ToLower();
                return s == "active" || s == "1" || s == "true";
            }
        }
}

