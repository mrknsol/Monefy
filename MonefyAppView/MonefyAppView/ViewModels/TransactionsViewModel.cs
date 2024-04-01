using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MonefyAppView.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using MonefyAppView.Messages;
using System;
using System.Collections.Generic;
using LiveCharts;
using MonefyAppView.Services.Classes;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;
using MonefyAppView.Services;
using MonefyAppView.Services.Interfaces;
using System.Transactions;

namespace MonefyAppView.ViewModels
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly INavigationService _navigationService;
        private readonly DataService _dataService;
        private ViewModelBase _currentView;

        public ObservableCollection<TransactionsModel> Transactions { get; set; } = new();



        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                Set(ref _currentView, value);
            }
        }

        public TransactionViewModel(IMessenger messenger, INavigationService navigationService, DataService dataService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            _dataService = dataService;

            _dataService.SendData(Transactions);
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                if (_count != value)
                {
                    _count = value;
                    Set(ref _count, value);
                }
            }
        }

        public RelayCommand CancelCommand
        {
            get => new(() =>
            {
                _messenger.Send<NavigationMessage>(null);
            });
        }
        
    }
}