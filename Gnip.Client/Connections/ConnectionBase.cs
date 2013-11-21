
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/25/2013</date>
// <summary>ConnectionBase class</summary>

using Gnip.Data.Common;

namespace Gnip.Client.Connections
{
    public abstract class ConnectionBase
    {
        public ConnectionBase(string username, string password, string account, GnipSources dataSource)
        {
            Username = username;
            Password = password;
            Account = account;
            DataSource = dataSource;
            DataFormat = GnipDataFormat.Json;

            Timeout = 30000;
            UseEncoding = true;
        }

        public string Username
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public string Account
        {
            get;
            private set;
        }

        public GnipSources DataSource
        {
            get;
            private set;
        }

        public GnipDataFormat DataFormat
        {
            get;
            set;
        }

        public int Timeout
        {
            get;
            set;
        }

        public bool UseEncoding
        {
            get;
            set;
        }

        public abstract string GetRulesAPIURL();

        public abstract string GetStreamAPIURL();
    }
}
