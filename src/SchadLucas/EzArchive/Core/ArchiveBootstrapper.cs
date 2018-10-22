using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using SchadLucas.EzArchive.Shell;
using SchadLucas.EzArchive.Storage;
using SchadLucas.Logging;
using SchadLucas.Wpf.InversionOfControl;
using LogManager = NLog.LogManager;

namespace SchadLucas.EzArchive.Core
{
    internal class ArchiveBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public ArchiveBootstrapper()
        {
            LogManager.Configuration = new AppLoggingConfiguration();
            Initialize();
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();

            EnforceNamespaceConvention = false;
            EnforceViewModelBaseType = false;
            AutoSubscribeEventAggegatorHandlers = true;

            WindowSettings.Add("Width", 1024);
            WindowSettings.Add("Height", 768);
            WindowSettings.Add("SizeToContent", SizeToContent.Manual);
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterModule<EzLogModule>();
            builder.RegisterModule<ContainerEventModule>();
            builder.RegisterModule<FactoryModule>();
            builder.RegisterModule(new ServiceModule(AssemblySource.Instance.ToArray()));

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                   .Where(t => t.Name.EndsWith("Model"))
                   .Where(t => !t.Name.EndsWith("ViewModel"))
                   .Where(x => { return true; })
                   .AsSelf()
                   .InstancePerDependency();

            builder.RegisterModule<EzDatabaseModule>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return AssemblyCache.Assemblies;
        }
    }
}