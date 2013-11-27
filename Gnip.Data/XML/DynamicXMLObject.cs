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
using System.Dynamic;
using System.Xml;
using System.Xml.Linq;

namespace Gnip.Data.XML
{
    public class DynamicXMLObject : DynamicObject
    {
		#region Private members

        private const string _value = "value";
		private XElement _xmlElement;

        #endregion

		#region Constructors

        public DynamicXMLObject(XElement xElement)
        {
            if (xElement == null)
                throw new ArgumentNullException("xElement");

            _xmlElement = DynamicXMLObjectConverter.RemoveAllNamespaces(xElement);
        }

		#endregion

		#region Overrides

		public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            if (binder.Name == _value)
            {
                result = _xmlElement.Value;
                return true;
            }

            if (_xmlElement.NodeType == XmlNodeType.Text)
            {
                result = _xmlElement.Value;
                return true;
            }
            else if (_xmlElement.NodeType == XmlNodeType.Element || _xmlElement.NodeType == XmlNodeType.Document)
            {
                if (_xmlElement.HasElements)
                {
                    foreach (XElement xelem in _xmlElement.Elements())
                    {
                        if (xelem.Name.LocalName.Equals(binder.Name))
                        {
                            if (xelem.NodeType == XmlNodeType.Element && !xelem.HasElements && !xelem.HasAttributes)
                            {
                                result = xelem.Value;
                                return true;
                            }
                            else if ((xelem.NodeType == XmlNodeType.Element || xelem.NodeType == XmlNodeType.Document) && (xelem.HasElements || xelem.HasAttributes))
                                result = new DynamicXMLObject(xelem);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            throw new NotImplementedException();
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
			result = null;

			if (indexes[0] != null)
			{
				if (typeof(int) == indexes[0].GetType())
				{
					int index = (int)indexes[0];
                    if (_xmlElement.HasElements)
                    {
                        int currentIndex = 0;
                        foreach (XElement xelem in _xmlElement.Elements())
                        {
                            if (currentIndex == index)
                            {
                                if (xelem.NodeType == XmlNodeType.Element && !xelem.HasElements)
                                {
                                    if (xelem.HasAttributes)
                                        result = new DynamicXMLObject(xelem);
                                    else
                                        result = xelem.Value;

                                    return true;
                                }
                                else if ((_xmlElement.NodeType == XmlNodeType.Element || _xmlElement.NodeType == XmlNodeType.Document) && xelem.HasElements)
                                    result = new DynamicXMLObject(xelem);

                                return true;
                            }

                            currentIndex++;
                        }
                    }
				}
				else if (typeof(string) == indexes[0].GetType())
				{
					string attributeName = (string)indexes[0];
					result = _xmlElement.Attribute(XName.Get(attributeName)).Value;

					return true;
				}
			}

			return false;
        }

		public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
            throw new NotImplementedException();
		}

        public override string ToString()
        {
            if (!_xmlElement.HasElements && !_xmlElement.HasAttributes)
                return _xmlElement.Value;

            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            bool firstInDictionary = true;

            if (_xmlElement.HasAttributes)
            {
                foreach (XAttribute attr in _xmlElement.Attributes())
                {
                    if (!firstInDictionary)
                        sb.Append(",");
                    firstInDictionary = false;

                    int intValue = 0;
                    bool boolValue = false;
                    if (bool.TryParse(attr.Value, out boolValue) || int.TryParse(attr.Value, out intValue))
                        sb.AppendFormat("\"{0}\":{1}", attr.Name, attr.Value);
                    else
                        sb.AppendFormat("\"{0}\":\"{1}\"", attr.Name, attr.Value);
                }
            }

            if (_xmlElement.HasElements)
            {
                foreach (XElement elem in _xmlElement.Nodes())
                {
                    if (!firstInDictionary)
                        sb.Append(",");
                    firstInDictionary = false;

                    if (elem.NodeType == XmlNodeType.Element && !elem.HasElements && !elem.HasAttributes)
                    {
                        int intValue = 0;
                        bool boolValue = false;
                        if (bool.TryParse(elem.Value, out boolValue) || int.TryParse(elem.Value, out intValue))
                            sb.AppendFormat("\"{0}\":{1}", elem.Name, elem.Value);
                        else
                            sb.AppendFormat("\"{0}\":\"{1}\"", elem.Name, elem.Value);
                    }
                    else if (elem.NodeType == XmlNodeType.Element && (elem.HasElements || elem.HasAttributes))
                    {
                        sb.AppendFormat("\"{0}\":", elem.Name);
                        sb.Append(new DynamicXMLObject(elem).ToString());
                    }
                }
            }

            if (!_xmlElement.HasElements && !_xmlElement.IsEmpty)
            {
                int intValue = 0;
                bool boolValue = false;
                if (bool.TryParse(_xmlElement.Value, out boolValue) || int.TryParse(_xmlElement.Value, out intValue))
                    sb.AppendFormat(",\"value\":{0}", _xmlElement.Value);
                else
                    sb.AppendFormat(",\"value\":\"{0}\"", _xmlElement.Value);
            }

            sb.Append("}");

            return sb.ToString();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            List<string> list = new List<string>();

            foreach (XElement xelem in _xmlElement.Elements())
                list.Add(xelem.Name.LocalName);

            return list;
        }

		#endregion
    }
}
