using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using MonefyAppView.Messages;
using MonefyAppView.Services.Interfaces;
using MonefyAppView.Messages;

namespace MonefyAppView.Services.Classes;

class NavigationService : INavigationService
{
    private readonly IMessenger _messenger;
    public NavigationService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        _messenger.Send(new NavigationMessage()
        {
            ViewModelType = App.Container.GetInstance<T>()
        });
    }

}
