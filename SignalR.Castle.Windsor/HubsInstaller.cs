using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Reflection;
using Microsoft.AspNet.SignalR;

namespace SignalR.Castle.Windsor
{
	public class HubsInstallers : IWindsorInstaller
	{
		private readonly FromAssemblyDescriptor _descriptor;

		/// <summary>
		/// registers all the hubs contained in the passed in assembly
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		public HubsInstallers(Assembly assembly)
		{
			_descriptor = AllTypes.FromAssembly(assembly);
		}

		/// <summary>
		/// registers all the hubs contained in the assembly to which the specified type belongs to
		/// </summary>
		/// <param name="typeContainedInAssembly"></param>
		public HubsInstallers(Type typeContainedInAssembly)
		{
			_descriptor = AllTypes.FromAssemblyContaining(typeContainedInAssembly);
		}
		
		/// <summary>
		/// registers all the hubs in the assemblies specified by the filter
		/// </summary>
		/// <param name="filter">The filter.</param>
		public HubsInstallers(AssemblyFilter filter)
		{
			_descriptor = AllTypes.FromAssemblyInDirectory(filter);
		}

		/// <summary>
		/// registers all the hubs in the assembly specified by the assembly name parameter
		/// </summary>
		/// <param name="assemblyName">Name of the assembly.</param>
		public HubsInstallers(string assemblyName)
		{
			_descriptor = AllTypes.FromAssemblyNamed(assemblyName);
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(_descriptor.BasedOn(typeof(Hub)));
		}
	}
}
