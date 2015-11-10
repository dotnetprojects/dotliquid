using System;
using System.Linq;
using System.Reflection;

namespace DotLiquid.Util
{
	public static class ObjectExtensionMethods
	{
#if NET35
        public static bool RespondTo(this object value, string member)
        {
            return value.RespondTo(member, true);
        }

        public static bool RespondTo(this object value, string member, bool ensureNoParameters)
#else
		public static bool RespondTo(this object value, string member, bool ensureNoParameters = true)
#endif
		{
			if (value == null)
				throw new ArgumentNullException("value");

			Type type = value.GetType();

			MethodInfo methodInfo = type.GetMethod(member);
			if (methodInfo != null && (!ensureNoParameters || !methodInfo.GetParameters().Any()))
				return true;

			PropertyInfo propertyInfo = null;
			var mbs = member.Split('.');
			foreach (var mb in mbs)
			{
				propertyInfo = type.GetProperty(mb.Replace("_", ""));
				if (propertyInfo == null)
					return false;

				type = propertyInfo.PropertyType;
			}

			if (propertyInfo != null && propertyInfo.CanRead)
				return true;

			return false;
		}

#if NET35
        public static object Send(this object value, string member)
        {
            return value.Send(member, null);
        }

        public static object Send(this object value, string member, object[] parameters)
#else
		public static object Send(this object value, string member, object[] parameters = null)
#endif
		{
			if (value == null)
				throw new ArgumentNullException("value");

			Type type = value.GetType();

			MethodInfo methodInfo = type.GetMethod(member);
			if (methodInfo != null)
				return methodInfo.Invoke(value, parameters);

			PropertyInfo propertyInfo = null;
			object gValueFrom = value;
			var mbs = member.Split('.');
			foreach (var mb in mbs)
			{
				propertyInfo = type.GetProperty(mb.Replace("_", ""));
				if (propertyInfo == null)
					return null;

				type = propertyInfo.PropertyType;
				gValueFrom = propertyInfo.GetValue(gValueFrom, null);
			}

			if (propertyInfo != null && propertyInfo.CanRead)
				return gValueFrom;

			return null;
		}
	}
}