using log4net;
using System;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions.Commons
{
    public sealed class Generic_Exception : MasterException<Generic_Exception.ErrorCodes>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Generic_Exception));

        public enum ErrorCodes
        {
            //TODO: Definir los codigos de error genericos
            DataBaseErrorNotDefined = -10,
            DataBaseError = -11,
            ErrorGoogleMaps = -12
        };

        public override ErrorCodes ErrorCode { get; internal set; }

        public override string ErrorDescription
        {
            get
            {
                // new ResourceManager("GenericLanguage_" + GlobalConfiguration.GetLanguageDefault, Assembly.GetExecutingAssembly()).GetString(this.ErrorCode.ToString())
                return this.Message;
            }
        }

        public Generic_Exception(Exception ex, Generic_Exception.ErrorCodes CodeError) : base(ex)
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
                                          "Codigo de Error: " + this.ErrorCode.ToString() + Environment.NewLine +
                                          "Descripción del Error: " + this.ErrorDescription + Environment.NewLine +
                                          "Mensaje de Error: " + Environment.NewLine + base.Message + Environment.NewLine +
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
