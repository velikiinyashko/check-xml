using check_xml.Interface;
using System;
using System.IO;

namespace check_xml
{
    class MoveFile
    {
        private ILogApp _log;
        public MoveFile(ILogApp log)
        {
            _log = log;
        }
        public void Move(string source, string destination)
        {
            try
            {
                //string NoValidePath = $"{Directory.GetCurrentDirectory()}\\NoValide";
                if (Directory.Exists(destination) == false)
                {
                    Directory.CreateDirectory(destination);
                    _log.WriteLog(LogLevel.Info, "create folder");
                }

                File.Move(source, $"{destination}\\{Path.GetFileName(source)}");
                _log.WriteLog(LogLevel.Move, $"file: [{Path.GetFileName(source)}]");
            }
            catch (Exception ex)
            {
                _log.WriteLog(LogLevel.Error, ex.Message);
            }
        }
    }
}
