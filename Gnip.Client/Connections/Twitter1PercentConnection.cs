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
    public class Twitter1PercentConnection : SandBoxConnection
    {
        public Twitter1PercentConnection(string username, string password, string account, GnipSources dataSource = GnipSources.Twitter)
            : base(username, password, account, dataSource)
        {
        }

        public override string GetRulesAPIURL()
        {
            string url = "https://api.gnip.com:443/accounts/{0}/publishers/twitter/streams/sample1track/{1}/rules.json";
            url = string.Format(url, Account, IsLive ? "prod" : "reply");

            return url;
        }

        public override string GetStreamAPIURL()
        {
            string url = "https://stream.gnip.com:443/accounts/{0}/publishers/twitter/streams/sample1track/{1}.json";
            url = string.Format(url, Account, IsLive ? "prod" : "reply");

            if (!IsLive)
                url = string.Format("{0}?fromDate={1}&toDate={2}", url, ReplyFrom.ToString("yyyyMMddHHmm"), ReplyTo.ToString("yyyyMMddHHmm"));

            return url;
        }
    }
}
