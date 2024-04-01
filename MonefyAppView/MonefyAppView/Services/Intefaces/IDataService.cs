using MonefyAppView.Models;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyAppView.Services.Interfaces;

interface IDataService
{
    public void NewSendData(object[] data);
    public void SendData(object data);

}
