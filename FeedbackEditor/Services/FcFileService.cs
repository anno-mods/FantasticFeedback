using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.ViewModel;
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
    public class FcFileService
    {
        public static FcFileService Instance
        {
            get;
            private set;
        } = new FcFileService();
        public FcFile CurrentFile { get; private set; }

        private XmlSerializer _serializer = new XmlSerializer(typeof(FcFile));

        public event EventHandler<FcFile> FileLoaded = delegate { };

        public FcFileService()
        {
            Instance ??= this;
            CurrentFile = new FcFile();
        }

        public String GetActorName(FeedbackConfig config)
        {
            var index = CurrentFile.FeedbackDefinition.FeedbackConfigs.IndexOf(config);

            if (CurrentFile.ActorNames.Names.Count != CurrentFile.FeedbackDefinition.FeedbackConfigs.Count)
            {
                index += 1;
                //special case where RootObject is a seperate thing
            }

            return CurrentFile.ActorNames.Names[index];
        }

        public void TrySetActorName(FeedbackConfig config, String NewName)
        {
            throw new NotImplementedException();
        }

        public Dummy? GetDummy(int id)
        {
            return CurrentFile.DummyRoot.GetContainedDummies().Where(x => x.Id == id).FirstOrDefault();
        }

        public FcFile LoadFcFile(String datapath)
        {
            using var fs1 = File.OpenRead(datapath);
            using (XmlReader reader = XmlReader.Create(fs1))
            {
                var file = _serializer.Deserialize(reader) as FcFile;
                return file;
            }
        }

        public void SetCurrentFile(FcFile file)
        {
            CurrentFile = file;
            FileLoaded?.Invoke(this, CurrentFile);
        }
    }
}
