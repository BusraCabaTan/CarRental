using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal , ICarService carService) 
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }


        [SecuredOperation("rental.add,admin")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(CarAvaiable(rental.CarId),CheckCarIsThere(rental.CarId));

            if (result == null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAded);
            }
            return new ErrorResult(result.Message);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            var result = BusinessRules.Run(CheckRentalIsThere(rental.Id));

            if (result == null)
            {
                _rentalDal.Delete(rental);
                return new SuccessResult(Messages.RentalDeleted);
            }
            return new ErrorResult(result.Message);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            var result = _rentalDal.GetAll();

            if(result.Count != 0)
            {
                return new SuccessDataResult<List<Rental>>(Messages.RentalsListed, result);
            }
            return new ErrorDataResult<List<Rental>>(Messages.RentalNotFound);
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            var result = BusinessRules.Run(CheckRentalIsThere(id));

            if (result == null)
            {
                return new SuccessDataResult<Rental>(Messages.RentalListed, _rentalDal.Get(r=> r.Id == id));
            }
            return new ErrorDataResult<Rental>(result.Message);
        }


        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            var result = BusinessRules.Run(CheckRentalIsThere(rental.Id));

            if (result == null)
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.RentalUpdated);
            }
            return new ErrorResult(result.Message);
        }


        private IResult CarAvaiable(int carId)
        {
            if ((_rentalDal.GetAll(r => r.CarId == carId) == null) || (_rentalDal.Get(r => r.CarId == carId && r.ReturnDate == null) == null))
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.RentalNotAded);
        }

        private IResult CheckCarIsThere(int id)
        {
            if (_carService.GetById(id).Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarNotFound);
        }

        private IResult CheckRentalIsThere(int id)
        {
            if(_rentalDal.Get(r => r.Id == id) != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.RentalNotFound);
        }
    }
}
