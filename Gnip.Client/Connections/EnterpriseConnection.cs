
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/25/2013</date>
// <summary>SandBoxConnection class</summary>

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
