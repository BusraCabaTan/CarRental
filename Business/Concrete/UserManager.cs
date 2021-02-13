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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            if (user.FirstName != "" && user.LastName != "")
            {
                _userDal.Add(user);
                return new SuccessResult(Messages.UserAded);
            }
            return new ErrorResult(Messages.UserNotAded);
        }
    }
}
