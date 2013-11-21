
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>GnipProcessorAsync class</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web.Script.Serialization;
using System.Threading;

using Gnip.Data;
using Gnip.Data.Json;
using Gnip.Data.XML;
using Gnip.Data.Common;
using Gnip.Client.Common;
using Gnip.Client.Connections;

namespace Gnip.Client
{
    
	public class GnipProcessorAsync : GnipProcessorBase
	{
        public GnipProcessorAsync(ConnectionBase connection)
            : base(connection)
        {
        }

        public override void BeginStreaming()
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

		public override void EndStreaming()
		{
			isCanceled = true;
		}

		private void AsyncCallbackHandler(IAsyncResult result)
		{
			HttpWebRequest request = (HttpWebRequest)result.AsyncState;

			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result))
                using (Stream stream = (Connection.UseEncoding) ? new GZipStream(response.GetResponseStream(), CompressionMode.Decompress) : response.GetResponseStream())
				{
					byte[] buffer = new byte[8192];
					List<byte> output = new List<byte>();

					if (!stream.CanRead)
						throw new EndOfStreamException("GNIP stream can't be read.");

					while (stream.CanRead && !isCanceled)
					{
						Thread.Sleep(1000);

						if (response.StatusCode != HttpStatusCode.OK)
							throw new StreamingException(response.StatusCode, response.StatusDescription);

                        int length = stream.Read(buffer, 0, buffer.Length);
                        output.AddRange(buffer.Take(length));
                        string outputString = Encoding.UTF8.GetString(output.ToArray());

                        if (!Formatter.HasDelimiter(outputString))
                            continue;

                        int delimiterIndex = Formatter.FindLastDelimiter(outputString);
                        outputString = outputString.Substring(0, delimiterIndex);
                        output.RemoveRange(0, Encoding.UTF8.GetByteCount(outputString));

                        if (String.IsNullOrEmpty(outputString))
                            continue;

                        outputString = Formatter.Normalize(outputString);

                        OnHeartbeatReceived(new HeartbeatEventArgs() { Message = outputString });

                        string[] lines = outputString.Split(new[] { Environment.NewLine }, new StringSplitOptions());
						for (int i = 0; i < lines.Length; i++)
						{
							string heartBeatCheck = lines[i];
                            if (heartBeatCheck.Trim().Length > 0)
							{
                                if (!Formatter.IsValid(heartBeatCheck))
                                    continue;

                                Type contentType = GnipProcessorBase.GetObjectTypeByService(Connection.DataSource);
                                try
                                {
                                    DynamicActivityBase activity = null;

                                    if (Connection.DataFormat == GnipDataFormat.XML)
                                    {
                                        MemoryStream memStr = new MemoryStream(Encoding.UTF8.GetBytes(heartBeatCheck));
                                        XmlTextReader xmlReader = new XmlTextReader(memStr);
                                        DynamicXMLObjectConverter converter = new DynamicXMLObjectConverter();
                                        dynamic obj = converter.Deserialize(xmlReader, typeof(DynamicXMLObject));
                                        activity = ReflectionHelper.CreateAndCastInstance(contentType, obj);
                                    }
                                    else
                                    {
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        serializer.RegisterConverters(new[] { new DynamicJsonObjectConverter() });
                                        dynamic obj = serializer.Deserialize(heartBeatCheck, typeof(DynamicJsonObject));
                                        activity = ReflectionHelper.CreateAndCastInstance(contentType, obj);
                                    }

                                    OnDataReceived(activity);
                                }
                                catch (Exception ex)
                                {
                                    OnErrorHappened(ex);
                                }
							}
						}
					}

					if (!stream.CanRead)
						throw new EndOfStreamException("GNIP stream can't be read.");
				}
			}
            catch (WebException ex)
            {
                string status = "";
                string message = "";

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        status = ((int)response.StatusCode).ToString();
                        message = response.StatusDescription;

                        if (stream != null && stream.CanRead)
                        {
                            message = reader.ReadToEnd();
                            OnErrorHappened(ex);
                        }
                    }
                }
                else
                {
                    OnErrorHappened(ex);
                }
            }
            catch (Exception ex)
            {
                OnErrorHappened(ex);
            }
		}
	}
}
