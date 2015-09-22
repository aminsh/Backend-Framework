using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapFieldAttribute : Attribute
    {
        public string Name { get; set; }
        public MapType MapType  { get; set; }
        public MapFieldAttribute()
        {
            
        }
    }

    public enum MapType
    {
        ForeignKey,
        Date,
        Integer,
        String,
        Enum
    }
}
