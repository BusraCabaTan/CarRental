using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());


           var result = rentalManager.Add(new Rental()
            {
                Id = 3,
                CarId = 1,
                CustomerId = 3,
                RentDate = DateTime.Now,
            }); 


            Console.WriteLine(result.Message);

            //CarDetails(carManager);
        }

        private static void CarDetails(CarManager carManager)
        {
            var result = carManager.GetCarDetails();

            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine(car.BradName + " / " + car.ColorName + " / " + car.Description + " / " + car.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
