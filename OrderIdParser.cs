using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPIConnect
{
	public class OrderIdParser
	{
	
		private static readonly string LegacyReportNoPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d-\d+";
		private static readonly string LegacyReportNoNumberPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d-(?<ReportNoNumber>\d+)";
		private static readonly string LegacyReportNoLetterPattern = @"(?<ReportNoLetter>[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*)\d\d-\d+";
		private static readonly string LegacyReportNoYearPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*(?<ReportNoYear>\d\d)-\d+";
		private static readonly string LegacyPSAReportNoPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d\d+";

		private static readonly string LegacyMasterAccessionNoPattern = @"^\d+";
		private static readonly string MasterAccessionNoPattern = @"^\d\d-\d+";

		private static readonly string ReportNoPattern = @"^\d\d-\d+.[AaBbFfIiMmPpRrSsTtYy]{1}\d*";
		private static readonly string ReportNoLetterPattern = @"^\d\d-\d+.(?<ReportNoPrefix>[AaBbFfIiMmPpRrSsTtYyQq])\d*";
		private static readonly string ReportNoNumberPattern = @"^\d\d-\d+.[AaBbFfIiMmPpRrSsTtYyQq](?<ReportNoNumber>\d*)";
		private static readonly string ReportNoYearPattern = @"^(?<ReportNoYear>\d\d)-\d+.[AaBbFfIiMmPpRrSsTtYyQq]{1}\d*";

		private static readonly string PanelOrderIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+";
		private static readonly string PanelOrderNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO(?<PanelOrderNo>\d+)";

		private static readonly string TestOrderIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+.TO\d+";
		private static readonly string TestOrderNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+.TO(?<TestOrderNo>\d+)";

		private static readonly string SurgicalSpecimenIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+";
		private static readonly string SurgicalSpecimenNoPattern = @"^\d\d-\d+.[Ss]\.SSR+(?<SurgicalSpecimenNo>\d+)";

		private static readonly string StainResultIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.STR\d+";
		private static readonly string StainResultNoPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.STR(?<StainResultNo>\d+)";

		private static readonly string IntraoperativeConsultationResultIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.IC\d+";
		private static readonly string IntraoperativeConsultationResultNoPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.IC(?<IntraopResultNo>\d+)";

		private static readonly string SurgicalAuditIdPattern = @"^\d\d-\d+.[Ss]\.SRA\d+";
		private static readonly string SurgicalAuditNoPattern = @"^\d\d-\d+.[Ss]\.SRA+(?<SurgicalAuditNo>\d+)";

		private static readonly string SurgicalSpecimenAuditIdPattern = @"^\d\d-\d+.[Ss]\.SRA\d+\.SSRA\d+";
		private static readonly string SurgicalSpecimenAuditNoPattern = @"^\d\d-\d+.[Ss]\.SRA+\d+\.SSRA(?<SurgicalSpecimenAuditNo>\d+)";

		private static readonly string CptBillingCodeIdPattern = @"^\d\d-\d+.CPT\d+";
		private static readonly string CptBillingCodeNoPattern = @"^\d\d-\d+.CPT(?<CptBillingCodeNo>\d+)";

		private static readonly string Icd9BillingCodeIdPattern = @"^\d\d-\d+.ICD\d+";
		private static readonly string Icd9BillingCodeNoPattern = @"^\d\d-\d+.ICD(?<IcdBillingCodeNo>\d+)";

		private static readonly string TaskOrderIdPattern = @"^\d\d-\d+.TSKO\d+";
		private static readonly string TaskOrderNoPattern = @"^\d\d-\d+.TSKO(?<TaskOrderNo>\d+)";

		private static readonly string TaskOrderDetailIdPattern = @"^\d\d-\d+.TSKO\d+\.TSKOD\d+";
		private static readonly string TaskOrderDetailNoPattern = @"^\d\d-\d+.TSKO\d+\.TSKOD(?<TaskOrderDetailNo>\d+)";

		private static readonly string AmendmentIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.AM\d+";
		private static readonly string AmendmentNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.AM(?<AmendmentNo>\d+)";

		private static readonly string FlowMarkerIdPattern = @"^\d\d-\d+.[Ff]{1}\d*.FM\d+";
		private static readonly string FlowMarkerNoPattern = @"^\d\d-\d+.[Ff]{1}\d*.FM(?<FlowMarkerNo>\d+)";

		private static readonly string PanelSetOrderBillableIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PSOB\d+";
		private static readonly string PanelSetOrderBillableNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PSOB(?<PanelSetOrderBillableNo>\d+)";

		private static readonly string SpecimenOrderIdPattern = @"^\d\d-\d+\.\d+";
		private static readonly string SpecimenOrderNoPattern = @"^\d\d-\d+\.(?<SpecimenOrderNo>\d+)";

		private static readonly string PanelSetOrderCPTCodeNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.CPT(?<PanelSetOrderCPTCodeNo>\d+)";
		private static readonly string PanelSetOrderCPTCodeBillNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.BLL(?<PanelSetOrderCPTCodeBillNo>\d+)";

		private static readonly string AliquotOrderBlockIdPattern = @"^\d\d-\d+\.\d+[A-Z](?<AliquotLetter>[A-Z]|$)";
		private static readonly string AliquotOrderBlockLetterPattern = @"^\d\d-\d+\.\d+(?<AliquotLetter>[A-Z])";
		private static readonly string AliquotOrderBlockLetter2Pattern = @"^\d\d-\d+\.\d+[A-Z](?<AliquotLetter2>[A-Z])";
		private static readonly string AliquotOrderAlqIdPattern = @"^\d\d-\d+\.\d+\.\d+";
		private static readonly string AliquotOrderAlqNoPattern = @"^\d\d-\d+\.\d+\.(?<AliquotOrderNo>\d+)";

		private static readonly string SlideOrderIdBlockPattern = @"^\d\d-\d+\.\d+[A-Z]+\d+";
		private static readonly string SlideOrderNoBlockPattern = @"^\d\d-\d+\.\d+[A-Z]+(?<SlideOrderNo>\d+)";
	

		private string m_IdToParse;

		public OrderIdParser(string idToParse)
		{
			this.m_IdToParse = idToParse;
		}

		private static int GetIdNumber(string idString, string prefix)
		{
			int result = 0;
			if (idString.Contains(prefix) == true)
			{
				int startIndex = idString.IndexOf(prefix) + prefix.Length;
				result = Convert.ToInt32(idString.Substring(startIndex));
			}
			return result;
		}

		public string MasterAccessionNo
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(MasterAccessionNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? MasterAccessionNoNumber
		{
			get
			{
				int? result = null;
				string masterAccessionNo = this.MasterAccessionNo;
				if (string.IsNullOrEmpty(masterAccessionNo) == false) result = Convert.ToInt32(masterAccessionNo.Substring(3));
				return result;
			}
		}

		public int? MasterAccessionNoYear
		{
			get
			{
				int? result = null;
				string masterAccessionNo = this.MasterAccessionNo;
				if (string.IsNullOrEmpty(masterAccessionNo) == false) result = Convert.ToInt32(masterAccessionNo.Substring(0, 2));
				return result;
			}
		}

		public bool IsLegacyMasterAccessionNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyMasterAccessionNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public bool IsValidMasterAccessionNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(MasterAccessionNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public bool IsValidAliquotOrderId
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AliquotOrderAlqIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public bool IsLegacyReportNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public bool IsLegacyPSAReportNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyPSAReportNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public string CreateCyotlogyReportNoFromMasterAccessionNo()
		{
			return this.MasterAccessionNo + ".P";
		}

		public string CreateSurgicalReportNoFromMasterAccessionNo()
		{
			return this.MasterAccessionNo + ".S";
		}

		public bool IsValidReportNo
		{
			get
			{
				bool result = false;
				if (string.IsNullOrEmpty(this.ReportNo) == false)
				{
					if (string.IsNullOrEmpty(this.ReportNoLetter) == false)
					{
						result = true;
					}
				}
				return result;
			}
		}

		public bool IsValidCytologyReportNo
		{
			get
			{
				bool result = false;
				if (this.ReportNo != null)
				{
					if (this.ReportNoLetter == "P") result = true;
				}
				return result;
			}
		}

		public bool IsValidSurgicalReportNo
		{
			get
			{
				bool result = false;
				if (this.ReportNo != null)
				{
					if (this.ReportNoLetter == "S") result = true;
				}
				return result;
			}
		}

		public string LegacyReportNo
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public string ReportNo
		{
			get
			{
				string result = null;
				if (this.IsLegacyReportNo == true) result = LegacyReportNo;
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (match.Captures.Count != 0) result = match.Value;
				}
				return result;
			}
		}

		public string ReportNoLetter
		{
			get
			{
				string result = null;
				if (this.IsLegacyReportNo == false)
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoLetterPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (match.Captures.Count != 0) result = match.Groups["ReportNoPrefix"].Captures[0].Value;
				}
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoLetterPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (match.Captures.Count != 0) result = match.Groups["ReportNoLetter"].Captures[0].Value;
				}
				return result;
			}
		}

		public int? ReportNoNumber
		{
			get
			{
				int? result = null;
				if (this.IsLegacyReportNo == true)
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoNumberPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoNumber"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoNumber"].Captures[0].Value);
				}
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoNumberPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoNumber"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoNumber"].Captures[0].Value);
				}
				return result;
			}
		}

		public int? ReportNoYear
		{
			get
			{
				int? result = null;
				if (this.IsLegacyReportNo == true)
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoYearPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoYear"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoYear"].Captures[0].Value);
				}
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoYearPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoYear"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoYear"].Captures[0].Value);
				}
				return result;
			}
		}

		public string SurgicalReportNoFromMasterAccessionNo
		{
			get
			{
				string result = null;
				if (this.MasterAccessionNo != null)
				{
					result = this.MasterAccessionNo + ".S";
				}
				return result;
			}
		}

		public static string SurgicalReportNoFromNumber(string number)
		{
			string result = null;
			int num;
			if (Int32.TryParse(number, out num) == true)
			{
				if (DateTime.Today.Year > 2013) result = DateTime.Today.Year.ToString().Substring(2) + "-" + number + ".S";
				else result = "S" + DateTime.Today.Year.ToString().Substring(2) + "-" + number;
			}
			return result;
		}

		public static string IncrementReportNo(string reportNo, int increment)
		{
			string result = null;
			int number;
			OrderIdParser orderIdParser = new OrderIdParser(reportNo);
			if (orderIdParser.ReportNo != null)
			{
				if (orderIdParser.IsLegacyReportNo == true)
				{
					number = orderIdParser.ReportNoNumber.Value + increment;
					result = orderIdParser.ReportNoLetter + orderIdParser.ReportNoYear.Value.ToString() + "-" + number.ToString();
				}
				else
				{
					number = orderIdParser.MasterAccessionNoNumber.Value + increment;
					result = orderIdParser.ReportNoYear.Value.ToString() + "-" + number.ToString() + "." + orderIdParser.ReportNoLetter;
					int? reportNumber = orderIdParser.ReportNoNumber;
					if (reportNumber != null) result += reportNumber.Value.ToString();
				}
			}
			return result;
		}

		public string LegacyReportNoFromLegacyPSAReportNo
		{
			get
			{
				string result = null;
				if (this.IsLegacyPSAReportNo == true)
				{
					result = this.m_IdToParse.Substring(0, 3) + "-" + this.m_IdToParse.Substring(3);
				}
				return result;
			}
		}		
	}
}
