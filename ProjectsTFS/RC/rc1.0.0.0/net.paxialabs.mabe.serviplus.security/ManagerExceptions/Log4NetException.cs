using System;

namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions
{
    public sealed class Log4NetException : Exception
    {
        public Log4NetException() {; }
        public Log4NetException(string message) : base(message) {; }
        public Log4NetException(string message, Exception inner) : base(message, inner) {; }
    }
}
