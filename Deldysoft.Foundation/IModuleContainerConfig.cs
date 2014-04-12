using Autofac;

namespace Deldysoft.Foundation
{
    public interface IModuleContainerConfig
    {
        void Register(EnvironmentType environment, ContainerBuilder container);
    }
}