
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/25/2013</date>
// <summary>SandBoxConnection class</summary>

using System;

using Gnip.Data.Common;

namespace Gnip.Client.Connections
{
    public class SandBoxConnection : ConnectionBase
    {
        public SandBoxConnection(string username, string password, string account, GnipSources dataSource)
            : base (username, password, account, dataSource)
        {
            IsLive = true;
        }

        public bool IsLive
        {
            get;
            set;
        }

        public DateTime ReplyFrom
        {
            get;
            set;
        }

        public DateTime ReplyTo
        {
            get;
            set;
        }

        public override string GetRulesAPIURL()
        {
            string url = "https://api.gnip.com:443/accounts/{0}/publishers/{1}/streams/track/{2}/rules.json";
            url = string.Format(url, Account, DataSource.ToString().ToLower(), IsLive ? "prod" : "reply");

            return url;
        }

        public override string GetStreamAPIURL()
        {
            string url = "https://stream.gnip.com:443/accounts/{0}/publishers/{1}/streams/track/{2}.json";
            url = string.Format(url, Account, DataSource.ToString().ToLower(), IsLive ? "prod" : "reply");

            if (!IsLive)
                url = string.Format("{0}?fromDate={1}&toDate={2}", url, ReplyFrom.ToString("yyyyMMddHHmm"), ReplyTo.ToString("yyyyMMddHHmm"));

            return url;
        }
    }
}
