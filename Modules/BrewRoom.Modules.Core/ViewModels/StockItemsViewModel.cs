﻿using System;
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

        private IStockFermentableViewModel selectedFermentable;
        private IStockHopViewModel selectedHop;

        #endregion

        #region Ctors
        public StockItemsViewModel(IEventAggregator eventAggregator, IStockItemsRepository stockItemsRepository)
        {
            this.eventAggregator = eventAggregator;
            this.stockItemsRepository = stockItemsRepository;
        }

        #endregion

        #region Properties

        public bool IsFermentableDetailsVisible { get; private set; }

        public bool IsHopDetailsVisible { get; private set; }

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

        public IStockFermentableViewModel SelectedFermentable
        {
            get { return selectedFermentable; }
            set
            {
                selectedFermentable = value;
                eventAggregator.GetEvent<StockItemSelectedEvent>().Publish(selectedFermentable);
                RaisePropertyChanged("SelectedFermentable"); // TODO not tested
            }
        }

        public IStockHopViewModel SelectedHop
        {
            get { return selectedHop; }
            set
            {
                selectedHop = value;
                eventAggregator.GetEvent<StockItemSelectedEvent>().Publish(selectedHop);
                RaisePropertyChanged("SelectedHop"); // TODO not tested
            }
        }

        #endregion

        #region Commands
        public DelegateCommand SelectHops
        {
            get { return new DelegateCommand(ShowHops); }
        }

        public DelegateCommand SelectFermentables
        {
            get { return new DelegateCommand(ShowFermentables); }
        }

        public DelegateCommand SaveFermentableCommand
        {
            get { return new DelegateCommand(SaveFermentable); }
        }

        #endregion

        #region Private Methods
        void ShowHops()
        {
            IsHopDetailsVisible = true;
            IsFermentableDetailsVisible = false;

            SelectedFermentable = null;

            RaisePropertyChanged("IsFermentableDetailsVisible"); // TODO not tested
            RaisePropertyChanged("IsHopDetailsVisible"); // TODO not tested
        }

        void ShowFermentables()
        {
            IsHopDetailsVisible = false;
            IsFermentableDetailsVisible = true;

            SelectedHop = null;

            RaisePropertyChanged("IsFermentableDetailsVisible"); // TODO not tested
            RaisePropertyChanged("IsHopDetailsVisible"); // TODO not tested
        }

        void SaveFermentable()
        {
            var fermentable = selectedFermentable;
            stockItemsRepository.Save(fermentable.Model);
        }

        #endregion
    }
}
