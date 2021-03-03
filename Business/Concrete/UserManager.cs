using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        public IResult Delete(User user)
        {
            var result = _userDal.Get(u => u.Id == user.Id);
            if (result != null)
            {
                _userDal.Delete(result);
                return new SuccessResult(Messages.UserDeleted);
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();

            if (result != null)
            {
                return new SuccessDataResult<List<User>>(Messages.UsersListed , result);
            }
            return new ErrorDataResult<List<User>>(Messages.UserNotFound);
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);

            if (result != null)
            {
                return new SuccessDataResult<User>(Messages.UserListed, result);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);

            if (result != null)
            {
                return new SuccessDataResult<User>(Messages.UserListed, result);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);

            return new SuccessDataResult<List<OperationClaim>>(Messages.UserListed,result);
        }

        public IResult Update(User user)
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
