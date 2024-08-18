using FeedbackEditor.Models.FC;
using FeedbackEditor.Models.FC.Dummy;
using FeedbackEditor.Serialization;
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


        public event EventHandler<FcFile> FileLoaded = delegate { };

        public FcFileService()
        {
            Instance ??= this;
            CurrentFile = new FcFile();
        }        

        public Dummy? GetDummy(int id)
        {
            return CurrentFile.DummyRoot.GetContainedDummies().Where(x => x.Id == id).FirstOrDefault();
        }

        public DummyGroup? GetDummyGroup(String id)
        {
            return CurrentFile.DummyRoot.GetContainedDummyGroups().Where(x => x.Name == id).FirstOrDefault();
        }

        public void SetCurrentFile(FcFile file)
        {
            CurrentFile = file;
            FileLoaded?.Invoke(this, CurrentFile);
        }
    }
}
