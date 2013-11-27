//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

using System;
using System.Web;
using System.Text;
using System.Linq;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

namespace Gnip.Data.Json
{
    public class DynamicJsonObject : DynamicObject
    {
        #region Private members

        readonly IDictionary<string, object> _rawData;

        #endregion

        #region Constructor

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            _rawData = dictionary;
        }

        #endregion

        #region Overrides

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            if (_rawData.Count > 0)
            {
                if (!_rawData.TryGetValue(binder.Name, out result))
                    return false;

                if (result is IDictionary<string, object> && ((IDictionary<string, object>)result).Count > 0)
                    result = new DynamicJsonObject((IDictionary<string, object>)result);
                else if (result is ArrayList && ((ArrayList)result).Count > 0)
                {
                    ArrayList iterate = result as ArrayList;
                    IDictionary<string, object> res = new Dictionary<string, object>(iterate.Count);
                    for (int i = 0; i < iterate.Count; i++)
                        res.Add(i.ToString(), iterate[i]);

                    result = new DynamicJsonObject((IDictionary<string, object>)res);
                }

                return true;
            }

            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_rawData.Count > 0)
            {
                if (_rawData.ContainsKey(binder.Name))
                    _rawData[binder.Name] = value;
                else
                    _rawData.Add(binder.Name, value);

                return true;
            }

            return false;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = null;

            if (indexes!= null && indexes.Length > 0 && indexes[0] != null)
            {
                if (typeof(int) == indexes[0].GetType())
                {
                    int index = (((int)indexes[0]) >= 0) ? ((int)indexes[0]) : 0;
                    List<object> values = new List<object>(_rawData.Values);
                    result = values[index];
                }
                else if (typeof(string) == indexes[0].GetType())
                {
                    string key = indexes[0] as string;
                    if (_rawData.ContainsKey(key))
                        result = _rawData[key];
                }
            }

            if (result != null)
            {
                if (result is IDictionary<string, object> && ((IDictionary<string, object>)result).Count > 0)
                    result = new DynamicJsonObject((IDictionary<string, object>)result);
                else if (result is ArrayList && ((ArrayList)result).Count > 0)
                {
                    ArrayList iterate = result as ArrayList;
                    IDictionary<string, object> res = new Dictionary<string, object>(iterate.Count);
                    for (int i = 0; i < iterate.Count; i++)
                        res.Add(i.ToString(), iterate[i]);

                    result = new DynamicJsonObject((IDictionary<string, object>)res);
                }

                return true;
            }

            return false;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            bool firstInDictionary = true;
            foreach (KeyValuePair<string, object> pair in _rawData)
            {
                if (!firstInDictionary)
                    sb.Append(",");
                firstInDictionary = false;

                if (pair.Value is string)
                    sb.AppendFormat("\"{0}\":\"{1}\"", pair.Key, HttpUtility.JavaScriptStringEncode((string)pair.Value));
                else if (pair.Value is IDictionary<string, object>)
                {
                    sb.AppendFormat("\"{0}\":", pair.Key);
                    sb.Append(new DynamicJsonObject((IDictionary<string, object>)pair.Value).ToString());
                }
                else if (pair.Value is IList)
                {
                    sb.AppendFormat("\"{0}\":[", pair.Key);

                    bool firstInArray = true;
                    foreach (object item in (IList)pair.Value)
                    {
                        if (!firstInArray)
                            sb.Append(",");
                        firstInArray = false;

                        if (item is IDictionary<string, object>)
                            sb.Append(new DynamicJsonObject((IDictionary<string, object>)item).ToString());
                        else if (item is string)
                            sb.AppendFormat("\"{0}\"", HttpUtility.JavaScriptStringEncode((string)item));
                        else
                            sb.AppendFormat("{0}", item);
                    }
                    sb.Append("]");
                }
                else if (pair.Value is Boolean)
                    sb.AppendFormat("\"{0}\":{1}", pair.Key, pair.Value.ToString().ToLower());
                else
                {
                    if (pair.Value != null)
                        sb.AppendFormat("\"{0}\":{1}", pair.Key, pair.Value);
                    else
                        sb.AppendFormat("\"{0}\":null", pair.Key);
                }
            }
            sb.Append("}");

            return sb.ToString();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            if (_rawData.Count > 0)
                return _rawData.Keys;

            return new List<string>() { string.Empty };
        }

        #endregion

        #region Public methods

        public string DecodeFromUtf8(string utf8String)
        {
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i=0; i<utf8String.Length; ++i) {
                utf8Bytes[i] = (byte)utf8String[i];
            }
            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        #endregion
    }
}
