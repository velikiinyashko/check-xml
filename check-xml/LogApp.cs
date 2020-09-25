using check_xml.Interface;
using System;
using System.Collections.Generic;

namespace check_xml
{
    public class LogApp : ILogApp
    {
        private delegate void DelegateWrite(LogLevel level, string message);
        private Dictionary<LogLevel, DelegateWrite> _log;
        public LogApp()
        {
            _log = new Dictionary<LogLevel, DelegateWrite>
            {
                {LogLevel.Text, this.TextLog },
                {LogLevel.Info, this.InfoLog },
                {LogLevel.Warning, this.WarningLog },
                {LogLevel.Error, this.ErrorLog },
                {LogLevel.Move, this.MoveLog }
            };
        }

        public void WriteLog(LogLevel level, string message)
        {
            if (!_log.ContainsKey(level))
                throw new ArgumentException();
            _log[level](level, message);
        }

        private void InfoLog(LogLevel level, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}]|[{level}] - {message}");
            Console.ResetColor();
        }
        private void WarningLog(LogLevel level, string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}]|[{level}] - {message}");
            Console.ResetColor();
        }
        private void ErrorLog(LogLevel level, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}]|[{level}] - {message}");
            Console.ResetColor();
        }
        private void TextLog(LogLevel level, string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}]|[{level}] - {message}");
            Console.ResetColor();
        }
        private void MoveLog(LogLevel level, string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}]|[{level}] - {message}");
            Console.ResetColor();
        }
        public void ProcentLog(int count, int position)
        {
            double pr = Math.Round((double)(position * 100 / count));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine($"[{pr}%]");
            Console.ResetColor();
        }
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Text,
        Move
    }
}
