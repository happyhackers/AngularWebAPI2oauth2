using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using AngularWebAPI2oauth2.DAL;

namespace AngularWebAPI2oauth2.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer Container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer();

            //container.RegisterType<IRepository<T>, Repository<T>>(new InjectionConstructor(typeof(IUnitOfWork)));

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}