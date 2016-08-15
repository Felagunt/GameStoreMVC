using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using System.Configuration;
using GameStore.Domain.Entities;
using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;

namespace GameStore.WebUI.Infrastructure
{
    /// <summary>
    /// well it's ninject)
    /// </summary>
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        /// <summary>
        /// this bindings 
        /// </summary>
        private void AddBindings()
        {
            kernel.Bind<IGameRepository>().To<EFGameRepository>();
        }
    }
}