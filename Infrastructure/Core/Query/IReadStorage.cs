using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.DynamicLinq;

namespace Core.Query
{
    public interface IReadStorage
    {
        IQueryable<TView> Get<TView>() where TView : class, new();

        TView GetById<TView>(object id) where TView : class, new();
    }
}