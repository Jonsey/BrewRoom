using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BrewRoom.Modules.Core;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;

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
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(CoreModule));
        }
    }
}
