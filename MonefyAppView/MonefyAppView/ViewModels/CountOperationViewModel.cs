using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using MonefyAppView.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using MonefyAppView.Services;
using MonefyAppView.Services.Classes;
using System.Windows.Controls;
using System.Windows;
using MonefyAppView.Messages;
using LiveCharts.Wpf;
using System.Windows.Media;
using LiveCharts.Wpf.Charts.Base;

namespace MonefyAppView.ViewModels;

class CountOperationViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private string _currentText = "";
    public string _currentNote = "";
    private readonly DataService _dataService;
    Color btnColor = new();


    public Button senderButton = new();
    public PieChart MyChart = new();
    private DateTime currentDate = DateTime.Today;
    public double _buttonsContent;


    private double Balance = new();


    private readonly WalletDataManager _walletDataManager;
    private CountOperationManager _countOperationManager;

    public CountOperationViewModel(IMessenger messenger, INavigationService navigationService, DataService dataService, CountOperationManager countOperationManager, WalletDataManager walletDataManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _dataService = dataService;
        _countOperationManager = countOperationManager;
        _walletDataManager = walletDataManager;

        _messenger.Register<NewDataMessage>(this, message =>
        {
            if (message.Datas.Length == 4)
            {
                senderButton = message.Datas[0] as Button;
                MyChart = message.Datas[1] as PieChart;
                currentDate = (DateTime)message.Datas[2];
                _buttonsContent = (double)message.Datas[3];
            }
        });

    }
    public DateTime CurrentDate
    {
        get { return currentDate; }
        set
        {
            if (Set(ref currentDate, value))
            {

                _walletDataManager.UpdateChartForCurrentDate(ref currentDate, _countOperationManager.MyChart, senderButton);
            }
        }
    }

    public RelayCommand CancelCommand
    {
        get => new(() =>
        {
            _navigationService.NavigateTo<WalletDataViewModel>();
        }

        );
    }

    public RelayCommand<string> Add
    {
        get => new((data) =>
        {
            CurrentText += data;
        });
    }

    public string CurrentText
    {
        get { return _currentText; }
        set { Set(ref _currentText, value); }
    }

    public string CurrentNote
    {
        get { return _currentNote; }
        set { Set(ref _currentNote, value); }
    }

    public RelayCommand Equal
    {
        get => new(() =>
        {
            Balance = double.Parse(new System.Data.DataTable().Compute(CurrentText, null).ToString());
            CurrentText = Balance.ToString();
        });
    }
    public RelayCommand AddElement
    {
        get => new(() =>
        {
            Balance = double.Parse(new System.Data.DataTable().Compute(CurrentText, null).ToString());
            CurrentText = Balance.ToString();
            _dataService.SendDatas( Balance);
            _countOperationManager.AddElement(senderButton,MyChart,ref _buttonsContent);
            CurrentText = "";

            _dataService.SendInfo(new object[] { senderButton, _buttonsContent });
        });                                                           
    }

    public RelayCommand DeleteData
    {
        get => new(() =>
        {
            if (CurrentText != string.Empty)
            {
                CurrentText = CurrentText.Substring(0, CurrentText.Length - 1);
            }
        }

            );
    }

}
