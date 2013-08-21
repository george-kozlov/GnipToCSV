
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>GnipProcessor class</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading;

using Gnip.Data;
using Gnip.Data.Common;
using Gnip.Data.Twitter;

using Gnip.Client.Common;

namespace Gnip.Client
{
	public delegate void ResultReceived(object sender, ActivityBase activity);
	public delegate void ErrorHappened(object sender, Exception ex);

	public sealed class GnipProcessor
	{
		#region Private members

		string _username = string.Empty;
		string _password = string.Empty;
        string _account = string.Empty;
		GnipSources _source = GnipSources.Twitter;
		bool _cancelFlag = false;
		bool _liveStreaming = true;
		int _timeout = 30000;

		Dictionary<GnipSources, string> _streamURLs = new Dictionary<GnipSources, string>();
        Dictionary<GnipSources, string> _apiURLs = new Dictionary<GnipSources, string>();
		Dictionary<GnipSources, Type> _types = new Dictionary<GnipSources, Type>();

		#endregion

		#region Constructor

		private GnipProcessor()
		{
			_streamURLs.Add(GnipSources.Twitter, "https://stream.gnip.com:443/accounts/{0}/publishers/{1}/streams/track/{2}.json");
            _apiURLs.Add(GnipSources.Twitter, "https://api.gnip.com:443/accounts/{0}/publishers/{1}/streams/track/{2}/rules.json");

			_types.Add(GnipSources.Twitter, typeof(TwitterActivity));
		}

		private GnipProcessor(string username, string password, string account, GnipSources service = GnipSources.Twitter, bool live = true) : this()
		{
			_username = username;
			_password = password;
            _account = account;
			_source = service;
			_liveStreaming = live;

			ReplyFrom = DateTime.Today.AddDays(-3);
			ReplyTo = DateTime.Today;
		}

		#endregion

		#region Static methods

		public static GnipProcessor CreateGnipProcessor(string username, string password, string account, GnipSources service = GnipSources.Twitter, bool live = true)
		{
			return new GnipProcessor(username, password, account, service, live);
		}

		public static Type GetObjectTypeByService(GnipSources service)
		{
			GnipProcessor processor = new GnipProcessor();
			Type objType = typeof(ActivityBase);
			if (processor._types.ContainsKey(service))
				objType = processor._types[service];

			return objType;
		}

		#endregion

		#region Public properties

		public GnipSources Source
		{
			get
			{
				return _source;
			}
		}

		public bool LiveStreaming
		{
			get
			{
				return _liveStreaming;
			}
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

		#endregion

		#region Public methods

		public void BeginStreaming()
		{
            HttpWebRequest request = GetStreamRequest();

			try
			{
				AsyncCallback asyncCallback = new AsyncCallback(AsyncCallbackHandler);
				request.BeginGetResponse(asyncCallback, request);
			}
			catch (Exception ex)
			{
				OnErrorHappened(ex);
			}
		}

		public void EndStreaming()
		{
			_cancelFlag = true;
		}

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

		public event ResultReceived DataReceived;

		public event ErrorHappened ErrorHappened;

		#endregion

		#region Private methods

        private HttpWebRequest GetStreamRequest()
		{
            HttpWebRequest request = GetAPIRequest("GET", true);
            request.Headers.Add("Accept-Encoding", "gzip");

            return request;
		}

        private HttpWebRequest GetAPIRequest(string method, bool useStream = false)
        {
            string url = string.Format(useStream ? _streamURLs[_source] : _apiURLs[_source], _account, _source.ToString().ToLower(), _liveStreaming ? "prod" : "reply");
            
            if (!_liveStreaming && useStream)
                url = string.Format("{0}?fromDate={1}&toDate={2}", url, ReplyFrom.ToString("yyyyMMddHHmm"), ReplyTo.ToString("yyyyMMddHHmm"));
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            NetworkCredential nc = new NetworkCredential(_username, _password);
            request.Credentials = nc;

            request.Method = method;
            request.PreAuthenticate = true;
            request.Timeout = _timeout;

            request.Accept = "application/json";
            request.ContentType = "application/json";

            return request;
        }

		private void OnDataReceived(ActivityBase activity)
		{
			if (DataReceived != null)
				DataReceived(this, activity);
		}

		private void OnErrorHappened(Exception ex)
		{
			if (ErrorHappened != null)
				ErrorHappened(this, ex);
		}

		private void AsyncCallbackHandler(IAsyncResult result)
		{
			HttpWebRequest request = (HttpWebRequest)result.AsyncState;

			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result))
				using (Stream stream = response.GetResponseStream())
				using (MemoryStream memory = new MemoryStream())
				using (GZipStream gzip = new GZipStream(memory, CompressionMode.Decompress))
				{
					byte[] compressedBuffer = new byte[8192];
					byte[] uncompressedBuffer = new byte[8192];
					List<byte> output = new List<byte>();

					if (!stream.CanRead)
						throw new EndOfStreamException("GNIP stream can't be read.");

					while (stream.CanRead)
					{
						Thread.Sleep(1000);

						if (response.StatusCode != HttpStatusCode.OK)
							throw new StreamingException(response.StatusCode, response.StatusDescription);

						if (_cancelFlag)
						{
							request.EndGetResponse(result);
							break;
						}

						int readCount = stream.Read(compressedBuffer, 0, compressedBuffer.Length);
						memory.Write(compressedBuffer.Take(readCount).ToArray(), 0, readCount);
						memory.Position = 0;

						int uncompressedLength = gzip.Read(uncompressedBuffer, 0, uncompressedBuffer.Length);
						output.AddRange(uncompressedBuffer.Take(uncompressedLength));

						if (!output.Contains(0x0A)) continue;  //Heartbeat

						byte[] bytesToDecode = output.Take(output.LastIndexOf(0x0A) + 1).ToArray();
						string outputString = Encoding.UTF8.GetString(bytesToDecode);
						output.RemoveRange(0, bytesToDecode.Length);

						string[] lines = outputString.Split(new[] { Environment.NewLine }, new StringSplitOptions());
						for (int i = 0; i < (lines.Length - 1); i++)
						{
							string heartBeatCheck = lines[i];
							if (heartBeatCheck.Trim().Length > 0)
							{
								using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(heartBeatCheck)))
								{
									Type contentType = _types[_source];
									DataContractJsonSerializer serializer = new DataContractJsonSerializer(contentType);
									Object obj = serializer.ReadObject(ms);

									OnDataReceived(obj as ActivityBase);
								}
							}
						}

						memory.SetLength(0);
					}

					if (!stream.CanRead)
						throw new EndOfStreamException("GNIP stream can't be read.");
				}
			}
			catch (Exception ex)
			{
				OnErrorHappened(ex);
			}
		}

		#endregion
	}
}
