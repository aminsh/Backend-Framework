using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Bus;
using Core.Domain;
using Core.IOC;
using Utility;

namespace Core.Command
{
    public class SendCommandToHandler : ISendCommandToHandler
    {
        public object Handle(CommnadMessage message)
        {
            var command = message.Command;
            var current = message.Current;

            AppDomain.CurrentDomain.Load(AssemblyNameList.CommandHandlers);

            var commandType = command.GetType();

            var commnadHandlerType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == AssemblyNameList.CommandHandlers)
                .SelectMany(a => a.GetTypes())
                .Single(t => t.GetInterfaces().Any(ifc =>
                    ifc.IsGenericType &&
                    ifc.GetGenericTypeDefinition() == typeof(ICommandHandler<>) &&
                    ifc.GetGenericArguments().First() == commandType));

            var handleMethod =
                commnadHandlerType.GetMethods()
                    .Single(m =>
                        m.Name == "Handle" &&
                        m.GetParameters().First().ParameterType == commandType);
            var arguments =
                commnadHandlerType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();

            var instance = Activator.CreateInstance(commnadHandlerType, arguments);

            commnadHandlerType.GetProperty("Current").SetValue(instance, current);

            handleMethod.Invoke(instance, new object[] { command });

            return instance.As<DomainService>().ReturnValue;
        }
    }
}
