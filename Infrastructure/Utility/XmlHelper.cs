using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Reporting.EntityManagement
{
    public class XmlHelper
    {
        public XmlHelper(string filename)
        {
            if (filename == string.Empty)
                throw new ArgumentNullException(filename);

            _filename = filename;

            var path = Path.GetDirectoryName(filename) ;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if(!File.Exists(filename))
            {
                var writer = new XmlTextWriter(filename,System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("categories");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            _xml = XDocument.Load(_filename);
        }

        private readonly String _filename;
        private readonly XDocument _xml;

        public Boolean IsExistsElement(String elemaneName)
        {
            try
            {
                return _xml.Descendants(elemaneName).First() != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<XElement> Elements(String elemaneName, Func<XElement,Boolean> predicate)
        {
            return predicate == null ? _xml.Descendants(elemaneName) : _xml.Descendants(elemaneName).Where(predicate);
        }

        public XElement Element(String elemaneName, Func<XElement, Boolean> predicate)
        {
            return predicate == null
                       ? _xml.Descendants(elemaneName).FirstOrDefault()
                       : _xml.Descendants(elemaneName).FirstOrDefault(predicate);
        } 

        public void Add(XElement element)
        {
            var root = _xml.Descendants("root").FirstOrDefault();
            if(root==null)
                throw new ArgumentNullException();
            root.Add(element);   
        }

        public void Add(XElement element,String parentElementName ,Func<XElement, Boolean> predicate)
        {
            if(_xml.Descendants(parentElementName).FirstOrDefault()==null)
                throw new ArgumentNullException();

            var parentElement = predicate == null
                                    ? _xml.Descendants(parentElementName).FirstOrDefault()
                                    : _xml.Descendants(parentElementName).FirstOrDefault(predicate);

            if(parentElement==null)
                throw new ArgumentNullException();
            parentElement.Add(element);
        }

        public void Remove(String parentElementName ,Func<XElement, Boolean> predicate)
        {
            if (_xml.Descendants(parentElementName).FirstOrDefault() == null)
                throw new ArgumentNullException();

            var parentElement = predicate == null
                                    ? _xml.Descendants(parentElementName).FirstOrDefault()
                                    : _xml.Descendants(parentElementName).FirstOrDefault(predicate);

            if (parentElement == null)
                throw new ArgumentNullException();

            parentElement.Remove();
        }
        public void Save()
        {
            _xml.Save(_filename);
        }
    }
}