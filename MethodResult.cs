using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace YPIConnect
{
    public class MethodResult
    {
        private List<string> m_Messages;

        private object m_Result;
        private bool m_Success;

        public MethodResult()
        {
            this.m_Messages = new List<string>();
            this.m_Success = true;
        }

        public void AddResult(JObject apiResult)
        {
            this.m_Messages.Add(apiResult["result"]["message"].ToString());
            if (apiResult["result"]["status"].ToString() == "OK")
            {
                this.Success = true;
            }
            else
            {
                this.Success = false;
            }
        }

        public void AddResult(JObject apiResult, Object result)
        {
            this.m_Messages.Add(apiResult["result"]["message"].ToString());
            if (apiResult["result"]["status"].ToString() == "OK")
            {
                this.Success = true;
            }
            else
            {
                this.Success = false;
            }

            this.m_Result = result;
        }

        public List<string> Messages
        {
            get { return this.m_Messages; }
        }

        public bool Success
        {
            get { return this.m_Success; }
            set { this.m_Success = value; }
        }

        public object Result
        {
            get { return this.m_Result; }
            set { this.m_Result = value; }
        }

        public string GetMessage()
        {
            StringBuilder result = new StringBuilder();
            foreach (string str in this.m_Messages)
            {
                result.AppendLine(str);
            }
            return result.ToString().TrimEnd();
        }

        public void ShowMethodResult()
        {
            MessageBox.Show(this.GetMessage());
        }

        public void ShowIfFailure()
        {
            if (this.Success == false)
            {
                MessageBox.Show(this.GetMessage());
            }
        }
    }
}
