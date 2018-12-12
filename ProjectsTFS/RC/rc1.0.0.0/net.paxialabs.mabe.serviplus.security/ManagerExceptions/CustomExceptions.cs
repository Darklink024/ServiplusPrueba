using log4net;
using net.paxialabs.mabe.serviplus.resource.Common;
using System;
using System.Resources;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions
{
    public class CustomExceptions : MasterException<CustomExceptions.ErrorCodes>
    {
        public enum ErrorCodes
        {
            E_C_Generic = -50201
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(CustomExceptions));

        public override ErrorCodes ErrorCode { get; internal set; }

        public override string ErrorDescription
        {
            get
            {
                var Manager = new ResourceManager("net.paxialabs.mabe.serviplus.resource.Common.ResourceExceptions", typeof(ResourceExceptions).Assembly);
                return Manager.GetString(this.ErrorCode.ToString());
            }
        }

        public CustomExceptions(Exception ex, CustomExceptions.ErrorCodes CodeError) : base(ex)
        {
            LoggerConfiguration.DefaultSetup();
            this.ErrorCode = CodeError;
            this.LoggerException();
        }

        protected override void LoggerException()
        {
            try
            {
                String errorDescription =
                                          Environment.NewLine + "GUI de Error: " + base.ErrorGUI.ToString() + Environment.NewLine +
                                          "Codigo de Error: " + ((int)this.ErrorCode).ToString() + Environment.NewLine +
                                          "Descripción del Error: " + this.ErrorDescription + Environment.NewLine +
                                          "Mensaje de Error: " + base.Message + Environment.NewLine +
                                          "Seguimiento de Pila: " + base.StackTrace;

                if (this.InnerException != null)
                    errorDescription += Environment.NewLine + "Excepción interna: " + Environment.NewLine +
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
