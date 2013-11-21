
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>10/06/2013</date>
// <summary>JsonFormatter class</summary>

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
