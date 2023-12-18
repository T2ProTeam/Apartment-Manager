using DocumentFormat.OpenXml.CustomProperties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Utilities
{

    public class ConvertDataTable
    {
        public DataTable Convert<T>(ObservableCollection<T> collection)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, propType);
            }
            foreach(T item in collection)
            {
                var values = new object[properties.Length];
                for(int i = 0; i<properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }

    }
}
