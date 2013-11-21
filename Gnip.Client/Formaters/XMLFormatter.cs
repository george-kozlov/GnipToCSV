
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>10/06/2013</date>
// <summary>XMLFormatter class</summary>

using System;
using System.Xml;
using System.Text.RegularExpressions;

namespace Gnip.Client.Formaters
{
    public sealed class XMLFormatter : IFormatter
    {
        public XMLFormatter()
        {
        }

        #region IFormatter implementation

        public bool IsValid(string rawText)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(rawText);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public string Normalize(string rawText)
        {
            string output = Regex.Replace(rawText, @">\n{1}\s*<", "><");
            output = Regex.Replace(output, @">\n{2}<", string.Format(">{0}<", Environment.NewLine));
            output = Regex.Replace(output, @"entry><entry", string.Format("entry>{0}<entry", Environment.NewLine));

            return output;
        }

        public bool HasDelimiter(string data)
        {
            return Regex.IsMatch(data, @">\n{2}[^<]*");
        }

        public int FindLastDelimiter(string data)
        {
            int index = 0;

            Regex regex = new Regex(@">\n{2}[^<]*");

            if (regex.IsMatch(data))
            {
                MatchCollection matches = regex.Matches(data);
                index = (matches[0].Index + 1) + (matches[0].Length - 1);

                foreach (Match match in matches)
                {
                    if (match.Index > index)
                        index = (match.Index + 1) + (match.Length - 1);
                }
            }

            return index;
        }

        #endregion
    }
}
