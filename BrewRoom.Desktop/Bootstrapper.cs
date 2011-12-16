using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using BrewRoom.Modules.Core;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using NHibernate;
using Prism.Extensions.FluentNH;
using Prism.Extensions.FluentNH.Services;
using Prism.Extensions.FluentNH.Services.interfaces;

namespace BrewRoom.Desktop
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var shell = new MainWindow();
            shell.Show();
            return shell;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IMappingRegistryService, MappingRegistryService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISessionFactory>();
            Container.RegisterType<ISession>();

            var sessionFactory = Session.CreateSessionFactory(Container, ConfigurationManager.ConnectionStrings["cnn"].ConnectionString, true);
            Container.RegisterInstance(sessionFactory, new ContainerControlledLifetimeManager());
        
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(CoreModule));
        }
    }
}
