using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YPIConnect
{
    public class DistributionLogCollection : ObservableCollection<DistributionLog>
    {
        private static string FIELDLIST = "select tord.TestOrderReportDistributionId, tord.ReportNo, tord.PhysicianName, tord.PhysicianId, tord.ClientName, tord.ClientId, ao.PLastName, ao.PFirstName, ao.PBirthdate, tord.DistributionType, tord.Distributed, tord.TimeOfLastDistribution ";

        public DistributionLogCollection()
        {

        }

        public static MethodResult GetByRecentCases()
        {
            string clientIdList = AuthenticatedUser.Instance.GetSQLClientIdInList();

            DistributionLogCollection distributionLogCollection = new DistributionLogCollection();
            string commandText = FIELDLIST;
            commandText += "from tblTestOrderReportDistribution tord ";
            commandText += "join tblPanelSetOrder pso on tord.ReportNo = pso.ReportNo ";
            commandText += "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo ";
            commandText += $"where tord.ClientId in (" + clientIdList + ") and tord.Distributed = 1 and tord.timeOfLastDistribution >= '2020-09-15';";

            JObject jsonRequest = APIRequestHelper.CreateSubmitSQLCommandMessage(commandText);
            APIResult apiResult = APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
            DistributionLogCollection.Build(apiResult.JSONResult, distributionLogCollection);

            MethodResult methodResult = new MethodResult();
            methodResult.AddResult(apiResult.JSONResult, distributionLogCollection);
            return methodResult;
        }

        public static MethodResult GetByNotAcknowledged()
        {
            string clientIdList = AuthenticatedUser.Instance.GetSQLClientIdInList();

            DistributionLogCollection distributionLogCollection = new DistributionLogCollection();
            string commandText = FIELDLIST;
            commandText += "from tblTestOrderReportDistributionLog tord ";
            commandText += "join tblPanelSetOrder pso on tord.ReportNo = pso.ReportNo ";
            commandText += "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo ";
            commandText += "where tord.ClientId in (" + clientIdList + ") and tord.DistributionType = 'Web Service' and tord.CaseDistributed = 0;";
            
            JObject jsonRequest = APIRequestHelper.CreateSubmitSQLCommandMessage(commandText);
            APIResult apiResult = APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
            DistributionLogCollection.Build(apiResult.JSONResult, distributionLogCollection);

            MethodResult methodResult = new MethodResult();
            methodResult.AddResult(apiResult.JSONResult, distributionLogCollection);
            return methodResult;            
        }

        public static MethodResult GetByDateOfBirth(DateTime dateOfBirth)
        {
            string clientIdList = AuthenticatedUser.Instance.GetSQLClientIdInList();

            DistributionLogCollection distributionLogCollection = new DistributionLogCollection();
            string commandText = FIELDLIST;
            commandText += "from tblTestOrderReportDistribution tord ";
            commandText += "join tblPanelSetOrder pso on tord.ReportNo = pso.ReportNo ";
            commandText += "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo ";
            commandText += "where tord.ClientId in (" + clientIdList + ") and ao.PBirthDate = '" + dateOfBirth.ToString("yyyyMMdd")  + "';";

            JObject jsonRequest = APIRequestHelper.CreateSubmitSQLCommandMessage(commandText);
            APIResult apiResult = APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
            DistributionLogCollection.Build(apiResult.JSONResult, distributionLogCollection);

            MethodResult methodResult = new MethodResult();
            methodResult.AddResult(apiResult.JSONResult, distributionLogCollection);
            return methodResult;
        }

        public static MethodResult GetByPatientName(PatientName patientName)
        {
            string clientIdList = AuthenticatedUser.Instance.GetSQLClientIdInList();

            DistributionLogCollection distributionLogCollection = new DistributionLogCollection();
            string commandText = FIELDLIST;
            commandText += "from tblTestOrderReportDistribution tord ";
            commandText += "join tblPanelSetOrder pso on tord.ReportNo = pso.ReportNo ";
            commandText += "join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo ";
            commandText += "where tord.ClientId in (" + clientIdList + ") ";

            if (patientName.LastName != null && patientName.FirstName != null)
            {
                commandText = commandText + "and PLastName like '" + patientName.LastName + "%' and pFirstName like '" + patientName.FirstName + "%' order by ao.AccessionDate desc";
            }

            if (patientName.LastName != null && patientName.FirstName == null)
            {
                commandText = commandText + "and PLastName like '" + patientName.LastName + "%' order by ao.AccessionDate desc;";
            }

            if (patientName.LastName != null)
            {
                JObject jsonRequest = APIRequestHelper.CreateSubmitSQLCommandMessage(commandText);
                APIResult apiResult = APIRequestHelper.SubmitAPIRequestMessage(jsonRequest);
                DistributionLogCollection.Build(apiResult.JSONResult, distributionLogCollection);

                MethodResult methodResult = new MethodResult();
                methodResult.AddResult(apiResult.JSONResult, distributionLogCollection);
                return methodResult;
            }
            else
            {
                MethodResult methodResult = new MethodResult();
                methodResult.Success = false;
                methodResult.Messages.Add("Invalid patient name.");
                return methodResult;
            }
        }

        public static void Build(JObject jsonResult, DistributionLogCollection distributionLogCollection)
        {
            if (jsonResult["result"]["queryResult"] != null)
            {
                for (int i = 0; i < jsonResult["result"]["queryResult"].Count(); i++)
                {
                    DistributionLog distributionLog = new DistributionLog();
                    distributionLog.FromSql((JObject)jsonResult["result"]["queryResult"][i]);
                    distributionLogCollection.Add(distributionLog);
                }
            }
        }       
    }
}
