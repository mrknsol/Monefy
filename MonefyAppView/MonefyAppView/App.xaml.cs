using SimpleInjector;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MonefyAppView.ViewModels;
using MonefyAppView.Views;
using MonefyAppView.Services;
using MonefyAppView.Services.Classes;
using MonefyAppView.Services.Interfaces;
using MonefyAppView.Messages;
using System.Xml.Linq;
using LiveCharts.Wpf.Charts.Base;
using System.Windows.Controls;

namespace MonefyAppView
{
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();
        
        public void Register()
        {
            Container.RegisterSingleton<WalletDataViewModel>();
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<CountOperationViewModel>();
            Container.RegisterSingleton<TransactionViewModel>();
            Container.RegisterSingleton<INavigationService,NavigationService>();

            Container.RegisterSingleton<WalletDataManager>();
            Container.RegisterSingleton<CountOperationManager>();
            Container.RegisterSingleton<TransactionsManager>();
            Container.RegisterSingleton<NewDataMessage>();
            Container.RegisterSingleton<DataMessage>();
            Container.RegisterSingleton<DataService>();
            Container.RegisterSingleton<IMessenger, Messenger>();

            Container.Verify();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            var window = new MainView();
            window.DataContext = Container.GetInstance<MainViewModel>();



            window.ShowDialog();

        }
    }
}