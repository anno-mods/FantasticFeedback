using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackEditor.Services
{
    public class FileDBReaderService
    {
        public static FileDBReaderService Instance { get; } = new FileDBReaderService();

        public bool IsInstalled() 
        {
            if (Properties.Settings.Default.FileDBReaderPath == String.Empty)
                return false;
            var exeExists = File.Exists(Properties.Settings.Default.FileDBReaderPath);
            var fcFileInterpreterPath = Path.Combine(Path.GetPathRoot(Properties.Settings.Default.FileDBReaderPath) ?? "", "FileFormats", "FcFile.xml");
            var fcFileExists = File.Exists(fcFileInterpreterPath);
            return exeExists && fcFileExists;
        }

        public void SetFileDBReaderPath(String path)
        {
            Properties.Settings.Default.FileDBReaderPath = path;
            Properties.Settings.Default.Save();
        }

        public void ConvertFc(String fcPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = Properties.Settings.Default.FileDBReaderPath;
            process.StartInfo.Arguments = @$"fctohex -f {fcPath} -d -y -i FcFile.xml";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
        }

        public void ConvertXml(String xmlPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = Properties.Settings.Default.FileDBReaderPath;
            process.StartInfo.Arguments = @$"hextofc -f {xmlPath} -d -y -i FcFile.xml";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            process.WaitForExit();
        }
    }
}
