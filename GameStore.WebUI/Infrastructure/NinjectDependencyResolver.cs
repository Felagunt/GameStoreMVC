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
            //fake repo
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {Name="SimCity",Price=1499 },
                new Game {Name="TITANFALL", Price=2299 },
                new Game {Name="Battlefield 4",Price=899.4m }
            });
            //all time return fake repo when ninject recive request to imp IGameRepository
            kernel.Bind<IGameRepository>().ToConstant(mock.Object);
        }
    }
}