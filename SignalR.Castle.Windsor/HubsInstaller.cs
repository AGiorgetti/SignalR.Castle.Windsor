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
			_descriptor = Types.FromAssembly(assembly);
		}

		/// <summary>
		/// registers all the hubs contained in the assembly to which the specified type belongs to
		/// </summary>
		/// <param name="typeContainedInAssembly"></param>
		public HubsInstallers(Type typeContainedInAssembly)
		{
			_descriptor = Types.FromAssemblyContaining(typeContainedInAssembly);
		}
		
		/// <summary>
		/// registers all the hubs in the assemblies specified by the filter
		/// </summary>
		/// <param name="filter">The filter.</param>
		public HubsInstallers(AssemblyFilter filter)
		{
			_descriptor = Types.FromAssemblyInDirectory(filter);
		}

		/// <summary>
		/// registers all the hubs in the assembly specified by the assembly name parameter
		/// </summary>
		/// <param name="assemblyName">Name of the assembly.</param>
		public HubsInstallers(string assemblyName)
		{
			_descriptor = Types.FromAssemblyNamed(assemblyName);
		}

        /// <summary>
        /// todo: first raw implementation... add options for lifestyle management, singleton might not be the best one 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="store"></param>
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(_descriptor.BasedOn(typeof(Hub)));
		}
	}
}
