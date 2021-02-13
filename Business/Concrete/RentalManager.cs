using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {

        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal) 
        {
            _rentalDal = rentalDal;
        }



        public IResult Add(Rental rental)
        {
            if ((_rentalDal.Get(r=> r.CarId == rental.CarId) == null) || (_rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate != null) != null))
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAded);
            }
            return new ErrorResult(Messages.RentalNotAded);
        }
    }
}
