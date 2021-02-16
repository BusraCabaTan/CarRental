using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {

        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }


        public IResult Add(Car car)
        {
            if (car != null)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
            return new ErrorResult(Messages.CarNotAdded);
        }

        public IResult Delete(Car car)
        {
            var result = _carDal.Get(c => c.Id == car.Id);

            if (result != null)
            {
                _carDal.Delete(result);
                return new SuccessResult(Messages.CarDeleted);
            }
            return new ErrorResult(Messages.CarNotFound);
        }

        public IDataResult<List<Car>> GetAll()
        {
            var result = _carDal.GetAll();

            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Car>>(Messages.CarsListed, result);
            }
            return new ErrorDataResult<List<Car>>(Messages.CarNotFound);
        }

        public IDataResult<List<Car>> GetByBrand(int id)
        {
            var result = _carDal.GetAll(c => c.BrandId == id);

            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Car>>(Messages.CarsListed, result);
            }
            return new ErrorDataResult<List<Car>>(Messages.CarNotFound);
        }

        public IDataResult<List<Car>> GetByColor(int id)
        {
            var result = _carDal.GetAll(c => c.ColorId == id);

            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Car>>(Messages.CarsListed,result);
            }
            return new ErrorDataResult<List<Car>>(Messages.CarNotFound);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);

            if (result != null)
            {
                return new SuccessDataResult<Car>(Messages.CarListed, result);
            }
            return new ErrorDataResult<Car>(Messages.CarNotFound);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = _carDal.GetCarDetails();
            if (result.Count != 0)
            {
                return new SuccessDataResult<List<CarDetailDto>>(Messages.CarsListed,result);
            }
            return new ErrorDataResult<List<CarDetailDto>>(Messages.CarNotFound);
        }

        public IResult Update(Car car)
        {
            var result = _carDal.Get(c => c.Id == car.Id);

            if (result != null)
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);
            }
            return new ErrorResult(Messages.CarNotFound);
        }
    }
}
