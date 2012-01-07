using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Interfaces.Views;
using BrewRoom.Modules.Core.Repositories;
using BrewRoom.Modules.Core.ViewModels;
using BrewRoom.Modules.Core.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Prism.Extensions.FluentNH;

namespace BrewRoom.Modules.Core
{
    public class CoreModule : FluentModule<CoreModule>, IModule
    {
        public CoreModule(IUnityContainer container, IRegionManager regionManager): base(container, regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        protected override string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[Environment.MachineName + "-cnn"].ConnectionString;
        }

        protected override void RegisterTypesAndServices()
        {
            container
                .RegisterType<IStockItemsRepository, StockItemsRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IRecipeRepository, RecipeRepository>(new ContainerControlledLifetimeManager());

            container
                .RegisterType<IEditRecipeView, EditRecipeView>(new ContainerControlledLifetimeManager())
                .RegisterType<IEditRecipeViewModel, EditRecipeViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<IStockItemsViewModel, StockItemsViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<IFermentableViewModel, FermentableViewModel>(new ContainerControlledLifetimeManager());
        }

        protected override void RegisterViewsWithRegions()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(IEditRecipeView));
        }
    }
}
