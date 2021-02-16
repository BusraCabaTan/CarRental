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


             var result =  rentalManager.Add(new Rental()
             {
                 Id = 10,
                 CarId = 4,
                 CustomerId = 1,
                 RentDate = DateTime.Now,
             });

   
            Console.WriteLine(result.Message);
        }

 
    }
}
