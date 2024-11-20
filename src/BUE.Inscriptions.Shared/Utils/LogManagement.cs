using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Shared.Utils
{
    public class LogManagement
    {
        private string m_exePath = string.Empty;
        public static LogManagement Instance { get; } = new LogManagement();
        public void write(string model, string method, string logMessage, string appName = "BUE.Services", string pathFile = "")
        {
            string pathFileLog = pathFile;
            if (pathFile == "" || pathFile == null)
                pathFileLog = Path.GetDirectoryName(Assembly.GetAssembly(typeof(LogManagement)).Location);
            m_exePath = pathFileLog + @"\logs";
            try
            {
                string fileLogName = m_exePath + "\\" + appName + ".log";
                if (!Directory.Exists(m_exePath))
                {
                    Directory.CreateDirectory(m_exePath);
                }
                if (!File.Exists(fileLogName))
                {
                    using (StreamWriter w = File.AppendText(fileLogName))
                    {
                        Log(logMessage, w, model, method);
                    }
                    return;
                }
                FileInfo fi = new FileInfo(fileLogName);
                if (fi.Length > 20480000)
                {
                    DateTime date1 = DateTime.Now;
                    File.Move(fileLogName, m_exePath + "\\" + date1.ToString("dd_mm_yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "_" + appName + ".log");
                }
                using (StreamWriter w = File.AppendText(fileLogName))
                {
                    Log(logMessage, w, model, method);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void writeAssembly(Type DeclaringType, string logMessage, string subFix = "",string pathFile = "")
        {
            string pathFileLog = pathFile;
           
            try
            {
                var modelName = DeclaringType.ReflectedType.Name;
                var methodName = DeclaringType.Name;
                var nameSpace = DeclaringType.Namespace + subFix;

                if (pathFile == "" || pathFile == null)
                    pathFileLog = Path.GetDirectoryName(Assembly.GetAssembly(typeof(LogManagement)).Location);
                m_exePath = pathFileLog + @"\logs";
                string fileLogName = m_exePath + "\\" + nameSpace + ".log";
                if (!Directory.Exists(m_exePath))
                {
                    Directory.CreateDirectory(m_exePath);
                }
                if (!File.Exists(fileLogName))
                {
                    using (StreamWriter w = File.AppendText(fileLogName))
                    {
                        Log(logMessage, w, modelName, methodName);
                    }
                    return;
                }
                FileInfo fi = new FileInfo(fileLogName);
                if (fi.Length > 20480000)
                {
                    DateTime date1 = DateTime.Now;
                    File.Move(fileLogName, m_exePath + "\\" + date1.ToString("dd_mm_yyyy", CultureInfo.CreateSpecificCulture("en-US")) + "_" + nameSpace + ".log");
                }
                using (StreamWriter w = File.AppendText(fileLogName))
                {
                    Log(logMessage, w, modelName, methodName);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Log(string logMessage, TextWriter txtWriter, string model, string method)
        {
            try
            {
                txtWriter.Write("\r\n");
                txtWriter.Write("{0}",
                    DateTime.Now.ToString() + " || " + model + " || " + method + " || " + logMessage);
            }
            catch
            {
            }
        }
    }
}
