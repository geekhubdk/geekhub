using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Deldysoft.Foundation.CommandHandling;

namespace Deldysoft.Foundation
{
    public static class Bootstrapper
    {
        public static void Configure(Action<BootstrapperFluentOptions> action)
        {
            var options = new BootstrapperFluentOptions();
            action(options);

            if (options.ContainerOptions != null) {
                ConfigureContainer(options.ContainerOptions);
            }
        }

        private static void ConfigureContainer(BootstrapperContainerFluentOptions containerOptions)
        {
            var builder = new ContainerBuilder();

            foreach (var a in containerOptions.AddDefaultBindingsFromAssemblies) {
                builder.RegisterAssemblyTypes(a);
            }

            foreach (var a in containerOptions.AddMvcBindingsFromAssemblies) {
                builder.RegisterControllers(a);
            }

            foreach (var a in containerOptions.AddModuleBindingsFromAssemblies) {
                RegisterAllModuleContainerConfigs(builder, a);
            }

            if (containerOptions.AddWithCommandHandlersFromAssemblies.Any()) {
                RegisterCommandBus(builder, containerOptions.AddWithCommandHandlersFromAssemblies);
            }

            IContainer container = builder.Build();

            if (containerOptions.AddMvcBindingsFromAssemblies.Any()) {
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            }
        }


        private static void RegisterCommandBus(ContainerBuilder container, List<Assembly> assemblies)
        {
            container.Register(x=>CreateCommandBus(x,assemblies)).As<CommandBus>().InstancePerLifetimeScope();
            container.Register(CreateCommandExecuter).As<ICommandExecuter>().InstancePerLifetimeScope();
        }

        private static object CreateCommandExecuter(IComponentContext componentContext)
        {
            var commandExecuter = new CommandExecuter {
                CommandBus = componentContext.Resolve<CommandBus>()
            };
            return commandExecuter;
        }

        private static object CreateCommandBus(IComponentContext componentContext, IEnumerable<Assembly> assemblies)
        {
            var bus = new CommandBus(componentContext.Resolve<ICommandLoggerAdapter>());
            foreach (var assembly in assemblies) {
                foreach (var type in assembly.GetTypes()) {
                    foreach (var handler in type.GetInterfaces()) {
                        if (handler.IsGenericType) {
                            if (handler.GetGenericTypeDefinition() == typeof(IHandleCommand<>)) {
                                var commandType = handler.GetGenericArguments()[0];
                                bus.RegisterHandler(commandType, componentContext.Resolve(type));
                            }
                        }
                    }
                }
            }

            return bus;
        }

        private static void RegisterAllModuleContainerConfigs(ContainerBuilder builder, Assembly assembly)
        {
            var types = assembly
                .GetTypes();

            var classes = types
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Where(x => typeof(IModuleContainerConfig).IsAssignableFrom(x));

            var instances = classes.Select(x => (IModuleContainerConfig)Activator.CreateInstance(x));

            foreach (var instance in instances) {
                instance.Register(AppEnvironment.Current, builder);
            }
        }
    }
}
