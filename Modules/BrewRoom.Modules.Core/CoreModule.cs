using System;
using System.Collections.Generic;
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

namespace BrewRoom.Modules.Core
{
    public class CoreModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public CoreModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterTypesAndServices();
            RegisterViewsWithRegions();
        }

        void RegisterTypesAndServices()
        {
            container
                .RegisterType<IStockItemsRepository, StockItemsRepository>(new ContainerControlledLifetimeManager());

            container
                .RegisterType<IEditRecipeView, EditRecipeView>(new ContainerControlledLifetimeManager())
                .RegisterType<IEditRecipeViewModel, EditRecipeViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<IStockItemsViewModel, StockItemsViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<IFermentableViewModel, FermentableViewModel>(new ContainerControlledLifetimeManager());
        }

        void RegisterViewsWithRegions()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(IEditRecipeView));
        }
    }
}
