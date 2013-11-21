
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/25/2013</date>
// <summary>Twitter1PercentConnection class</summary>

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
