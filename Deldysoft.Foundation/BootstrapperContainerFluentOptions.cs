using System.Collections.Generic;
using System.Reflection;

namespace Deldysoft.Foundation
{
    public class BootstrapperContainerFluentOptions
    {
        public List<Assembly> AddDefaultBindingsFromAssemblies { get; private set; }
        public List<Assembly> AddMvcBindingsFromAssemblies { get; private set; }
        public List<Assembly> AddModuleBindingsFromAssemblies { get; private set; }
        public List<Assembly> AddWithCommandHandlersFromAssemblies { get; private set; }
        
        public BootstrapperContainerFluentOptions()
        {
            AddDefaultBindingsFromAssemblies = new List<Assembly>();
            AddMvcBindingsFromAssemblies = new List<Assembly>();
            AddModuleBindingsFromAssemblies = new List<Assembly>();
            AddWithCommandHandlersFromAssemblies = new List<Assembly>();
        }

        public BootstrapperContainerFluentOptions WithDefaultBindings(Assembly assembly)
        {
            AddDefaultBindingsFromAssemblies.Add(assembly);
            return this;
        }

        public BootstrapperContainerFluentOptions WithMvcBindings(Assembly assembly)
        {
            AddMvcBindingsFromAssemblies.Add(assembly);
            return this;
        }

        public BootstrapperContainerFluentOptions WithModuleBindings(Assembly assembly)
        {
            AddModuleBindingsFromAssemblies.Add(assembly);
            return this;
        }

        public BootstrapperContainerFluentOptions WithCommandHandlers(Assembly assembly)
        {
            AddWithCommandHandlersFromAssemblies.Add(assembly);
            return this;
        }
    }
}