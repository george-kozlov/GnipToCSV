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

    public abstract class GnipStreamProcessorBase
    {
        #region Private members

        ConnectionBase _connection;
        IFormatter _formatter;

        bool _cancelFlag = false;

        static Dictionary<GnipSources, Type> _types = new Dictionary<GnipSources, Type>();

        #endregion

        #region Constructor

        static GnipStreamProcessorBase()
        {
            _types.Add(GnipSources.Twitter, typeof(TwitterActivity));
            _types.Add(GnipSources.Facebook, typeof(FacebookActivity));
            _types.Add(GnipSources.YouTube, typeof(YouTubeActivity));
            _types.Add(GnipSources.GooglePlus, typeof(GooglePlusActivity));
            _types.Add(GnipSources.Reddit, typeof(RedditActivity));
        }

        public GnipStreamProcessorBase(ConnectionBase connection)
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

        public static T CreateRulesProcessor<T>(ConnectionBase connection)
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

        #region Abstract methods

        public abstract void BeginStreaming();
        public abstract void EndStreaming();

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
            string url = Connection.GetStreamAPIURL();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(Connection.Username, Connection.Password);
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.Timeout = Connection.Timeout;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add("Accept-Encoding", "gzip");

            return request;
        }

        #endregion
    }
}
