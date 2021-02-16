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
            if ((_rentalDal.GetAll(r=> r.CarId == rental.CarId) == null) || (_rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null) == null))
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAded);
            }
            return new ErrorResult(Messages.RentalNotAded);
        }

        IResult IRentalService.Delete(Rental rental)
        {
            var result = _rentalDal.Get(r => r.Id == rental.Id);

            if (result != null)
            {
                _rentalDal.Delete(result);
                return new SuccessResult(Messages.RentalDeleted);
            }
            return new ErrorResult(Messages.RentalNotFound);
        }

        IDataResult<List<Rental>> IRentalService.GetAll()
        {
            var result = _rentalDal.GetAll();

            if(result.Count != 0)
            {
                return new SuccessDataResult<List<Rental>>(Messages.RentalsListed, result);
            }
            return new ErrorDataResult<List<Rental>>(Messages.RentalNotFound);
        }

        IDataResult<Rental> IRentalService.GetById(int id)
        {
            var result = _rentalDal.Get(r => r.Id == id);

            if (result != null)
            {
                return new SuccessDataResult<Rental>(Messages.RentalListed, result);
            }
            return new ErrorDataResult<Rental>(Messages.RentalNotFound);
        }

        IResult IRentalService.Update(Rental rental)
        {
            var result = _rentalDal.Get(r => r.Id == rental.Id);

            if (result != null)
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.RentalUpdated);
            }
            return new ErrorResult(Messages.RentalNotFound);
        }
    }
}
