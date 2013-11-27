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
    public class EnterpriseConnection : ConnectionBase
    {
        public EnterpriseConnection(string username, string password, string account, GnipSources dataSource)
            : base(username, password, account, dataSource)
        {
            Timeout = 60000;
            DataFormat = GnipDataFormat.XML;
            UseEncoding = false;
        }

        public string ConnectionId
        {
            get;
            set;
        }

        public override string GetRulesAPIURL()
        {
            string url = "https://{0}.gnip.com/data_collectors/{1}/rules.json";
            url = string.Format(url, Account.ToLower(), ConnectionId);

            return url;
        }

        public override string GetStreamAPIURL()
        {
            string url = "https://{0}.gnip.com/data_collectors/{1}/stream.json";
            url = string.Format(url, Account.ToLower(), ConnectionId);

            return url;
        }
    }
}
