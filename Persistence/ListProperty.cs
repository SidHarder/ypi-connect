using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPIConnect.Persistence
{
    public class ListProperty : System.Attribute
    {
        private string m_SqlPropertyName;

        public ListProperty(string sqlPropertyName)
        {
            this.m_SqlPropertyName = sqlPropertyName;
        }

        public string SqlPropertyName
        {
            get { return this.m_SqlPropertyName; }
        }
    }
}

