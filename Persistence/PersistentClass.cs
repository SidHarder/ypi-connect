using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPIConnect.Persistence
{
    public class PersistentClass : System.Attribute
    {
        private string m_TableName;

        public PersistentClass(string tableName)
        {
            this.m_TableName = tableName;
        }

        public string TableName
        {
            get { return this.m_TableName; }
        }
    }
}

