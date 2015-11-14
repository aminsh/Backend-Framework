using System;
using Core.Api;
using Core.ApiResult;
using Core.Bus;
using Core.Command;
using Core.DataAccess;
using Core.Provider;
using DataAccess;
using Microsoft.Practices.Unity;
using Utility;

namespace Core.IOC
{
    public static class DependencyManager
    {
        public static IUnityContainer Container { get; set; }

        public static void Initialize()
        {
            Container = new UnityContainer();

            Initialize<ICookieProvider, CookieProvider>(Lifetime.Singletone);
            Initialize<IUnitOfWork, EntityFrameworkUnitOfWork>(Lifetime.PerRequest);
            Initialize<IValidationResult, ValidationResult>(Lifetime.PerRequest);
            Initialize<IResult, Result>(Lifetime.PerRequest);
            //Initialize<ICommandBus, RabbitMQCommandBus>();
            //Initialize<IEventBus, EventBus>();

            Container.RegisterType(
                typeof(IRepository<>),
                typeof(EntityFrameworkRepository<>),
                new InjectionFactory((ctr, type, str) =>
                {
                    var genericType = type.GenericTypeArguments[0];
                    var repo = typeof(IUnitOfWork)
                        .GetMethod("GetRepository")
                        .MakeGenericMethod(genericType)
                        .Invoke(Container.Resolve<IUnitOfWork>(), new object[] { });

                    return repo;
                }));
        }

        public static void Initialize<TFrom, TTo>(Lifetime lifetime) where TTo : TFrom
        {
            Container.RegisterType<TFrom, TTo>(GetLifetimeManager(lifetime));
        }

        public static void Initialize(Type type, object instance, Lifetime lifetime)
        {
            Container.RegisterInstance(type, instance, GetLifetimeManager(lifetime));
        }

        public static T Resolve<T>() where T : class
        {
            return Container.Resolve(typeof (T)).As<T>();
        }

        public static object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public static LifetimeManager GetLifetimeManager(Lifetime lifetime)
        {
            if(lifetime == Lifetime.NewInstance)
                return  new TransientLifetimeManager();
            if(lifetime == Lifetime.Singletone)
                return new ContainerControlledLifetimeManager();
            if(lifetime == Lifetime.PerRequest)
                return new PerThreadLifetimeManager();
            return null;
        }
    }

    public enum Lifetime
    {
        NewInstance,
        Singletone,
        PerRequest
    }
}