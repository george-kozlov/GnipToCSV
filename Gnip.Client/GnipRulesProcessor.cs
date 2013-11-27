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
using Gnip.Client.Common;
using Gnip.Client.Connections;

namespace Gnip.Client
{
    public sealed class GnipRulesProcessor : IRulesProcessor
    {
        #region Private members

        private string _url = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;

        #endregion

        #region Constructor

        private GnipRulesProcessor(string url, string username, string password)
        {
            _url = url;
            _username = username;
            _password = password;
        }

        #endregion

        #region Static members

        public static IRulesProcessor CreateRulesProcessor(string url, string username, string password)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("url, username and password parameters can not be null or string.Empty.");

            return new GnipRulesProcessor(url, username, password);
        }

        public static IRulesProcessor CreateRulesProcessor(ConnectionBase connection)
        {
            if (connection == null)
                throw new ArgumentException("connection can not bu null.");

            return new GnipRulesProcessor(connection.GetRulesAPIURL(), connection.Username, connection.Password);
        }

        #endregion

        public event ErrorHappened ErrorHappened;

        #region IRulesProcessor members

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

        #region Private methods

        private void OnErrorHappened(Exception ex)
        {
            if (ErrorHappened != null)
                ErrorHappened(this, ex);
        }

        private HttpWebRequest GetAPIRequest(string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Credentials = new NetworkCredential(_username, _password);
            request.Method = method;
            request.PreAuthenticate = true;
            request.Timeout = 60000;
            request.Accept = "application/json";
            request.ContentType = "application/json";

            return request;
        }

        #endregion
    }
}
