using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts.Wpf;
using MonefyAppView.Services;
using MonefyAppView.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MonefyAppView.Messages;
using MonefyAppView.Services.Classes;
using System.Windows.Media;
using LiveCharts;
using MonefyAppView.Models;
using System.Collections.ObjectModel;

namespace MonefyAppView.ViewModels;

internal class WalletDataViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private ViewModelBase _currentView;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set
        {
            Set(ref _currentView, value);
        }
    }
    public PieChart Chart { get; set; } = new();
    private Button MyButton = new();
    private readonly WalletDataManager _walletDataManager;



    private readonly INavigationService _navigationService;
    private readonly DataService _dataService;
    private readonly TransactionsManager _transactionsManager;

    private DateTime currentDate = DateTime.Today;
    public Button myButton = new();
    public readonly DataManager dataManager = new();

    public DateTime CurrentDate
    {
        get { return currentDate; }
        set
        {
            if (Set(ref currentDate, value))
            {

                _walletDataManager.UpdateChartForCurrentDate(ref currentDate, Chart, myButton);
            }
        }
    }

    private double _buttonContent;
    public double ButtonContent
    {
        get { return _buttonContent; }
        set
        {
            Set(ref _buttonContent, value);
        }
    }
    public WalletDataViewModel(INavigationService navigationService, DataService dataService, IMessenger messenger, TransactionsManager transactionsManager, WalletDataManager walletDataManager)
    {
        _buttonContent = 0;
        _navigationService = navigationService;
        _dataService = dataService;
        _messenger = messenger;
        _walletDataManager = walletDataManager;
        _transactionsManager = transactionsManager;
        _messenger.Register<NavigationMessage>(this, message =>
        {
            if (message == null)
            {
                CurrentView = null;
            }
        });
        _messenger.Register<InfoDataMessage>(this, message =>
        {
            myButton = message.Infoes[0] as Button;
            ButtonContent = (double)message.Infoes[1];
        });

    }

    public RelayCommand ChangeDateBack_Button => new(() =>
    {
        _transactionsManager.SaveTransactionsForDate(currentDate);
        _walletDataManager.chartValuesByDate[currentDate] = new Dictionary<Color, double>(_walletDataManager.ChartValues);

        DateTime newDate = CurrentDate.AddDays(-1);

        if (_transactionsManager.HasTransactionForDate(newDate))
        {
            _transactionsManager.LoadTransactionsForDate(newDate);
            _walletDataManager.UpdateChartForCurrentDate(ref newDate, Chart, myButton);
            CurrentDate = newDate;
        }
        else
        {
            Chart.Series.Clear();
            _walletDataManager.ChartValues.Clear();
            _walletDataManager.UpdateChart(Chart, myButton);

            _transactionsManager._transactions.Clear();
            CurrentDate = newDate;
        }
    });


    public RelayCommand ChangeDateForward_Button => new(() =>
    {
        _transactionsManager.SaveTransactionsForDate(currentDate);
        _walletDataManager.chartValuesByDate[currentDate] = new Dictionary<Color, double>(_walletDataManager.ChartValues);

        DateTime newDate = CurrentDate.AddDays(1);

        if (_transactionsManager.HasTransactionForDate(newDate))
        {
            _transactionsManager.LoadTransactionsForDate(newDate);
            _walletDataManager.UpdateChartForCurrentDate(ref newDate, Chart, myButton);
            CurrentDate = newDate;
        }
        else
        {
            Chart.Series.Clear();
            _walletDataManager.ChartValues.Clear();
            _walletDataManager.UpdateChart(Chart, myButton);
            _transactionsManager._transactions.Clear();
            CurrentDate = newDate;
        }
    });

   


    public RelayCommand TransactionNavigate
    {
        get => new(() =>
        {
            CurrentView = App.Container.GetInstance<TransactionViewModel>();
        });
    }

    public RelayCommand ExitSaveCommand
    {
        get => new(() =>
        {
            dataManager.SaveData(_walletDataManager.ChartValues, _walletDataManager.chartValuesByDate);
        });
    }

    public RelayCommand LoadDatas
    {
        get => new(() =>
        {
            var (chartValues, chartValuesByDate) = dataManager.LoadData();

            _walletDataManager.ChartValues = chartValues;
            _walletDataManager.chartValuesByDate = chartValuesByDate;

            _walletDataManager.UpdateChart(Chart, MyButton);
        });
    }

    public RelayCommand<Button> OperationButton
    {
        get => new(
            (button) =>
            {
                if (button.Name == "Plus" || button.Name == "Minus")
                {
                    _dataService.NewSendData(new object[] { button, Chart, currentDate, _buttonContent });
                    _navigationService.NavigateTo<CountOperationViewModel>();
                }
                else
                {
                    SolidColorBrush btnBackground = button.Foreground as SolidColorBrush;
                    if (btnBackground != null)
                    {
                        _dataService.NewSendData(new object[] { button, Chart, currentDate, _buttonContent });
                        _navigationService.NavigateTo<CountOperationViewModel>();
                    }
                }

            });
    }
}