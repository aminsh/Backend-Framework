using Core;
using Core.ApiResult;
using Core.Bus;
using Core.Command;
using Core.DataAccess;
using Core.Domain.Contract;
using Core.Event;
using DataAccess;
using Microsoft.Practices.Unity;
using Presentation.App_Start;
using Utility;

namespace Presentation
{
    public static class IocConfig
    {
        public static IUnityContainer Register()
        {
            IUnityContainer container = new UnityContainer();
            
            DependencyManager.Container = container;

            container.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>(new PerThreadLifetimeManager());
            container.RegisterType<IValidationResult, ValidationResult>();
            container.RegisterType<ICommandBus, RabbitMQCommandBus>();
            container.RegisterType<IEventBus, EventBus>();
            container.RegisterType<ICurrent, Current>(new PerThreadLifetimeManager());

            //DependencyManager.Register<IUnitOfWork, EntityFrameworkUnitOfWork>(Lifetime.PerRequest);
            
            //DependencyManager.Register<IValidationResult, ValidationResult>(Lifetime.PerRequest);

            //DependencyManager.Register<ICurrent, FakeCurrent>(Lifetime.PerRequest);

            //DependencyManager.Register<ReportData, ReportData>(Lifetime.Singletone);


            container.RegisterType(
                typeof(IRepository<>),
                typeof(EntityFrameworkRepository<>),
                new InjectionFactory((ctr, type, str) =>
                {
                    var genericType = type.GenericTypeArguments[0];
                    var repo = typeof(IUnitOfWork)
                        .GetMethod("GetRepository")
                        .MakeGenericMethod(genericType)
                        .Invoke(container.Resolve<IUnitOfWork>(), new object[] { });

                    return repo;
                }));

            container.RegisterType(typeof(IReadRepository<>), typeof(EntityFrameworkReadRepository<>),
                new InjectionFactory(
                    (ctr, type, str) =>
                    {
                        var genericType = type.GenericTypeArguments[0];
                        var repo = typeof(IUnitOfWork)
                            .GetMethod("GetReadRepository")
                            .MakeGenericMethod(genericType)
                            .Invoke(container.Resolve<IUnitOfWork>(), new object[] { });

                        return repo;
                    }));

            return container;
        }
    }
}