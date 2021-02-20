using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {

            _userDal.Add(user);
            return new SuccessResult(Messages.UserAded);
        
        }

        IResult IUserService.Delete(User user)
        {
            var result = _userDal.Get(u => u.Id == user.Id);
            if (result != null)
            {
                _userDal.Delete(result);
                return new SuccessResult(Messages.UserDeleted);
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        IDataResult<List<User>> IUserService.GetAll()
        {
            var result = _userDal.GetAll();

            if (result != null)
            {
                return new SuccessDataResult<List<User>>(Messages.UsersListed , result);
            }
            return new ErrorDataResult<List<User>>(Messages.UserNotFound);
        }

        IDataResult<User> IUserService.GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);

            if (result != null)
            {
                return new SuccessDataResult<User>(Messages.UserListed, result);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        IResult IUserService.Update(User user)
        {
            var result = _userDal.Get(u => u.Id == user.Id);
            if (result != null)
            {
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            return new ErrorResult(Messages.UserNotFound);
        }
    }
}
