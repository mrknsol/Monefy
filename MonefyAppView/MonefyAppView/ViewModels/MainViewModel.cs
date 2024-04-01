using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonefyAppView.Views;
using GalaSoft.MvvmLight.Messaging;
using MonefyAppView.Messages;
using MonefyAppView.ViewModels;
using MonefyAppView;
using LiveCharts.Wpf;
using System.Windows.Controls;
using LiveCharts.Wpf.Charts.Base;
using MonefyAppView.Services.Classes;

namespace MonefyAppView.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly IMessenger _messenger;


        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                Set(ref _currentView, value);
            }
        }

        //public MainViewModel()
        //{
        //    CurrentView = App.Container.GetInstance<WalletDataModel>();
        //}

        public MainViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            CurrentView = App.Container.GetInstance<WalletDataViewModel>();

            _messenger.Register<NavigationMessage>(this, message =>
            {
                if (message != null)
                {
                    CurrentView = message.ViewModelType;
                }
            });

        }

        //public RelayCommand NavigateWindow
        //{
        //    get => new(() =>
        //    {
        //        CurrentView = App.Container.GetInstance<Transactions>
        //    })
        //}
    }
}
