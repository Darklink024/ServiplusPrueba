using System;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions
{
    public abstract class MasterException<T> : Exception
    {
        public abstract T ErrorCode { get; internal set; }
        public Guid ErrorGUI { get; internal set; }
        public abstract string ErrorDescription { get; }
        protected MasterException(Exception ex) : base(ex.Message, ex) { this.ErrorGUI = Guid.NewGuid(); }
        protected abstract void LoggerException();

    }
}
