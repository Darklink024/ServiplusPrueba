using log4net;
using System;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons
{
    public sealed class Argument_Exception : ArgumentException
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Argument_Exception));
        private readonly int ErrorCode = -888;
        public int ErrorCodeException { get { return this.ErrorCode; } }

        public Argument_Exception(ArgumentException ex) : base(ex.Message, ex)
        {
            LoggerConfiguration.DefaultSetup();
            this.LoggerException();
        }

        private void LoggerException()
        {
            try
            {
                String errorDescription =
                                             Environment.NewLine + "Codigo de Error: " + this.ErrorCodeException.ToString() + Environment.NewLine +
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
