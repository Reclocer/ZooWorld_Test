using System;

namespace SUBS.Core.EventBus
{
    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg)
        {
        }
    }
}