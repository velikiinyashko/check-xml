namespace check_xml.Interface
{
    public interface ILogApp
    {
        void WriteLog(LogLevel level, string Log);
        void ProcentLog(int count, int position);
    }
}
