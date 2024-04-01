using GalaSoft.MvvmLight;

namespace MonefyAppView.Services.Interfaces;

public interface INavigationService
{
    public void NavigateTo<T>() where T : ViewModelBase;
}
