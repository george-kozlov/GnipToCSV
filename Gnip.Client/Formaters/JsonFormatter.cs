//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Gnip.Client.Formaters
{
    public sealed class JsonFormatter : IFormatter
    {
        public JsonFormatter()
        {
        }

        #region IFormatter implementation

        public bool IsValid(string rawText)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> result = serializer.Deserialize<Dictionary<string, object>>(rawText);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public string Normalize(string rawText)
        {
            string output = Regex.Replace(rawText, @"(\r\n)+", string.Empty);
            output = Regex.Replace(output, @"[}]{1}[{]{1}", string.Format("}}{0}{{", Environment.NewLine));

            return output;
        }

        public bool HasDelimiter(string data)
        {
            return data.Contains(Environment.NewLine);
        }

        public int FindLastDelimiter(string data)
        {
            int index = 0;
            index = data.LastIndexOf(Environment.NewLine) + 1;

            return index;
        }

        #endregion
    }
}
