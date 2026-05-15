using System;

namespace SUBS.Core.EventBus
{
    public class SendEventException : Exception
    {
        public SendEventException(string msg)
            : base(msg)
        {
        }
    }
}