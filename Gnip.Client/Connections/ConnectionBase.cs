//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

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
