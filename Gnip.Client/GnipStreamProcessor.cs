//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

using System.Web.Script.Serialization;

using Gnip.Data;
using Gnip.Data.Json;
using Gnip.Data.XML;
using Gnip.Data.Common;
using Gnip.Client.Common;
using Gnip.Client.Connections;

namespace Gnip.Client
{
	public class GnipStreamProcessor : GnipStreamProcessorBase
	{
        public GnipStreamProcessor(ConnectionBase connection)
            : base(connection)
        {
        }

        #region GnipStreamProcessorBase overrides

        public override void BeginStreaming()
		{
            HttpWebRequest request = GetStreamRequest();
            
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                stream = (Connection.UseEncoding) ? new GZipStream(response.GetResponseStream(), CompressionMode.Decompress) : response.GetResponseStream();

                byte[] buffer = new byte[8192];
                List<byte> output = new List<byte>();

                while(stream.CanRead)
                {
                    Thread.Sleep(1000);

                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new StreamingException(response.StatusCode, response.StatusDescription);

                    int readCount = stream.Read(buffer, 0, buffer.Length);
                    output.AddRange(buffer.Take(readCount));
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

                            Type contentType = GnipStreamProcessorBase.GetObjectTypeByService(Connection.DataSource);
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
            }
            catch (WebException ex)
            {
                string status = "";
                string message = "";

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    using (response)
                    {
                        status = ((int)response.StatusCode).ToString();
                        message = response.StatusDescription;

                        stream = response.GetResponseStream();
                        if (stream != null && stream.CanRead)
                        {
                            reader = new StreamReader(stream);
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
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
            } 
		}

		public override void EndStreaming()
		{
            isCanceled = true;
        }

        #endregion
    }
}
