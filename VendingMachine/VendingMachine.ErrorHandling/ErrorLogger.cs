using System;
using System.Configuration;
using System.IO;

namespace VendingMachine.ErrorHandling
{
    public static class ErrorLogger
    {
        public static void logError(string sClass, string sMethod, string sError)
        {
            try
            {
                string sPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ErrorLogFile"];
                string sErrorMsg = string.Format("Class: {0} -- ", sClass);
                sErrorMsg += string.Format("Method: {0} -- ", sMethod);
                sErrorMsg += string.Format("Date: {0} -- ", DateTime.Now.ToString("[yyyy-MMM-dd HH:mm:ss.fff(K)]"));
                sErrorMsg += string.Format("Exception: {0}", sError);

                if (!File.Exists(sPath))
                    File.Create(sPath);

                StreamWriter stmFile = File.AppendText(sPath);
                stmFile.WriteLine(sErrorMsg);
                stmFile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
