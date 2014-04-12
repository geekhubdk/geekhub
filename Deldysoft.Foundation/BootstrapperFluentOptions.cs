using System;

namespace Deldysoft.Foundation
{
    public class BootstrapperFluentOptions
    {
        public bool AddContainer { get; private set; }
        public BootstrapperContainerFluentOptions ContainerOptions { get; private set; }


        public void WithContainer(Action<BootstrapperContainerFluentOptions> action)
        {
            ContainerOptions = new BootstrapperContainerFluentOptions();
            action(ContainerOptions);
        }
    }
}