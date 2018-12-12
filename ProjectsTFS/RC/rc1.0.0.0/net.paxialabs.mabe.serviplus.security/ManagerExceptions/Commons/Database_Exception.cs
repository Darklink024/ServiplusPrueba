using log4net;
using System;
using System.Data.Common;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons
{
    public sealed class Database_Exception : DbException
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Database_Exception));
        public int ErrorCodeException { get { return base.ErrorCode; } }
        public Guid ErrorGUI { get; internal set; }

        public Database_Exception(DbException ex) : base(ex.Message, ex)
        {
            this.ErrorGUI = Guid.NewGuid();
            LoggerConfiguration.DefaultSetup();
            this.LoggerException();
        }

        private void LoggerException()
        {
            try
            {
                String errorDescription =
                                Environment.NewLine + "GUI de Error: " + this.ErrorGUI.ToString() + Environment.NewLine +
                                "Codigo de Error: " + this.ErrorCode.ToString() + Environment.NewLine +
                                "Mensaje de Error: " + Environment.NewLine + base.Message + Environment.NewLine +
                                "Seguimiento de Pila: " + base.StackTrace;

                if (this.InnerException != null)
                    errorDescription += Environment.NewLine + "Excepcion interna: " + Environment.NewLine +
                                          "Mensaje de Error Interno: " + Environment.NewLine + this.InnerException.Message + Environment.NewLine +
                                          "Seguimiento de Pila interna: " + this.InnerException.StackTrace;

                log.Error(errorDescription);
            }
            catch (Exception ex)
            {
                throw new Log4NetException("Ocurrio un error al escribir en el archivo de log", ex);
            }
        }
    }
}
