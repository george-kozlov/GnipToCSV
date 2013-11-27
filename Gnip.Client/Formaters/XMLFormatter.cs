//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

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
