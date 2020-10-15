using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Windows;
using YPIConnect.Persistence;

namespace YPIConnect
{
    public class JsonObjectWriter
    {
        public JsonObjectWriter()
        {

        }

        public static void WriteFromSql(Type type, JObject jObject, Object obj)
        {
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToList();
            foreach (PropertyInfo property in propertyList)
            {
                PersistentProperty persistentProperty = (PersistentProperty)property.GetCustomAttributes(typeof(PersistentProperty), false)[0];
                if (jObject[persistentProperty.SqlName] != null)
                {
                    switch (property.PropertyType.Name)
                    {
                        case "String":
                            property.SetValue(obj, jObject[persistentProperty.SqlName].ToString());
                            break;
                        case "Int32":
                            property.SetValue(obj, Convert.ToInt32(jObject[persistentProperty.SqlName].ToString()));
                            break;
                        case "JObject":
                            if (jObject[persistentProperty.SqlName].ToString() != "")
                            {
                                property.SetValue(obj, JObject.Parse(jObject[persistentProperty.SqlName].ToString()));
                            }
                            break;
                        case "Boolean":
                            property.SetValue(obj, Convert.ToBoolean(Convert.ToInt32(jObject[persistentProperty.SqlName].ToString())));
                            break;
                        case "DateTime":
                            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                            property.SetValue(obj, DateTime.Parse(jObject[persistentProperty.SqlName].ToString()));
                            break;
                        case "Nullable`1":
                            Type nullableType = property.PropertyType.GetGenericArguments()[0];
                            switch (nullableType.Name)
                            {
                                case "Int32":
                                    if (jObject[persistentProperty.SqlName].ToString() != "")
                                    {
                                        property.SetValue(obj, Int32.Parse(jObject[persistentProperty.SqlName].ToString()));
                                    }
                                    break;
                                case "DateTime":
                                    if (jObject[persistentProperty.SqlName].ToString() != "")
                                    {
                                        property.SetValue(obj, DateTime.Parse(jObject[persistentProperty.SqlName].ToString()));
                                    }
                                    break;
                                default:
                                    throw new Exception("Nullable type not implemented yet.");
                            }
                            break;
                        default:
                            throw new Exception("Data Type not handled in JSON Object Writer");
                    }
                }
            }
        }

        public static void WriteListObject(Type type, JObject jObject, Object obj)
        {
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ListProperty))).ToList();
            foreach (PropertyInfo property in propertyList)
            {
                ListProperty listProperty = (ListProperty)property.GetCustomAttributes(typeof(ListProperty), false)[0];
                if (jObject[listProperty.SqlPropertyName] != null)
                {
                    switch (property.PropertyType.Name)
                    {
                        case "String":
                            property.SetValue(obj, jObject[listProperty.SqlPropertyName].ToString());
                            break;
                        case "Boolean":
                            property.SetValue(obj, Convert.ToBoolean(Convert.ToInt32(jObject[listProperty.SqlPropertyName].ToString())));
                            break;
                        case "DateTime":
                            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                            property.SetValue(obj, DateTime.Parse(jObject[listProperty.SqlPropertyName].ToString()));
                            break;
                        case "Nullable`1":
                            Type nullableType = property.PropertyType.GetGenericArguments()[0];
                            switch (nullableType.Name)
                            {
                                case "Int32":
                                    if (jObject[listProperty.SqlPropertyName].ToString() != "")
                                    {
                                        property.SetValue(obj, Int32.Parse(jObject[listProperty.SqlPropertyName].ToString()));
                                    }
                                    break;
                                case "DateTime":
                                    if (jObject[listProperty.SqlPropertyName].ToString() != "")
                                    {
                                        property.SetValue(obj, DateTime.Parse(jObject[listProperty.SqlPropertyName].ToString()));
                                    }
                                    break;
                                default:
                                    throw new Exception("Nullable type not implemented yet.");
                            }
                            break;
                        case "Int32":
                            property.SetValue(obj, Convert.ToInt32(jObject[listProperty.SqlPropertyName].ToString()));
                            break;
                        default:
                            MessageBox.Show(property.PropertyType.Name);
                            break;
                    }
                }
            }
        }
    }
}
