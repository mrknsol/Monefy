using MonefyAppView.Services.Interfaces;
using MonefyAppView.ViewModels;
using System;
using System.Windows.Media;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MonefyAppView.Messages;
using LiveCharts.Wpf;
using System.Windows.Controls;
using MonefyAppView.Models;
using System.Transactions;

namespace MonefyAppView.Services.Classes
{
    internal class CountOperationManager
    {
        public Button senderButton = new();
        public PieChart MyChart = new();
        private double Balance = new();

        private readonly WalletDataManager _walletDataManager = new();
        private readonly TransactionsManager _transactionManager;


        private readonly IMessenger _messenger;
        private readonly INavigationService _navigationService;
        private readonly DataService _dataService;
        private WalletDataViewModel _walletDataViewModel;


        private DateTime currentDate = DateTime.Today;
        public double _buttonContent { get; set; }

        public CountOperationManager(IMessenger messenger, INavigationService navigationService, DataService dataService, WalletDataManager walletDataManager, WalletDataViewModel walletDataViewModel,TransactionsManager transactionsManager)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            _dataService = dataService;
            _walletDataManager = walletDataManager;
            _transactionManager = transactionsManager;
            _walletDataViewModel = walletDataViewModel;

            _messenger.Register<DatasMessage>(this, message =>
            {
                if (message.Data != null)
                {
                    Balance = (double)message.Data;

                }
            });
        }
        public void AddElement(Button _button, PieChart _chart, ref double buttonBalance)
        {
            if (_button.Name == "Plus")
            {
                _navigationService.NavigateTo<WalletDataViewModel>();
                buttonBalance += Balance;
                MessageBox.Show($"{Balance}for +");
            }
            else if (_button.Name == "Minus")
            {
                _navigationService.NavigateTo<WalletDataViewModel>();
                buttonBalance -= Balance;
                MessageBox.Show($"{Balance}for -");
            }
            else
            {
                _navigationService.NavigateTo<WalletDataViewModel>();
                MessageBox.Show($"{Balance}");
                SolidColorBrush btnBackground = _button.Foreground as SolidColorBrush;

                if (_walletDataManager.ChartValues.ContainsKey(btnBackground.Color))
                {
                    _walletDataManager.ChartValues[btnBackground.Color] += Balance;
                }
                else
                {
                    _walletDataManager.ChartValues.Add(btnBackground.Color, Balance);
                }
                buttonBalance -= Balance;
                _transactionManager.AddTransactions(Balance, _button.Name.ToString(), _walletDataViewModel.CurrentDate);
                _walletDataManager.UpdateChart(_chart, _button);
            }
            
            MessageBox.Show("Added");
        }

    }
}
