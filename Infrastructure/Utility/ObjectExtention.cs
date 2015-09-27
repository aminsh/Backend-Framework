using System;
using System.Web.Script.Serialization;

namespace Utility
{
    public static class ObjectExtention
    {
        public static TType As<TType>(this object obj)
        {
            return (TType) obj;
        }

        public static TType Convert<TType>(this object obj)
        {
            return System.Convert.ChangeType(obj, typeof (TType)).As<TType>();
        }

        public static string Serialize(this object source)
        {
            return new JavaScriptSerializer().Serialize(source);
        }

        public static T Deserialize<T>(string message)
        {
            return new JavaScriptSerializer().Deserialize<T>(message);
        }

        public static object Deserialize(string message , Type type)
        {
            return new JavaScriptSerializer().Deserialize(message, type);
        }
    }
}
