using SRS.Common;
using SRS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.ViewModels
{
    public class RequestViewModel
    {
        [DisplayName("WO Number:")]
        public int Id { get; set; }
        [DisplayName("WO Title:")]
        public string WOTitle { get; set; }
        [DisplayName("WO Status:")]
        public string WOStatus { get; set; }
        [DisplayName("Request Priority:")]
        public string RequestPriority { get; set; }
        [DisplayName("Created By:")]
        public string CreatedBy { get; set; }

        #region WO Identification

        [DisplayName("Version Number:")]
        public int? VersionNumber { get; set; }
        [DisplayName("Workflow:")]
        public string Workflow { get; set; }
        [DisplayName("Client Work Request ID:")]
        public string ClientWorkRequestId 
        {
            get { return ClientWorkRequestIdPrefix + ClientWorkRequestIdNumber.ToString(); }
        }
        [DisplayName("Client Work Request ID Prefix:")]
        public string ClientWorkRequestIdPrefix { get; set; }
        [DisplayName("Client Work Request ID Number:")]
        public int ClientWorkRequestIdNumber { get; set; }
        [DisplayName("Zurich Sub Project Code:")]
        public string ZurichSubProjectCode { get; set; }
        [DisplayName("Business Unit ID:")]
        public string BusinessUnitId { get; set; }
        [DisplayName("Related Work Order Request:")]
        public string RelatedWorkOrderRequest { get; set; }
        [DisplayName("GAM SR Number:")]
        public string GAMSRNumber { get; set; }
        [DisplayName("GAM SR Number Link:")]
        public string GAMSRNumberLink { get; set; }
        [DisplayName("Client SAP ID:")]
        public int? ClientSAPId { get; set; }
        [DisplayName("Client Program ID:")]
        public int? ClientProgramId { get; set; }
        [DisplayName("Client Project ID:")]
        public int? ClientProjectId { get; set; }
        [DisplayName("Client Strategic Program:")]
        public bool ClientStrategicProgram { get; set; }
        [DisplayName("Client Strategic Program ID:")]
        public int? ClientStrategicProgramId { get; set; }
        [DisplayName("Client Strategic Project ID:")]
        public int? ClientStrategicProjectId { get; set; }
        [DisplayName("As Of Date:")]
        public DateTime? AsOfDate { get; set; }
        [DisplayName("Date Time Request Entered:")]
        public DateTime? DateTimeRequestEntered { get; set; }
        [DisplayName("Date Request Entered:")]
        public string DateRequestEntered
        {
            get 
            {
                if (DateTimeRequestEntered.HasValue)
                    return DateTimeRequestEntered.Value.ToString("MMMM dd, yyyy");
                else
                    return string.Empty;
            }
        }
        [DisplayName("Time Request Entered:")]
        public string TimeRequestEntered
        {
            get 
            {
                if (DateTimeRequestEntered.HasValue)
                    return DateTimeRequestEntered.Value.ToString("hh:mm:ss tt");
                else
                    return string.Empty;
            }
        }
        [DisplayName("Client Submit Date:")]
        public DateTime? ClientSubmitDate { get; set; }
        public string ClientSubmitDateDisplay 
        {
            get 
            {
                return ClientSubmitDate.HasValue ? ClientSubmitDate.Value.ToString("MMMM dd, yyyy hh:mm:ss tt") + " EST" : string.Empty;
            }
        }
        [DisplayName("Work Status:")]
        public string WorkStatus { get; set; }
        [DisplayName("DXC Transfer Date:")]
        public DateTime? DXCTransferDate { get; set; }
        [DisplayName("Client Transfer Date:")]
        public DateTime? ClientTransferDate { get; set; }
        [DisplayName("Owning Business Unit:")]
        public string OwningBusinessUnit { get; set; }
        [DisplayName("Owning Business Unit Code:")]
        public string OwningBusinessUnitCode { get; set; }
        [DisplayName("Sub Business Unit:")]
        public string SubBusinessUnit { get; set; }
        [DisplayName("Sub Business Unit Code:")]
        public string SubBusinessUnitCode { get; set; }
        [DisplayName("Sub Segment:")]
        public string SubSegment { get; set; }
        [DisplayName("Sub Segment Code:")]
        public string SubSegmentCode { get; set; }
        [DisplayName("LSA:")]
        public string LSA { get; set; }
        [DisplayName("LSA Code:")]
        public string LSACode { get; set; }
        [DisplayName("Billed Entity:")]
        public string BilledEntity { get; set; }
        [DisplayName("Region:")]
        public string Region { get; set; }
        [DisplayName("Region Code:")]
        public string RegionCode { get; set; }
        [DisplayName("Segment:")]
        public string Segment { get; set; }
        [DisplayName("Segment Code:")]
        public string SegmentCode { get; set; }

        #endregion

        #region Details

        #region Requestor Info
        public UserInfo Requestor;
        #endregion

        #region Work Definition

        [DisplayName("Requested Type of Work:")]
        public string RequestedTypeOfWork { get; set; }
        [DisplayName("Requested Work Order Type:")]
        public string RequestedWorkOrderType { get; set; }
        [DisplayName("Requested Billing Type:")]
        public string RequestedBillingType { get; set; }
        [DisplayName("Actual Type of Work:")]
        public string ActualTypeOfWork { get; set; }
        [DisplayName("Actual Type of Charge:")]
        public string ActualTypeOfCharge { get; set; }
        [DisplayName("Actual Work Order Type:")]
        public string ActualWorkOrderType { get; set; }
        [DisplayName("Release Reference:")]
        public string ReleaseReference { get; set; }
        [DisplayName("Zurich Base Support Offer:")]
        public string ZurichBaseSupportOffer { get; set; }
        [DisplayName("Requested Reduced Tax:")]
        public string RequestedReducedTax { get; set; }
        [DisplayName("Actual Reduced Tax:")]
        public string ActualReducedTax { get; set; }
        [DisplayName("Agreed Estimate Delivery Date:")]
        public DateTime? AgreedEstimateDeliveryDate { get; set; }
        [DisplayName("Release of Imp. Date:")]
        public DateTime? ReleaseOrImplementDate { get; set; }
        [DisplayName("Preferred Start Date:")]
        public DateTime? PreferredStartDate { get; set; }
        [DisplayName("UAT Delivery Date:")]
        public DateTime? UATDeliveryDate { get; set; }
        [DisplayName("AEDD Source:")]
        public string AEDDSource { get; set; }
        [DisplayName("Priority Sequence:")]
        public int PrioritySequence { get; set; }
        [DisplayName("CAPPUD:")]
        public string CAPPUD { get; set; }
        [DisplayName("Request Description:")]
        public string RequestDescription { get; set; }
        [DisplayName("Request Justification:")]
        public string RequestJustification { get; set; }
        [DisplayName("Application Group:")]
        public string ApplicationGroup { get; set; }
        [DisplayName("Application Catalog Impact:")]
        public string ApplicationCatalogImpact { get; set; }
        [DisplayName("Primary Application:")]
        public string PrimaryApplication { get; set; }
        [DisplayName("Application Count:")]
        public int? ApplicationCount { get; set; }
        [DisplayName("Other Application(s):")]
        public string OtherApplications { get; set; }
        [DisplayName("Portfolio:")]
        public string Portfolio { get; set; }
        [DisplayName("Parent SR:")]
        public string ParentSR { get; set; }
        [DisplayName("Project:")]
        public string Project { get; set; }
        [DisplayName("Funding Project:")]
        public string FundingProject { get; set; }
        [DisplayName("Project Type:")]
        public string ProjectType { get; set; }
        [DisplayName("Translation Services:")]
        public string TranslationServices { get; set; }
        [DisplayName("Impeding Work Order:")]
        public string ImpedingWorkOrder { get; set; }
        [DisplayName("Impeding Work Order Reason:")]
        public string ImpedingWorkOrderReason { get; set; }
        [DisplayName("Offshore %:")]
        public float? OffshorePercentage { get; set; }
        [DisplayName("PI Special Exclusion:")]
        public string PISpecialExclusion { get; set; }
        [DisplayName("PSD Attachment:")]
        public string PSDAttachment { get; set; }
        [DisplayName("Lock Down Date in GAM:")]
        public DateTime? LockDownDateInGAM { get; set; }
        [DisplayName("Actual Hours in GAM:")]
        public int ActualHoursInGAM { get; set; }
        [DisplayName("Size Threshold:")]
        public string SizeThreshold { get; set; }
        [DisplayName("Note:")]
        public string WorkDefinitionNote { get; set; }
        [DisplayName("ZET Note:")]
        public string ZETNote { get; set; }
        [DisplayName("ZET Data Extract Table:")]
        public string ZETDataExtractTable { get; set; }

        #endregion

        #endregion
    }
}
