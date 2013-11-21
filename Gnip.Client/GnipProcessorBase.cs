
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/03/2013</date>
// <summary>GnipProcessorBase class</summary>

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

using Gnip.Data;
using Gnip.Data.Common;
using Gnip.Client.Connections;
using Gnip.Client.Formaters;

namespace Gnip.Client.Common
{
    public delegate void ResultReceived(object sender, DynamicActivityBase activity);
    public delegate void ErrorHappened(object sender, Exception ex);
    public delegate void HeartbeatReceived(object sender, HeartbeatEventArgs e);

    public abstract class GnipProcessorBase
    {
        #region Private members

        ConnectionBase _connection;
        IFormatter _formatter;

        bool _cancelFlag = false;

        static Dictionary<GnipSources, Type> _types = new Dictionary<GnipSources, Type>();

        #endregion

        #region Constructor

        static GnipProcessorBase()
        {
            _types.Add(GnipSources.Twitter, typeof(TwitterActivity));
            _types.Add(GnipSources.Facebook, typeof(FacebookActivity));
            _types.Add(GnipSources.YouTube, typeof(YouTubeActivity));
            _types.Add(GnipSources.GooglePlus, typeof(GooglePlusActivity));
            _types.Add(GnipSources.Reddit, typeof(RedditActivity));
        }

        public GnipProcessorBase(ConnectionBase connection)
        {
            _connection = connection;

            if (_connection.DataFormat == GnipDataFormat.Json)
                _formatter = new JsonFormatter();
            else if (_connection.DataFormat == GnipDataFormat.XML)
                _formatter = new XMLFormatter();
        }

        #endregion

        #region Static methods

        public static T CreateGnipProcessor<T>(ConnectionBase connection)
        {
            return ReflectionHelper.CreateAndCastInstance<T>(typeof(T), new object[] { connection });
        }

        public static Type GetObjectTypeByService(GnipSources service)
        {
            return _types[service];
        }

        #endregion

        #region Public & prottected properties

        public bool isCanceled
        {
            get
            {
                return _cancelFlag;
            }
            internal set
            {
                _cancelFlag = value;
            }
        }

        public ConnectionBase Connection
        {
            get
            {
                return _connection;
            }
        }

        protected IFormatter Formatter
        {
            get
            {
                return _formatter;
            }
        }

        public event ResultReceived DataReceived;
        public event ErrorHappened ErrorHappened;
        public event HeartbeatReceived HeartbeatReceived;

        #endregion

        #region Public methods

        public abstract void BeginStreaming();
        public abstract void EndStreaming();

        public List<MatchingRule> GetRules()
        {
            List<MatchingRule> rules = new List<MatchingRule>();
            HttpWebRequest request = GetAPIRequest("GET");

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                using (MemoryStream memory = new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd())))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MatchingRulesCollection));
                    rules = (serializer.ReadObject(memory) as MatchingRulesCollection).Rules;
                }
            }
            catch (Exception ex)
            {
                OnErrorHappened(ex);
            }

            return rules;
        }

        public void AddRules(List<MatchingRule> rules)
        {
            if (rules == null || rules.Count == 0)
                return;

            HttpWebRequest request = GetAPIRequest("POST");

            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MatchingRulesCollection));
                    serializer.WriteObject(memStream, new MatchingRulesCollection() { Rules = rules });

                    request.ContentLength = memStream.Length;
                    Stream writer = request.GetRequestStream();
                    memStream.WriteTo(writer);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                }
            }
            catch (Exception ex)
            {
                OnErrorHappened(ex);
            }
        }

        public void RemoveRules(List<MatchingRule> rules)
        {
            if (rules == null || rules.Count == 0)
                return;

            HttpWebRequest request = GetAPIRequest("DELETE");

            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MatchingRulesCollection));
                    serializer.WriteObject(memStream, new MatchingRulesCollection() { Rules = rules });

                    request.ContentLength = memStream.Length;
                    Stream writer = request.GetRequestStream();
                    memStream.WriteTo(writer);

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                }
            }
            catch (Exception ex)
            {
                OnErrorHappened(ex);
            }
        }

        #endregion

        #region Protected methods

        protected void OnDataReceived(DynamicActivityBase activity)
        {
            if (DataReceived != null)
                DataReceived(this, activity);
        }

        protected void OnErrorHappened(Exception ex)
        {
            if (ErrorHappened != null)
                ErrorHappened(this, ex);
        }

        protected void OnHeartbeatReceived(HeartbeatEventArgs e)
        {
            if (HeartbeatReceived != null)
                HeartbeatReceived(this, e);
        }

        protected HttpWebRequest GetStreamRequest()
        {
            HttpWebRequest request = GetAPIRequest("GET", true);
            request.Headers.Add("Accept-Encoding", "gzip");

            return request;
        }

        protected HttpWebRequest GetAPIRequest(string method, bool useStream = false)
        {
            string url = string.Empty;
            if (useStream)
                url = Connection.GetStreamAPIURL();
            else
                url = Connection.GetRulesAPIURL();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(Connection.Username, Connection.Password);
            request.Method = method;
            request.PreAuthenticate = true;
            request.Timeout = Connection.Timeout;
            request.Accept = "application/json";
            request.ContentType = "application/json";

            return request;
        }

        #endregion
    }
}
