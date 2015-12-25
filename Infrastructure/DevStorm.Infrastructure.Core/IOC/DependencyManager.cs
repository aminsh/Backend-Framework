using System;
using DevStorm.Infrastructure.Utility;
using Microsoft.Practices.Unity;

namespace DevStorm.Infrastructure.Core.IOC
{
    public static class DependencyManager
    {
        public static IUnityContainer Container { get; private set; }

        public static void Init()
        {
            Container = new UnityContainer();
        }

        public static void Register<TFrom, TTo>(Lifetime lifetime) where TTo : TFrom
        {
            Container.RegisterType<TFrom, TTo>(GetLifetimeManager(lifetime));
        }

        public static void Register(Type type, object instance, Lifetime lifetime)
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