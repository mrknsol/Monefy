using MonefyAppView.Services.Interfaces;
using MonefyAppView.Models;
using GalaSoft.MvvmLight.Messaging;
using MonefyAppView.Messages;
using System.Windows.Media;

namespace MonefyAppView.Services
{
    public class DataService : IDataService
    {
        public readonly IMessenger _messenger;

        public DataService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void NewSendData(object[] data)
        {
            _messenger.Send(new NewDataMessage()
            {
                Datas = data
            });
        }
        public void SendData(object data)
        {
            _messenger.Send(new DataMessage()
            {
                Data = data
            });
        }
        public void SendInfo(object[] info)
        {
            _messenger.Send(new InfoDataMessage()
            {
                Infoes = info
            });
        }
        public void SendDatas(object data)
        {
            _messenger.Send(new DatasMessage()
            {
                Data = data
            });
        }
    }
}