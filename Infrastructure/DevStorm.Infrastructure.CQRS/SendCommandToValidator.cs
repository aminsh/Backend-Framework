using System;
using System.Linq;
using DevStorm.Infrastructure.Core;
using DevStorm.Infrastructure.Core.Api;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.IOC;
using DevStorm.Infrastructure.Utility;

namespace DevStorm.Infrastructure.CQRS
{
    public class SendCommandToValidator : ISendCommandToValidator
    {
        public IValidationResult Validate(CommnadMessage message)
        {
            var commandType = message.Command.GetType();
            var commnadValidationType = TypeService.GetType(t => t.GetInterfaces().Any(ifc =>
                ifc.IsGenericType &&
                ifc.GetGenericTypeDefinition() == typeof (ICommandValidator<>) &&
                ifc.GetGenericArguments().First() == commandType));

            if (commnadValidationType == null)
                return new ValidationResult();

            var validatorMethod =
                commnadValidationType.GetMethods()
                    .Single(m =>
                        m.Name == "Validate" &&
                        m.GetParameters().First().ParameterType == commandType);

            var arguments =
                commnadValidationType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();

            dynamic instance = Activator.CreateInstance(commnadValidationType, arguments);
            instance.Current = message.Current;
            instance.ValidationResult = new ValidationResult();

            validatorMethod.Invoke(instance, new object[] {message.Command});
            return instance.ValidationResult;
        }
    }
}