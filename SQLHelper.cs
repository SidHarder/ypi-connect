using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using YPIConnect.Persistence;
using System.Reflection;

namespace YPIConnect
{
    public class SQLHelper
    {
        public static string GetSaveCommandText(Type type, object domainObject, RowOperationTypeEnum rowOperationType)
        {
            string commandText = "";
            switch (rowOperationType)
            {
                case RowOperationTypeEnum.Insert:
                    commandText = SQLHelper.GetInsertCommands(type, domainObject);
                    break;
                case RowOperationTypeEnum.Update:
                    commandText = SQLHelper.GetUpdateCommands(type, domainObject);
                    break;
                case RowOperationTypeEnum.Delete:
                    commandText = SQLHelper.GetDeleteCommands(type, domainObject);
                    break;
                default:
                    throw new Exception("This row operation type is not handled.");
            }
            return commandText;
        }

        public static string GetInsertCommands(Type type, object domainObject)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttribute(typeof(PersistentClass));
            string commandText = "Insert " + persistentClassAttribute.TableName + " ([FIELDLIST]) values ([VALUELIST]);";
            commandText = commandText.Replace("[FIELDLIST]", GetSqlInsertFieldList(type));
            commandText = commandText.Replace("[VALUELIST]", GetSqlInsertValueList(type, domainObject));
            return commandText;
        }

        public static string GetUpdateCommands(Type type, object domainObject)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttribute(typeof(PersistentClass));
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PersistentProperty persistentProperty = (PersistentProperty)primaryKeyProperty.GetCustomAttribute(typeof(PersistentProperty));

            string commandText = "Update " + persistentClassAttribute.TableName + " set [FIELDLIST] where " + persistentProperty.SqlName + " = '" + primaryKeyProperty.GetValue(domainObject) + "';";
            commandText = commandText.Replace("[FIELDLIST]", GetSqlUpdateFieldList(type, domainObject));
            return commandText;
        }

        public static string GetDeleteCommands(Type type, object domainObject)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttribute(typeof(PersistentClass));
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PersistentProperty persistentProperty = (PersistentProperty)primaryKeyProperty.GetCustomAttribute(typeof(PersistentProperty));
            string commandText = "Delete from " + persistentClassAttribute.TableName + " where " + persistentProperty.SqlName + " = '" + primaryKeyProperty.GetValue(domainObject) + "';";
            return commandText;
        }

        public static string GetSqlInsertFieldList(Type type)
        {
            string result = "";
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToList();
            foreach (PropertyInfo property in propertyList)
            {
                PersistentProperty persistentProperty = (PersistentProperty)property.GetCustomAttribute(typeof(PersistentProperty));
                result = result + persistentProperty.SqlName + ", ";
            }
            result = result.Substring(0, result.Length - 2);
            return result;
        }

        public static string GetSqlUpdateFieldList(Type type, object domainObject)
        {
            string result = "";
            PropertyInfo primaryKeyProperty = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToList();
            foreach (PropertyInfo property in propertyList)
            {
                if (property.Name != primaryKeyProperty.Name)
                {
                    PersistentProperty persistentProperty = (PersistentProperty)property.GetCustomAttribute(typeof(PersistentProperty));
                    switch (property.PropertyType.Name)
                    {
                        case "Nullable`1":
                            Type nullableType = property.PropertyType.GetGenericArguments()[0];
                            switch (nullableType.Name)
                            {
                                case "Int32":
                                    if (property.GetValue(domainObject) == null)
                                    {
                                        result = result + persistentProperty.SqlName + "=null, ";
                                    }
                                    else
                                    {
                                        Int32 nullableInt32 = Int32.Parse(property.GetValue(domainObject).ToString());
                                        result = result + persistentProperty.SqlName + "= " + nullableInt32 + ", ";
                                    }
                                    break;
                                case "DateTime":
                                    if (property.GetValue(domainObject) == null)
                                    {
                                        result = result + persistentProperty.SqlName + "=null, ";
                                    }
                                    else
                                    {
                                        DateTime nullableDateTime = DateTime.Parse(property.GetValue(domainObject).ToString());
                                        result = result + persistentProperty.SqlName + "='" + nullableDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                                    }
                                    break;
                                default:
                                    throw new Exception("Nullable type not implemented yet.");
                            }

                            break;
                        case "Boolean":
                            result = result + persistentProperty.SqlName + " = '" + Convert.ToInt32(property.GetValue(domainObject)) + "', ";
                            break;
                        case "DateTime":
                            DateTime dateTime = (DateTime)property.GetValue(domainObject);
                            result = result + persistentProperty.SqlName + " = '" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                            break;
                        default:
                            if (property.GetValue(domainObject) == null || property.GetValue(domainObject).ToString() == "")
                            {
                                result = result + persistentProperty.SqlName + "=null, ";
                            }
                            else
                            {
                                result = result + persistentProperty.SqlName + " = '" + property.GetValue(domainObject).ToString().Replace("'", "''") + "', ";
                            }
                            break;
                    }
                }
            }
            result = result.Substring(0, result.Length - 2);
            return result;
        }

        public static string GetSqlInsertValueList(Type type, object domainObject)
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToList();
            foreach (PropertyInfo property in propertyList)
            {
                switch (property.PropertyType.Name)
                {
                    case "Nullable`1":
                        Type nullableType = property.PropertyType.GetGenericArguments()[0];
                        switch (nullableType.Name)
                        {
                            case "Int32":
                                if (property.GetValue(domainObject) == null)
                                {
                                    result.Append("null, ");
                                }
                                else
                                {
                                    Int32 nullableInt32 = Int32.Parse(property.GetValue(domainObject).ToString());
                                    result.Append(property.GetValue(domainObject).ToString() + ", ");
                                }
                                break;
                            case "DateTime":
                                if (property.GetValue(domainObject) == null)
                                {
                                    result.Append("null, ");
                                }
                                else
                                {
                                    DateTime date = DateTime.Parse(property.GetValue(domainObject).ToString());
                                    result.Append("'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                                }
                                break;
                        }
                        break;
                    case "Boolean":
                        result.Append("'" + Convert.ToInt32(property.GetValue(domainObject)) + "', ");
                        break;
                    default:
                        if (property.GetValue(domainObject) == null || property.GetValue(domainObject).ToString() == "")
                        {
                            result.Append("null, ");
                        }
                        else
                        {
                            result.Append("'" + property.GetValue(domainObject).ToString().Replace("'", "''") + "', ");
                        }
                        break;
                }
            }
            result = result.Remove(result.Length - 2, 2);
            return result.ToString();
        }
    }
}
