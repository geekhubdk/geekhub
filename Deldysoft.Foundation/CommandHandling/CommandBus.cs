using System;
using System.Collections.Generic;

namespace Deldysoft.Foundation.CommandHandling
{
    public class CommandBus
    {
        private readonly ICommandLoggerAdapter _commandLogger;
        private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

        public CommandBus(ICommandLoggerAdapter commandLogger)
        {
            _commandLogger = commandLogger;
        }

        public void RegisterHandler<T>(IHandleCommand<T> handler)
        {
            _handlers[typeof (T)] = handler;
        }

        public void RegisterHandler(Type type, object handler)
        {
            _handlers[type] = handler;
        }

        public void Execute<T>(T command)
        {
            _commandLogger.Log(command);
            var handler = (IHandleCommand<T>)_handlers[typeof(T)];
            handler.Execute(command);
        }
    }
}