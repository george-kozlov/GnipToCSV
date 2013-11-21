
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/29/2013</date>
// <summary>DynamicXMLObjectConverter class</summary>

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Reflection;

namespace Gnip.Data.XML
{
    public class DynamicXMLObjectConverter
    {
        ReadOnlyCollection<Type> _supportedTypes = new ReadOnlyCollection<Type>(new List<Type>()
        {
            typeof(DynamicXMLObject)
        });

        public virtual object Deserialize(XmlTextReader reader, Type type)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            if (!_supportedTypes.Contains(type))
                throw new NotSupportedException(type.ToString());

            return new DynamicXMLObject(XElement.Load(reader));
        }

        public virtual IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<Type> SupportedTypes
        {
            get
            {
                return _supportedTypes;
            }
        }
 
        public static XElement RemoveAllNamespaces(XDocument xDocumentSource)
        {
			Stream docStream = new MemoryStream();
            xDocumentSource.Save(docStream);
            docStream.Position = 0;

            Stream outputStream = new MemoryStream();
            outputStream.Position = 0;

            XPathDocument xPathDocument = new XPathDocument(docStream);
            XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
 
            XslCompiledTransform myXslTransform;
            myXslTransform = new XslCompiledTransform();
            
            XmlReader xsltReader = 
				XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("Gnip.Data.XML.StripNamespace.xslt"));
            myXslTransform.Load(xsltReader);
 
            XsltArgumentList xsltArgList = new XsltArgumentList();
            myXslTransform.Transform(xPathNavigator, xsltArgList, outputStream);
 
            outputStream.Position = 0;
			XDocument finalDocument = XDocument.Load(outputStream);

			XElement root = finalDocument.Root;
            return root;
        }

		public static XElement RemoveAllNamespaces(XElement xElementSource)
		{
			Stream docStream = new MemoryStream();
			xElementSource.Save(docStream);
			docStream.Position = 0;

			Stream outputStream = new MemoryStream();
			outputStream.Position = 0;

			XPathDocument xPathDocument = new XPathDocument(docStream);
			XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();

            XslCompiledTransform myXslTransform;
            myXslTransform = new XslCompiledTransform();

			XmlReader xsltReader =
				XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("Gnip.Data.XML.StripNamespace.xslt"));
			myXslTransform.Load(xsltReader);

			XsltArgumentList xsltArgList = new XsltArgumentList();
			myXslTransform.Transform(xPathNavigator, xsltArgList, outputStream);

			outputStream.Position = 0;
			XDocument finalDocument = XDocument.Load(outputStream);

			XElement root = finalDocument.Root;
			return root;
		}
    }
}