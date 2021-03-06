﻿using SRS.Common;
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
        public Priority RequestPriority { get; set; }
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
        public int[] OtherApplications { get; set; }
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

        #region Cilent Contact
        [DisplayName("Technical Contact:")]
        public string TechContact { get; set; }
        [DisplayName("Tech Contact Phone:")]
        public string TechContactPhone { get; set; }
        [DisplayName("Administrative Contact:")]
        public string AdminContact { get; set; }
        [DisplayName("Admin Contact Phone:")]
        public string AdminContactPhone { get; set; }
        #endregion

        #region Request Approver
        [DisplayName("Local Approval:")]
        public string LocalApproval{ get; set; }
        [DisplayName("Local Approval Date:")]
        public DateTime? LocalApprovalDate { get; set; }
        [DisplayName("Customer Submission Approval (initial):")]
        public string CustomerApprovalInit { get; set; }
        [DisplayName("Customer (initial) Approval Date:")]
        public DateTime? CustomerApprovalInitDate { get; set; }
        [DisplayName("Customer Submission Approval (final):")]
        public string CustomerApprovalFinal { get; set; }
        [DisplayName("Customer (final) Approval Date:")]
        public DateTime? CustomerApprovalFinalDate { get; set; }
        [DisplayName("Client Authorized Approver of DXC Estimate:")]
        public UserInfo ClientAuthorizedApprover { get; set; }
        public string ClientAuthorizedApproverEmail { get; set; }
        #endregion

        [DisplayName("Portfolio Manager:")]
        public string PortfolioManager { get; set; }
        [DisplayName("Alternate Portfolio Manager:")]
        public UserInfo PortfolioManagerAlt { get; set; }
        public string PortfolioManagerAltEmail { get; set; }

        #region Assignment

        [DisplayName("GHPAT:")]
        public string GHPAT { get; set; }
        [DisplayName("Captain:")]
        public string Captain { get; set; }
        [DisplayName("Assignee:")]
        public UserInfo Assignee { get; set; }
        [DisplayName("Assignee E-mail Address:")]
        public string AssigneeEmail { get; set; }
        [DisplayName("Assignee Phone:")]
        public string AssigneePhone { get { return Assignee != null ? Assignee.Phone : string.Empty; } }
        [DisplayName("Assignee Date:")]
        public DateTime? AssigneeDate { get; set; }

        [DisplayName("Backup Dispatcher:")]
        public UserInfo BackupDispatcher { get; set; }
        [DisplayName("Backup Dispatcher E-mail Address:")]
        public string BackupDispatcherEmail { get; set; }
        [DisplayName("Backup Dispatcher Phone:")]
        public string BackupDispatcherPhone { get { return BackupDispatcher != null ? BackupDispatcher.Phone : string.Empty; } }
        [DisplayName("Backup Dispatcher Date:")]
        public DateTime? BackupDispatcherDate { get; set; }

        #endregion

        #region Delegation
        [DisplayName("Portfolio Manager Delegate:")]
        public string PortfolioManagerDelegate { get; set; }
        [DisplayName("Assignee Delegate:")]
        public string AssigneeDelegate { get; set; }
        [DisplayName("Client Requestor Delegate:")]
        public string ClientRequestorDelegate { get; set; }
        [DisplayName("Client Approver Delegate:")]
        public string ClientApproverDelegate { get; set; }
        #endregion

        #region Client Approved Estimate Data

        [DisplayName("Resource Allocation:")]
        public string[] ResourceAllocation { get; set; }
        [DisplayName("Resource Hours and Costs per Month:")]
        public string[] ResourceHoursCostsPerMonth { get; set; }
        [DisplayName("WO Hours Summary:")]
        public string[] WOHoursSummary { get; set; }
        [DisplayName("WO Costs Summary:")]
        public string[] WOCostsSummary { get; set; }
        [DisplayName("WO Milestones Summary:")]
        public string[] WOMilestonesSummary { get; set; }
        [DisplayName("Fixed Price Billing Schedule:")]
        public string[] FixedPriceBillingSchedule { get; set; }
        [DisplayName("Budget Change History:")]
        public string[] BudgetChangeHistory { get; set; }
        [DisplayName("Schedule Change History:")]
        public string[] ScheduleChangeHistory { get; set; }
        [DisplayName("Number of Hours:")]
        public int? NumberOfHours { get; set; }
        [DisplayName("Function Point Estimate:")]
        public int? FunctionPointEstimate { get; set; }
        [DisplayName("Finance Extract Billing Schedule:")]
        public string[] FinanceExtractBillingSchedule { get; set; }
        [DisplayName("Total Authorized Hours:")]
        public string[] TotalAuthorizedHours { get; set; }
        [DisplayName("ZET Version:")]
        public string ZETVersion { get; set; }
        [DisplayName("Local Currency:")]
        public string LocalCurrency { get; set; }

        #endregion

        #region Direct Cost Margin

        [DisplayName("Initial DCM %:")]
        public string InitialDCMPerCentage { get; set; }
        [DisplayName("Initial DCM $:")]
        public string InitialDCMCost { get; set; }
        [DisplayName("Initial DCM Local Currency:")]
        public string InitialDCMLocalCurrency { get; set; }
        [DisplayName("Initial DCM Timestamp:")]
        public string InitialDCMTimestamp { get; set; }
        [DisplayName("Last Approved DCM %:")]
        public string LastApprovedDCMPerCentage { get; set; }
        [DisplayName("Last Approved DCM $:")]
        public string LastApprovedDCMCost { get; set; }
        [DisplayName("Last Approved DCM Local Currency:")]
        public string LastApprovedDCMLocalCurrency { get; set; }
        [DisplayName("Last Approved DCM Timestamp:")]
        public string LastApprovedDCMTimestamp { get; set; }
        [DisplayName("Last Approved DCM History:")]
        public string[] LastApprovedDCMHistory { get; set; }
        [DisplayName("Actual DCM %:")]
        public string ActualDCMPerCentage { get; set; }
        [DisplayName("Actual DCM $:")]
        public string ActualDCMCost { get; set; }
        [DisplayName("Actual DCM Local Currency:")]
        public string ActualDCMLocalCurrency { get; set; }
        [DisplayName("Actual DCM Timestamp:")]
        public string ActualDCMTimestamp { get; set; }

        #endregion

        #region Other

        [DisplayName("Out of Scope:")]
        public bool OutOfScope { get; set; }
        [DisplayName("Out of Plan:")]
        public bool OutOfPlan { get; set; }
        [DisplayName("Portfolio Manager Approved Date:")]
        public DateTime? PorManagerApprovedDate { get; set; }
        [DisplayName("Estimate Completed Date:")]
        public DateTime? EstimateCompletedDate { get; set; }
        [DisplayName("DXC Approved Date:")]
        public DateTime? DXCApprovedDate { get; set; }
        [DisplayName("DXC Rejected Date:")]
        public DateTime? DXCRejectedDate { get; set; }
        [DisplayName("Client Approved Date:")]
        public DateTime? ClientApprovedDate { get; set; }
        [DisplayName("Client Rejected Date:")]
        public DateTime? ClientRejectedDate { get; set; }
        [DisplayName("Closed Date:")]
        public DateTime? ClosedDate { get; set; }
        [DisplayName("Cancelled Date:")]
        public DateTime? CancelledDate { get; set; }
        [DisplayName("On Hold Date:")]
        public DateTime? OnHoldDate { get; set; }
        [DisplayName("Change Control in Process Date:")]
        public DateTime? ChangeControlProcessDate { get; set; }
        [DisplayName("DXC Group MailBox 1:")]
        public string DXCGroupMailBox1 { get; set; }
        [DisplayName("DXC Group Mail Box 2:")]
        public string DXCGroupMailBox2 { get; set; }
        [DisplayName("Client Group Mail Box:")]
        public string ClientGroupMailBox { get; set; }
        [DisplayName("Unlock Reason:")]
        public string UnlockReason { get; set; }
        [DisplayName("Unlock Justification:")]
        public string UnlockJustification { get; set; }
        [DisplayName("Hidden Current Unlock Step:")]
        public string HiddenCurrentUnlockStep { get; set; }
        [DisplayName("Hidden Save Parent SR:")]
        public string HiddenSaveParentSR { get; set; }
        [DisplayName("Hidden Save Project:")]
        public string HiddenSaveProject { get; set; }
        [DisplayName("Hidden Save Project Type:")]
        public string HiddenSaveProjectType { get; set; }

        #endregion

        [DisplayName("Current Work Status:")]
        public string CurrentWorkStatus { get; set; }
        [DisplayName("Current Work Status Date:")]
        public DateTime? CurrentWorkStatusDate { get; set; }
        [DisplayName("Work Status Source:")]
        public string WorkStatusSource { get; set; }
        [DisplayName("Work Status History:")]
        public string[] WorkStatusHistory { get; set; }

        [DisplayName("Note:")]
        public string RequestNote { get; set; }
        [DisplayName("Existing Notes:")]
        public IList<Note> ExistingNotes { get; set; }

        public IList<RequestHistory> RequestStatus { get; set; }

        #endregion
    }
}
