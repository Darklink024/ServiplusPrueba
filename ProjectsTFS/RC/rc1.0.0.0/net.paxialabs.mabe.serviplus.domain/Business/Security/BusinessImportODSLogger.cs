using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using net.paxialabs.mabe.serviplus.security.ManagerExceptions;
using System;

namespace net.paxialabs.mabe.serviplus.domain.Business.Security
{
    internal class BusinessImportODSLogger
    {
        private static readonly ILog log = LogManager.GetLogger("MabeImportODSLog");

        public BusinessImportODSLogger()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = @"Logs\MabeImportODSLog.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        public void WriteEntry(string msg)
        {
            try
            {
                log.Info(msg);
            }
            catch (Exception ex)
            {
                throw new Log4NetException("Ocurrio un error al escribir en el archivo de log", ex);
            }
        }

        public void WriteError(Exception ex, string section)
        {
            try
            {
                String errorDescription = "Sección: " + section + Environment.NewLine +
                                Environment.NewLine + "Mensaje de Error: " + Environment.NewLine + ex.Message + Environment.NewLine +
                                "Seguimiento de Pila: " + ex.StackTrace;

                if (ex.InnerException != null)
                    errorDescription += Environment.NewLine + "Excepcion interna: " + Environment.NewLine +
                                          "Mensaje de Error Interno: " + Environment.NewLine + ex.InnerException.Message + Environment.NewLine +
                                          "Seguimiento de Pila interna: " + ex.InnerException.StackTrace;


                log.Error(errorDescription);
            }
            catch (Exception exInner)
            {
                throw new Log4NetException("Ocurrio un error al escribir en el archivo de log", exInner);
            }
        }
    }
}
