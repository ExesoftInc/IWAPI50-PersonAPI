using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PersonAPI.Services
{
    public static class ClassExtension
    {
        internal struct PropChanged 
        {
            internal string PropName;
            internal string OldValue;
            internal string NewValue;
        }

        internal static T GetNewObject<T>()
        {
            try
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return default(T);
            }

        }

        internal static List<PropertyInfo> GetPropList<T>()
        {
            object instance = GetNewObject<T>();

            return GetPropList<T>(instance);
        }

        internal static List<PropertyInfo> GetPropList<T>(object instance)
        {
            List<PropertyInfo> lstProps = new List<PropertyInfo>();

            Type instanceType = instance.GetType();
            lstProps = instanceType.GetProperties().ToList();

            return lstProps;
        }

        internal static List<FieldInfo> GetFieldList<T>()
        {
            object instance = GetNewObject<T>();

            return GetFieldList<T>(instance);
        }

        internal static List<FieldInfo> GetFieldList<T>(object instance)
        {
            List<FieldInfo> lstFields = new List<FieldInfo>();

            Type instanceType = instance.GetType();
            lstFields = instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).ToList();

            return lstFields;
        }

        internal static List<PropChanged> GetDelta<T>(T o1, T o2)
        {
            List<PropChanged> props = new List<PropChanged>();
            var propInfos = GetPropList<T>(o1);
            foreach (PropertyInfo pi in propInfos)
            {                
                var value1 = pi.GetValue(o1);
                var value2 = pi.GetValue(o2);
				if(value1 is null || value2 is null) continue;
                if (value1.GetType().IsGenericType) continue;

                if (!value1.Equals(value2))
                {
                    var propChanged = new PropChanged
                    {
                        PropName = pi.Name,
                        OldValue = value1.ToString(),
                        NewValue = value2.ToString()
                    };

                    props.Add(propChanged);
                }
            }

            return props;
        }
    }
}