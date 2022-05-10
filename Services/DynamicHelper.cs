using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace PersonAPI.Services
{
    public class DynamicHelper
    {
        /// <summary>
        /// Loops through the props of an object and select only the ones that match the list provided
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propNames">List with props that should display</param>
        /// <returns></returns>
        public static ExpandoObject ConvertToExpando(object obj, List<string> propNames)
        {
            //Get Properties Using Reflections
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] properties = obj.GetType().GetProperties(flags);

            //Add Them to a new Expando
            ExpandoObject expando = new ExpandoObject();
            foreach (PropertyInfo property in properties)
            {
                if (propNames.Contains(property.Name))
                {
                    AddProperty(expando, property.Name, property.GetValue(obj));
                }                
            }

            return expando;
        }
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            //Take use of the IDictionary implementation
            var expandoDict = expando as IDictionary<String, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
