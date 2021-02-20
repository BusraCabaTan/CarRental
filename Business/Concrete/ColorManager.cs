using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
                _colorDal.Add(color);
                return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Delete(Color color)
        {
            var result = _colorDal.Get(b => b.Id == color.Id);

            if (result != null)
            {
                _colorDal.Delete(result);
                return new SuccessResult(Messages.ColorDeleted);
            }
            return new ErrorResult(Messages.ColorNotFound);
        }

        public IDataResult<List<Color>> GetAll()
        {
            var result = _colorDal.GetAll();

            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Color>>(Messages.ColorsListed, result);
            }
            return new ErrorDataResult<List<Color>>(Messages.ColorNotFound);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Color color)
        {
            var result = _colorDal.Get(b => b.Id == color.Id);

            if (result != null)
            {
                _colorDal.Update(color);
                return new SuccessResult(Messages.ColorUpdated);
            }
            return new ErrorResult(Messages.ColorNotFound);
        }
    }
}
