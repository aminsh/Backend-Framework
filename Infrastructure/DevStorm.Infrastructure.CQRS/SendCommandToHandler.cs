using System;
using System.Linq;
using DevStorm.Infrastructure.Core;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.DataAccess;
using DevStorm.Infrastructure.Core.Domain;
using DevStorm.Infrastructure.Core.IOC;
using DevStorm.Infrastructure.Utility;

namespace DevStorm.Infrastructure.CQRS
{
    public class SendCommandToHandler : ISendCommandToHandler
    {
        public object Handle(CommnadMessage message)
        {
            var command = message.Command;
            var current = message.Current;

            var commandType = command.GetType();

            var commnadHandlerType = TypeService.GetType(
                t => t.GetInterfaces().Any(ifc =>
                    ifc.IsGenericType &&
                    ifc.GetGenericTypeDefinition() == typeof (ICommandHandler<>) &&
                    ifc.GetGenericArguments().First() == commandType));

            var handleMethod =
                commnadHandlerType.GetMethods()
                    .Single(m =>
                        m.Name == "Handle" &&
                        m.GetParameters().First().ParameterType == commandType);

            var arguments =
                commnadHandlerType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();

            dynamic instance = Activator.CreateInstance(commnadHandlerType, arguments);
            instance.Current = current;
            instance.UnitOfWork = DependencyManager.Resolve<IUnitOfWork>();

            handleMethod.Invoke(instance, new object[] { command });

            return instance.ReturnValue;
        }
    }
}
