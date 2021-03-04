using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
             using (TransactionScope scope = new TransactionScope())
             {
                try
                {
                    invocation.Proceed();
                    scope.Complete();
                } 
                catch (Exception e)
                {
                    scope.Dispose();
                    throw;
                }
             }
        }
    }
}
