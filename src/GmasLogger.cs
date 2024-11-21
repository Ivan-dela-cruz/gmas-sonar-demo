using System.Globalization;
using System.IO;
using System.Reflection;
using System;
using System.Configuration;

public class GmasLogger
{
    private string m_exePath = string.Empty;
    public static GmasLogger Instance { get; } = new GmasLogger();
    public void write(string logMessage, string appName = "BalconGP", string pathFile = "")
    {
        string pathFileLog = ConfigurationManager.AppSettings["RutaLog"];// pathFile;
        
        if (pathFile == "" || pathFile == null)
            pathFileLog = ConfigurationManager.AppSettings["RutaLog"]; //.GetDirectoryName(Assembly.GetAssembly(typeof(GmasLogger)).Location);
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
                    Log(logMessage, w);
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
                Log(logMessage, w);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Log(string logMessage, TextWriter txtWriter)
    {
        try
        {
            txtWriter.Write("\r\n");
            txtWriter.Write("{0}",
                DateTime.Now.ToString() + " " + logMessage);
        }
        catch
        {
        }
    }
}