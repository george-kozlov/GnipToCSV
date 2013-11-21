using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnip.Client.Common
{
    public class HeartbeatEventArgs : EventArgs 
    {
        public string Message { get; set; }
    }
}
