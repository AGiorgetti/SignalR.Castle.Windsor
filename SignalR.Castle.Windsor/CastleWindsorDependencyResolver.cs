using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;

namespace SignalR.Castle.Windsor
{
	public class CastleWindsorDependencyResolver : DefaultDependencyResolver
	{
		private readonly IWindsorContainer _container;

		public CastleWindsorDependencyResolver(IWindsorContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}

			_container = container;
		}

		public override object GetService(Type serviceType)
		{
			if (_container.Kernel.HasComponent(serviceType))
				return _container.Resolve(serviceType);
			return base.GetService(serviceType);
		}

		public override IEnumerable<object> GetServices(Type serviceType)
		{
			IEnumerable<object> objects;
			if (_container.Kernel.HasComponent(serviceType))
				objects = _container.ResolveAll(serviceType).Cast<object>();
			else
				objects = new object[] { };
			return objects.Concat(base.GetServices(serviceType));
		}
	}
}
