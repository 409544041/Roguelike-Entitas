﻿namespace Assets.Sources.Helpers.Entitas
{
	using System;
	using System.Linq;

	internal static class TypeExtensions
	{
		public static Phase GetPhase(this Type type)
		{
			var attr = type.GetCustomAttributes(typeof(SystemPhaseAttribute), true).FirstOrDefault() as SystemPhaseAttribute;
			if (attr != null)
			{
				return attr.Phase;
			}

			throw new ArgumentException(string.Format("System {0} does not have a SystemPhase attribute!", type.Name));
		}

		public static T GetCustomAttribute<T>(this Type type) where T : class
		{
			return type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
		}

		public static R GetCustomAttributeValue<T, R>(this Type type, Func<T, R> selector) where T : class
		{
			var attr = type.GetCustomAttribute<T>();

			return attr == null ? default(R) : selector(attr);
		}
	}
}
