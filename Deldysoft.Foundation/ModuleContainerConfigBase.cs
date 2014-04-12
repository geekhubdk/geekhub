using Autofac;

namespace Deldysoft.Foundation
{
    public abstract class ModuleContainerConfigBase : IModuleContainerConfig
    {
        public void Register(EnvironmentType environment, ContainerBuilder container)
        {
            Container = container;

            RegisterCommon();

            if (environment == EnvironmentType.Development) {
                RegisterDevelopment();
            }
            if (environment == EnvironmentType.Live) {
                RegisterLive();
            }
        }

        public ContainerBuilder Container { get; set; }

        public virtual void RegisterCommon() {}
        public virtual void RegisterDevelopment() {}
        public virtual void RegisterLive() {}
    }
}