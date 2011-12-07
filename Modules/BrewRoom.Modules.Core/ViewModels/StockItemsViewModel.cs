using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Events;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class StockItemsViewModel : NotificationObject, IStockItemsViewModel
    {
        #region Fields

        readonly IEventAggregator eventAggregator;
        readonly IStockItemsRepository stockItemsRepository;
        #endregion

        #region Ctors
        public StockItemsViewModel(IEventAggregator eventAggregator, IStockItemsRepository stockItemsRepository)
        {
            this.eventAggregator = eventAggregator;
            this.stockItemsRepository = stockItemsRepository;
        }

        #endregion

        #region Properties
        public IList<IFermentableViewModel> Fermentables
        {
            get
            {
                var result = new List<IFermentableViewModel>();

                var fermentables = stockItemsRepository.GetGrains().ToList();
                fermentables.ForEach(x => result.Add(new FermentableViewModel(x)));

                return result;
            }
        }

        public IList<IHopViewModel> Hops
        {
            get
            {
                var result = new List<IHopViewModel>();

                var hops = stockItemsRepository.GetHops().ToList();
                hops.ForEach(x => result.Add(new HopViewModel(x)));

                return result;
            }
        }

        IFermentableViewModel selectedFermentable;
        public IFermentableViewModel SelectedFermentable
        {
            get { return selectedFermentable; }
            set
            {
                selectedFermentable = value;
                eventAggregator.GetEvent<StockFermentableSelectedEvent>().Publish(selectedFermentable);
                RaisePropertyChanged("SelectedFermentable"); // TODO not tested
            }
        }

        IHopViewModel selectedHop;
        public IHopViewModel SelectedHop
        {
            get { return selectedHop; }
            set
            {
                selectedHop = value;
                RaisePropertyChanged("SelectedHop"); // TODO not tested
            }
        }

        public DelegateCommand SelectHops
        {
            get { return new DelegateCommand(ShowHops); }
        }

        void ShowHops()
        {
            IsHopDetailsVisible = true;
            IsFermentableDetailsVisible = false;

            RaisePropertyChanged("IsFermentableDetailsVisible");
            RaisePropertyChanged("IsHopDetailsVisible");
        }

        public DelegateCommand SelectFermentables
        {
            get { return new DelegateCommand(ShowFermentables); }
        }

        void ShowFermentables()
        {
            IsHopDetailsVisible = false;
            IsFermentableDetailsVisible = true;

            RaisePropertyChanged("IsFermentableDetailsVisible");
            RaisePropertyChanged("IsHopDetailsVisible");
        }

        public bool IsFermentableDetailsVisible { get; private set; }

        public bool IsHopDetailsVisible { get; private set; }

        #endregion
    }
}
