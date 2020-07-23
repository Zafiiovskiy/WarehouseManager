using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDesktopUI.Library.DataManaging.Models;
using WMDesktopUI.Models;

namespace WMDesktopUI.Events
{
    public class OrderDetailsEventModel
    {
        public ClientModel Client { get; set; }
        
        public OrderDetailsEventModel(ClientModel client)
        {
            Client = client;
        }
    }
}
