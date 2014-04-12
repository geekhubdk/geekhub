using System;
using System.Reflection;
using Autofac;
using Deldysoft.Foundation;
using Deldysoft.Foundation.CommandHandling;
using Geekhub.App.Core.Adapters;
using Geekhub.App.Core.CommandHandling;
using Geekhub.App.Core.Data;

namespace Geekhub.App.Core.Config
{
    public class CoreContainerConfig : ModuleContainerConfigBase
    {
        public override void RegisterCommon()
        {
            Container.RegisterType<CommandLogger>().As<ICommandLoggerAdapter>();
            Container.Register(c => DataContext.Current).As<DataContext>();
            Container.RegisterType<SmtpEmailAdapter>().As<IEmailAdapter>();
        }

    }
}