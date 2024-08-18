using FeedbackEditor.Models.FC;
using FeedbackEditor.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FeedbackEditor.Services
{
    class FcFileLoaderService
    {
        public static FcFileLoaderService Instance { get; private set; } = new FcFileLoaderService();
        private XmlSerializer _serializer = new XmlSerializer(typeof(FcFile));

        public FcFile LoadFcFile(String datapath)
        {
            using var fs1 = File.OpenRead(datapath);
            using (XmlReader reader = XmlReader.Create(fs1))
            {
                var file = _serializer.Deserialize(reader) as FcFile;
                return file;
            }
        }

        public void SaveFcFile(String datapath, FcFile file)
        {
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            using var fs = File.Create(datapath);
            using (XmlWriter writer = new FeedbackXmlWriter(XmlWriter.Create(fs, settings)))
            {
                _serializer.Serialize(writer, file);
            }
        }
    }
}
