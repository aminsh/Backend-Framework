using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ApiResult;
using DevStorm.Infrastructure.ApiResult;
using Core.Bus;
using Core.Domain;
using Core.IOC;
using Utility;

namespace Core.Command
{
    public class SendCommandToValidator : ISendCommandToValidator
    {
        public IValidationResult Validate(CommnadMessage message)
        {
            AppDomain.CurrentDomain.Load(AssemblyNameList.CommandValidation);
            var commandType = message.Command.GetType();

            var commnadValidationType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == AssemblyNameList.CommandValidation)
                .SelectMany(a => a.GetTypes())
                .SingleOrDefault(t => t.GetInterfaces().Any(ifc =>
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

            var instance = Activator.CreateInstance(commnadValidationType, arguments);
            commnadValidationType.GetProperty("Current").SetValue(instance, message.Current);

            validatorMethod.Invoke(instance, new object[] {message.Command});

            return instance.As<DomainValidator>().ValidationResult;
        }
    }
}