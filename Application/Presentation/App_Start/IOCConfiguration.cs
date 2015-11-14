using Core.Api;
using Core.ApiResult;
using Core.Bus;
using Core.Command;
using Core.DataAccess;
using Core.Domain.Contract;
using Core.IOC;
using Core.Provider;
using DataAccess;
using Microsoft.Practices.Unity;
using Utility;

namespace Presentation.App_Start
{
    public static class IOCConfiguration
    {
        public static IUnityContainer Register()
        {
            DependencyManager.Initialize();

            DependencyManager.Initialize<ICurrent, Current>(Lifetime.PerRequest);

            return DependencyManager.Container;
        }
    }
}