using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Add(IFormFile file, CarImage carImage)
        {

            var result = BusinessRules.Run(CheckCarImagesLimit(carImage.CarId));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);

            return new SuccessResult(Messages.CarImageAded);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAllByCarId(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id);

            if(result.Count == 0)
            {
                var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName 
                            + @"\WebAPI\wwwroot\Uploads\default.jpg");
                result.Add(new CarImage { CarId = id ,ImagePath = path, Date = DateTime.Now });
                return new SuccessDataResult<List<CarImage>>(Messages.CarImagesListed, result);
            }
            return new SuccessDataResult<List<CarImage>>(Messages.CarImagesListed, result);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var updatedPath = FileHelper.Update(carImage.ImagePath , file);
            carImage.ImagePath = updatedPath;
             _carImageDal.Update(carImage);

            return new SuccessResult(Messages.CarImageUpdated);

        }

        private IResult CheckCarImagesLimit(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id).Count;

            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImagesLimited);
            }
            return new SuccessResult();
        }

    }
}
