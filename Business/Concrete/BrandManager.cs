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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            if (brand != null)
            {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);
            }
            return new ErrorResult(Messages.BrandNotAdded);
        }

        public IResult Delete(Brand brand)
        {
            var result = _brandDal.Get(b => b.Id == brand.Id);

            if (result != null)
            {
                _brandDal.Delete(result);
                return new SuccessResult(Messages.BrandDeleted);
            }
            return new ErrorResult(Messages.BrandNotFound);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            var result = _brandDal.GetAll();

            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Brand>>(Messages.BrandsListed, result);
            }
            return new ErrorDataResult<List<Brand>>(Messages.BrandNotFound);
        }

        public IResult Update(Brand brand)
        {
            var result = _brandDal.Get(b => b.Id == brand.Id);

            if (result != null)
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
            }
            return new ErrorResult(Messages.BrandNotFound);
        }
    }
}
