using System;

namespace DevStorm.Infrastructure.Utility.Attributes
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
