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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            if(customer != null)
            {
                _customerDal.Add(customer);
                return new SuccessResult(Messages.CustomerAdded);
            }
            return new ErrorResult(Messages.CustomerNotAdded);
        }

        public IResult Delete(Customer customer)
        {
            var result = _customerDal.Get(c => c.Id == customer.Id);

            if (result != null)
            {
                _customerDal.Delete(result);
                return new SuccessResult(Messages.CustomerDeleted);
            }
            return new ErrorResult(Messages.CustomerNotFound);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();

            if (result.Count != 0)
            {
                return new SuccessDataResult<List<Customer>>(Messages.CustomersListed, result);
            }
            return new ErrorDataResult<List<Customer>>(Messages.CustomerNotFound);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(c => c.Id == id);

            if (result != null)
            {
                return new SuccessDataResult<Customer>(Messages.CustomerListed, result);
            }
            return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
        }

        public IResult Update(Customer customer)
        {
            var result = _customerDal.Get(c => c.Id == customer.Id);

            if (result != null)
            {
                _customerDal.Update(customer);
                return new SuccessResult(Messages.CustomerUpdated);
            }
            return new ErrorResult(Messages.CustomerNotFound);
        }
    }
}
