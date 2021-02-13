using Business.Concrete;
using Core.Utilities.Results;
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

            UserManager userManager = new UserManager(new EfUserDal());

            var result = userManager.Add(new User() { 
                Id = 4,
                FirstName = "Melahat",
                LastName = "Caba Tan",
                Email = "melos@cabatanailese.com",
                Password = "melos41"
            });

            Console.WriteLine(result.Message);



            //IResult result = AddRental(rentalManager);
            //Console.WriteLine(result.Message);
            //CarDetails(carManager);
        }

        private static IResult AddRental(RentalManager rentalManager)
        {
            return rentalManager.Add(new Rental()
            {
                Id = 3,
                CarId = 1,
                CustomerId = 3,
                RentDate = DateTime.Now,
            });
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
