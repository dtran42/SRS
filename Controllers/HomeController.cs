using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SRS.ViewModels;
using SRS.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using SRS.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SRS.Data;

namespace SRS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //todo: get data and populate to RequestViewModel
            List<RequestViewModel> requests = new List<RequestViewModel>()
            {
                new RequestViewModel { Id = 53278, WOTitle = "Test the ZET 3.43 Version", RequestPriority = Priority.Medium.GetDescription(), WOStatus = "Calculate AEDO", CreatedBy = "User A" },
                new RequestViewModel { Id = 53277, WOTitle = "Test the ZET 4.0 release", RequestPriority = Priority.Low.GetDescription(), WOStatus = "Client Approval", CreatedBy = "User B" },
                new RequestViewModel { Id = 43206, WOTitle = "Zurich new WO with long phone", RequestPriority = Priority.Hight.GetDescription(), WOStatus = "Calculate AEDO", CreatedBy = "User U" },
                new RequestViewModel { Id = 43080, WOTitle = "Test the ZET 5.0 Version", RequestPriority = Priority.Hight.GetDescription(), WOStatus = "Client Approval", CreatedBy = "User P" },
                new RequestViewModel { Id = 43078, WOTitle = "Test the ZET 5.2 realease", RequestPriority = Priority.Low.GetDescription(), WOStatus = "Client Approval", CreatedBy = "User M" },
                new RequestViewModel { Id = 53232, WOTitle = "Test the ZET 3.43 Version", RequestPriority = Priority.Medium.GetDescription(), WOStatus = "Calculate AEDO", CreatedBy = "User K" },
                new RequestViewModel { Id = 53246, WOTitle = "Test the ZET 4.0 release", RequestPriority = Priority.Low.GetDescription(), WOStatus = "Client Approval", CreatedBy = "User X" },
                new RequestViewModel { Id = 43123, WOTitle = "Zurich new WO with long phone", RequestPriority = Priority.Hight.GetDescription(), WOStatus = "Calculate AEDO", CreatedBy = "User Q" },
                new RequestViewModel { Id = 43090, WOTitle = "Test the ZET 5.0 Version", RequestPriority = Priority.Hight.GetDescription(), WOStatus = "Client Approval", CreatedBy = "User I" },
                new RequestViewModel { Id = 43876, WOTitle = "Test the ZET 5.2 realease", RequestPriority = Priority.Low.GetDescription(), WOStatus = "Client Approval", CreatedBy = "User Z" },
            };
            return View(requests);
        }

        public IActionResult Detail(int? id)
        {
            IList<WorkflowViewModel> workflows = GetWorkflows();
            workflows = workflows.OrderBy(w => w.Code).ToList();
            ViewData["Workflows"] = new SelectList(workflows, "Code", "Code");

            IList<ClientWorkRequestPrefix> cwRequestPrefixes = GetClientWorkRequestPrefixes();
            ViewData["CWQRequestPrefixes"] = new SelectList(cwRequestPrefixes, "IdPrefix", "IdPrefix");

            IList<KeyValuePair<string, string>> lSAs = GetLSAs();
            ViewData["LSAs"] = new SelectList(lSAs, "Key", "Value");

            IList<KeyValuePair<string, string>> segments = GetSegments();
            ViewData["Segments"] = new SelectList(segments, "Key", "Value");

            IList<KeyValuePair<string, string>> helpList = GetHelpList();
            ViewBag.HelpList = helpList;

            List<KeyValuePair<string, string>> locations = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Zurich","Zurich"),
                new KeyValuePair<string, string>("Zurich1","Zurich1"),
                new KeyValuePair<string, string>("Zurich2","Zurich2"),
                new KeyValuePair<string, string>("Zurich3","Zurich3"),
                new KeyValuePair<string, string>("Zurich4","Zurich4"),
                new KeyValuePair<string, string>("Zurich5","Zurich5"),
                new KeyValuePair<string, string>("Zurich6","Zurich6"),
                new KeyValuePair<string, string>("Zurich7","Zurich7"),
                new KeyValuePair<string, string>("Zurich8","Zurich8"),
                new KeyValuePair<string, string>("Zurich9","Zurich9")
            };
            ViewData["LocationList"] = new SelectList(locations, "Key", "Value");

            List<KeyValuePair<string, string>> actualChargeTypes = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Fixed","Fixed"),
                new KeyValuePair<string, string>("Variable","Variable")
            };
            ViewData["ActualChargeTypes"] = new SelectList(actualChargeTypes, "Key", "Value");

            List<KeyValuePair<string, string>> workOrderTypes = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("<=4 Hours","<=4 Hours"),
                new KeyValuePair<string, string>("Bucket","Bucket"),
                new KeyValuePair<string, string>("Collective","Collective"),
                new KeyValuePair<string, string>("Estimating WO","Estimating WO"),
                new KeyValuePair<string, string>("Funding","Funding"),
                new KeyValuePair<string, string>("None","None"),
                new KeyValuePair<string, string>("Overhead","Overhead"),
                new KeyValuePair<string, string>("Planning","Planning"),
                new KeyValuePair<string, string>("Pre-Approved","Pre-Approved"),
                new KeyValuePair<string, string>("WIP (Work in Progress)","WIP (Work in Progress)")
            };
            ViewData["WorkOrderTypes"] = new SelectList(workOrderTypes, "Key", "Value");

            List<KeyValuePair<string, string>> applicationGroups = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("All Applications","All Applications"),
                new KeyValuePair<string, string>("Application Group 1","Application Group 1"),
                new KeyValuePair<string, string>("Application Group 2","Application Group 2"),
                new KeyValuePair<string, string>("Application Group 3","Application Group 3"),
                new KeyValuePair<string, string>("Application Group 4","Application Group 4")
            };
            ViewData["ApplicationGroups"] = new SelectList(applicationGroups, "Key", "Value");

            List<KeyValuePair<string, string>> appCatalogImpacts = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Not Applicable","Not Applicable"),
                new KeyValuePair<string, string>("Add","Add"),
                new KeyValuePair<string, string>("Retire","Retire")
            };
            ViewData["AppCatalogImpacts"] = new SelectList(appCatalogImpacts, "Key", "Value");

            List<KeyValuePair<string, string>> cappudList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Corrective","Corrective"),
                new KeyValuePair<string, string>("Adaptive","Adaptive"),
                new KeyValuePair<string, string>("Preventive","Preventive"),
                new KeyValuePair<string, string>("Perfective","Perfective"),
                new KeyValuePair<string, string>("User Support","User Support"),
                new KeyValuePair<string, string>("Development","Development"),
                new KeyValuePair<string, string>("App Modernization","App Modernization")
            };
            ViewData["CAPPUDList"] = new SelectList(cappudList, "Key", "Value");

            List<KeyValuePair<string, string>> primaryApplications = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("New Application","New Application"),
                new KeyValuePair<string, string>("No Application","No Application"),
                new KeyValuePair<string, string>("50 Screen for inquiry","50 Screen for inquiry"),
                new KeyValuePair<string, string>("A&H Cash Management","A&H Cash Management"),
                new KeyValuePair<string, string>("AA Centrica data transfer","AA Centrica data transfer"),
                new KeyValuePair<string, string>("ACCMAN - Accident Management System","ACCMAN - Accident Management System"),
                new KeyValuePair<string, string>("AGIL Client","AGIL Client"),
                new KeyValuePair<string, string>("AIA","AIA"),
                new KeyValuePair<string, string>("AKIS","AKIS"),
                new KeyValuePair<string, string>("ALEX","ALEX"),
                new KeyValuePair<string, string>("ANGIS","ANGIS"),
                new KeyValuePair<string, string>("Account Documentation","Account Documentation"),
                new KeyValuePair<string, string>("Agency Dashboard","Agency Dashboard"),
                new KeyValuePair<string, string>("Agency Management System","Agency Management System")
            };
            ViewData["PrimaryApplications"] = new SelectList(primaryApplications, "Key", "Value");

            List<KeyValuePair<string, string>> portfolioList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("FNWL","FNWL"),
                new KeyValuePair<string, string>("Singapore Life","Singapore Life"),
                new KeyValuePair<string, string>("Commercial (US)","Commercial (US)"),
                new KeyValuePair<string, string>("Farmers Life","Farmers Life"),
                new KeyValuePair<string, string>("EAS Consumer","EAS Consumer"),
                new KeyValuePair<string, string>("Specialty Life","Specialty Life"),
                new KeyValuePair<string, string>("Underwriting","Underwriting"),
                new KeyValuePair<string, string>("Finance","Finance"),
                new KeyValuePair<string, string>("Claims","Claims"),
                new KeyValuePair<string, string>("UK LIfe","UK LIfe"),
                new KeyValuePair<string, string>("ES CH","ES CH"),
                new KeyValuePair<string, string>("CH GI Shared","CH GI Shared"),
                new KeyValuePair<string, string>("CH Global Life","CH Global Life"),
                new KeyValuePair<string, string>("BI CH","BI CH")
            };
            ViewData["PortfolioList"] = new SelectList(portfolioList, "Key", "Value");

            List<KeyValuePair<string, string>> srList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("51664","51664 - Funding Project for ECM Services"),
                new KeyValuePair<string, string>("48963","48963 - AMER4886-GL_GLIFE-BS-PUZ0033148-Swindon"),
                new KeyValuePair<string, string>("48825","48825 - AMER4485-Technology Solution-BS"),
                new KeyValuePair<string, string>("48823","48823 - AMER4485-Business Solution-BS"),
                new KeyValuePair<string, string>("47385","47385 - FY 11 Italy Funding SR")
            };
            ViewData["SRList"] = new SelectList(srList, "Key", "Value");

            List<KeyValuePair<string, string>> projectList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("S9921-41335","S9921-41335 - Network Get Well Expert"),
                new KeyValuePair<string, string>("S9245-41726","S9245-41726 - UKG Project X"),
                new KeyValuePair<string, string>("S9210-38675","S9210-38675 - Benefits Statements (JD)"),
                new KeyValuePair<string, string>("S9189-35078","S9189-35078 - Regional Day 2"),
                new KeyValuePair<string, string>("S8894-42755","S8894-42755 - Chicago Winter Engineer")
            };
            ViewData["ProjectList"] = new SelectList(projectList, "Key", "Value");

            List<KeyValuePair<string, string>> piSpecExcList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ZNA Enhanced Model (not Dedicated)","ZNA Enhanced Model (not Dedicated)"),
                new KeyValuePair<string, string>("ZNA Center of Competency","ZNA Center of Competency"),
                new KeyValuePair<string, string>("Joint Development - Zurich performing more than 10%","Joint Development - Zurich performing more than 10%"),
                new KeyValuePair<string, string>("Evaluation Effort","Evaluation Effort"),
                new KeyValuePair<string, string>("Process Inhibited Effort","Process Inhibited Effort"),
                new KeyValuePair<string, string>("Mixed Work (Measured and Non-measured)","Mixed Work (Measured and Non-measured)"),
                new KeyValuePair<string, string>("Zurich Managed Effort","Zurich Managed Effort"),
                new KeyValuePair<string, string>("Subsequent Implementations","Subsequent Implementations")
            };
            ViewData["PISpecExcList"] = new SelectList(piSpecExcList, "Key", "Value");


            var accountList = _context.UserInfos.ToList();
            accountList.Insert(0, new UserInfo());
            ViewData["AccountList"] = new SelectList(accountList, "Email", "FullName");
            var requestor = _context.UserInfos.FirstOrDefault(i => i.Email == "requestor@dxc.com");

            RequestViewModel request = new RequestViewModel
            {
                #region WO Identification
                Id = 53278,
                WOTitle = "Test the ZET 3.43 Version",
                RequestPriority = Priority.Medium.GetDescription(),
                WOStatus = "Calculate AEDD",
                CreatedBy = "User A",
                Workflow = "SRMT-WF-R17V1-ZFS-WorkOrder",
                VersionNumber = 1,
                ClientWorkRequestIdPrefix = "CHL_PTY",
                ClientWorkRequestIdNumber = 4352,
                ZurichSubProjectCode = string.Empty,
                BusinessUnitId = string.Empty,
                RelatedWorkOrderRequest = string.Empty,
                GAMSRNumber = string.Empty,
                GAMSRNumberLink = "(No Link)",
                ClientSAPId = 23543637,
                ClientProgramId = null,
                ClientProjectId = null,
                AsOfDate = new DateTime(2020, 3, 3),
                ClientStrategicProgram = false,
                ClientStrategicProgramId = null,
                ClientStrategicProjectId = null,
                DateTimeRequestEntered = new DateTime(2020, 3, 3, 2, 45, 30),
                WorkStatus = "Not Started",
                DXCTransferDate = null,
                ClientTransferDate = null,
                ClientSubmitDate = new DateTime(2020, 3, 3, 4, 41, 42),
                OwningBusinessUnit = "Switzerland (GI)",
                OwningBusinessUnitCode = "V1001",
                SubBusinessUnit = string.Empty,
                SubBusinessUnitCode = string.Empty,
                SubSegment = string.Empty,
                SubSegmentCode = string.Empty,
                LSA = "Switzerland",
                LSACode = "CHL",
                BilledEntity = "Zürich Versicherungs-Gesellschaft AG - Regionalsitz Zürich",
                Region = string.Empty,
                RegionCode = string.Empty,
                Segment = "General Insurance",
                SegmentCode = "V1",
                #endregion

                #region Requestor Info
                Requestor = requestor,
                #endregion

                #region Details

                #region Work Definition

                RequestedTypeOfWork = "Application Project",
                RequestedWorkOrderType = "None",
                RequestedBillingType = "Fixed",
                ActualTypeOfWork = string.Empty,
                ActualTypeOfCharge = "Fixed",
                ActualWorkOrderType = "None",
                ReleaseReference = string.Empty,
                ZurichBaseSupportOffer = "N/A",
                RequestedReducedTax = "No",
                ActualReducedTax = "No",
                AgreedEstimateDeliveryDate = null,
                AEDDSource = string.Empty,
                ReleaseOrImplementDate = null,
                PreferredStartDate = null,
                UATDeliveryDate = null,
                PrioritySequence = 1,
                CAPPUD = string.Empty,
                RequestDescription = "To Test the ZET 3.43 version",
                RequestJustification = "To Test the ZET 3.43 version",
                ApplicationGroup = "All Applications",
                ApplicationCatalogImpact = "Not Applicable",
                PrimaryApplication = "New Application",
                ApplicationCount = null,
                OtherApplications = "(No Entries)",
                Portfolio = "CH GI Shared",
                ParentSR = string.Empty,
                Project = string.Empty,
                FundingProject = string.Empty,
                ProjectType = string.Empty,
                TranslationServices = string.Empty,
                ImpedingWorkOrder = string.Empty,
                ImpedingWorkOrderReason = string.Empty,
                OffshorePercentage = null,
                PISpecialExclusion = string.Empty,
                PSDAttachment = string.Empty,
                ActualHoursInGAM = 0,
                LockDownDateInGAM = null,
                SizeThreshold = "Task",
                WorkDefinitionNote = "",
                ZETDataExtractTable = "(No Entries)",
                ZETNote = string.Empty,

                #endregion

                #region Cilent Contact
                TechContact = string.Empty,
                TechContactPhone = string.Empty,
                AdminContact = string.Empty,
                AdminContactPhone = string.Empty,
                #endregion

                #region Request Approver
                LocalApproval = "User A",
                LocalApprovalDate = new DateTime(2020, 3, 3),
                CustomerApprovalInit = "User B",
                CustomerApprovalInitDate = new DateTime(2020, 3, 3),
                CustomerApprovalFinal = string.Empty,
                CustomerApprovalFinalDate = null,
                ClientAuthorizedApprover = null,
                #endregion

                PortfolioManager = "Hermann Tjabben",
                PortfolioManagerAlt = null,
                GHPAT=string.Empty,
                Captain=string.Empty,
                Assignee=null,
                AssigneeDate=null,
                BackupDispatcher=null,
                BackupDispatcherDate=null,
                PortfolioManagerDelegate=string.Empty,
                AssigneeDelegate = string.Empty,
                ClientRequestorDelegate = string.Empty,
                ClientApproverDelegate = string.Empty

                #endregion
            };
            return View(request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Private Methods
        private IList<WorkflowViewModel> GetWorkflows()
        {
            return new List<WorkflowViewModel>()
            {
                new WorkflowViewModel { Code="CSC-SUN-Service Request", Description="SUN Service Request Workflow", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="CSC-ZFS-Work Order Workflow ", Description="This Workflow is for Zurich Work Orders", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRM-WF-R10V1-Reset-User-Password ", Description="Reset User Password ", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMT-WF-R17V1-ZFS-WorkOrder", Description="Work Order", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMT-WF-R17V1-ZFS-ChangeOrder", Description="Change Order", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMT-WF-R17V1-ZFS-ChargeCode", Description="Charge Code", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMT-WF-R20V1-NMC-ChangeOrder", Description="Change Request", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMT-WF-R20V1-NMC-Estimate", Description="Estimate", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R21V1-NBC-Project Workflow", Description="Project workfow for Nobel Biocare", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R21V2-NBC-Catalog Workflow", Description="Workflow for Catalog Items", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R20V2-UPH-Project Workflow", Description="Project workfow for UPH", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R20V3-AHM-Project Workflow", Description="Project workfow for AHM", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R20V2-AON-Project Workflow", Description="Project workfow for Aon ", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R20V2-RRS-Delegation Workflow", Description="Workflow for Delegation", Scheme="Workflow scheme" },
                new WorkflowViewModel { Code="SRMG-WF-R20V1-TXT-Project Workflow", Description="Project workfow for Textron Company ", Scheme="Workflow scheme" }
            };
        }

        private IList<ClientWorkRequestPrefix> GetClientWorkRequestPrefixes()
        { 
            return new List<ClientWorkRequestPrefix>()
            {
                new ClientWorkRequestPrefix { IdPrefix="AUST_LOC", BusinessArea="Australia_Local" },
                new ClientWorkRequestPrefix { IdPrefix="CHL_ACCMgt", BusinessArea="Enabling Services" },
                new ClientWorkRequestPrefix { IdPrefix="CHL_Cl", BusinessArea="GI Claims" },
                new ClientWorkRequestPrefix { IdPrefix="CHL_PTY", BusinessArea="CH Party System" },
                new ClientWorkRequestPrefix { IdPrefix="CHL_SLife", BusinessArea="Special Life" },
                new ClientWorkRequestPrefix { IdPrefix="CHS_ApInt", BusinessArea="Application Integration" },
                new ClientWorkRequestPrefix { IdPrefix="CHS_CAS", BusinessArea="CAS" },
                new ClientWorkRequestPrefix { IdPrefix="CHS_CC", BusinessArea="Corporate Center" },
                new ClientWorkRequestPrefix { IdPrefix="CHS_FIN", BusinessArea="Finance" },
                new ClientWorkRequestPrefix { IdPrefix="CSC_FL_SPEC_", BusinessArea="Specialty Lines" },
                new ClientWorkRequestPrefix { IdPrefix="DEL_HYPO", BusinessArea="Hypotheken" },
                new ClientWorkRequestPrefix { IdPrefix="DEL_IMMO", BusinessArea="Immobilien" },
                new ClientWorkRequestPrefix { IdPrefix="DEL_INK", BusinessArea="Inkasso" },
                new ClientWorkRequestPrefix { IdPrefix="DEL_KFM", BusinessArea="Kapital- und Finanzmanagement" },
                new ClientWorkRequestPrefix { IdPrefix="DEL_KK", BusinessArea="Kredit und Kaution " },
                new ClientWorkRequestPrefix { IdPrefix="DEL_KOMM", BusinessArea="Kommunikation" },
                new ClientWorkRequestPrefix { IdPrefix="DEL_LHPart", BusinessArea="Partner DH" }
            };
        }

        private IList<KeyValuePair<string, string>> GetLSAs()
        { 
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Austria","Austria"),
                new KeyValuePair<string, string>("Belgium","Belgium"),
                new KeyValuePair<string, string>("Croatia","Croatia"),
                new KeyValuePair<string, string>("Denmark","Denmark"),
                new KeyValuePair<string, string>("Finland","Finland"),
                new KeyValuePair<string, string>("France","France"),
                new KeyValuePair<string, string>("Germany","Germany"),
                new KeyValuePair<string, string>("Greece","Greece"),
                new KeyValuePair<string, string>("Hungary","Hungary"),
                new KeyValuePair<string, string>("Ireland","Ireland"),
                new KeyValuePair<string, string>("Italy","Italy"),
                new KeyValuePair<string, string>("Netherlands","Netherlands"),
                new KeyValuePair<string, string>("Norway","Norway"),
                new KeyValuePair<string, string>("Poland","Poland"),
                new KeyValuePair<string, string>("Portugal","Portugal"),
                new KeyValuePair<string, string>("Spain","Spain"),
                new KeyValuePair<string, string>("Sweden","Sweden"),
                new KeyValuePair<string, string>("Switzerland","Switzerland")
            };
        }

        private IList<KeyValuePair<string, string>> GetSegments()
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("General Insurance","General Insurance"),
                new KeyValuePair<string, string>("Global Life","Global Life"),
                new KeyValuePair<string, string>("Farmers","Farmers"),
                new KeyValuePair<string, string>("Group Operations","Group Operations")
            };
        }

        private IList<KeyValuePair<string, string>> GetHelpList()
        { 
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("WO Number", "ITG generated SR number. Used by ITG to track work orders. Independent and not related to GAM SR number."),
                new KeyValuePair<string, string>("Business Unit ID","Enter a business unit identifier that you want appended to the Client Work Order ID."),
                new KeyValuePair<string, string>("Client SAP ID","Enter the Client SAP Internal Order number or the local host code."),
                new KeyValuePair<string, string>("Client Program ID","Enter the Program ID from the Global Project Tracking Database if this project is part of a larger program."),
                new KeyValuePair<string, string>("Client Project ID","Enter the Project ID from the Global Project Tracking Database if this project is part of a larger program."),
                new KeyValuePair<string, string>("Client Work Request ID Prefix","Click on the small icon to the right of the Client Work Request ID Prefix field to select from a list of Prefixes. Typing the first few letters of the Prefix will bring you closer to the entry you need to select."),
                new KeyValuePair<string, string>("Client Work Request ID Number","Enter a numeric value for the Client Work Request ID Number."),
                new KeyValuePair<string, string>("Client Work Request ID","The work request ID will be formed automatically using a prefix associated with the selected business area, the sequence number specified in the Client Work Request ID Number field and the Business Unit ID."),
                new KeyValuePair<string, string>("Requested Type of Work",@"<p>ECP - Enhancement, Compliance, Projects.</p><p>Base Support - is a non-billable item that will not require an estimate and is to be used by entities that have no other way to request base support.</p><p>Other - everything else that does not meet the criteria for the items above</p>"),
                new KeyValuePair<string, string>("PI Special Exclusion","Selecting a value for this field will indicate the work order should be excluded from the Productivity Index Report."),
                new KeyValuePair<string, string>("Requested Reduced Tax","Requested Reduced Tax (a.k.a. Reduced VAT) is sent by ZEBRA. It is for Germany jurisdiction only and is always be 'No' for all other entities."),
                new KeyValuePair<string, string>("Actual Reduced Tax","DXC can update the value at Pending Portfolio Manager status only. Actual Reduced Tax is updatable via ZET upon Client Approval in the RM."),
                new KeyValuePair<string, string>("CAPPUD",@"<p>Corrective - Activities that are performed to identify and correct code and supporting documentation.</p><p>Adaptive - Work performed to modify applications as required by the operating environment or regulatory changes.</p><p>Preventive - Work performed to increase software systems future maintainability.</p><p>Perfective - Activities that are performed to accommodate new or changed business requirements. (enhancement of existing applications in the portfolio).</p><p>Development - Activities that are performed to construct new software systems and include all of the software development cycle.</p><p>Other - Other activities that do not fall under any of the categories described above.</p>"),
                new KeyValuePair<string, string>("Agreed Estimate Delivery Date","This is the date that Client and CSC have mutually agreed for delivery of the CSC WO estimate and response to Client. If there is no agreed to date, the response time is 4 days for Tasks and 10 days for Projects from the SR submission date in GAM."),
                new KeyValuePair<string, string>("Actual Type of Charge","This is the Type of Charge from the DXC Approved Estimate."),
                new KeyValuePair<string, string>("Actual Work Order Type",@"<p><=4 Hours - Tasks that are less than or equal to 4 hours are pre-approved if CSC agrees the work is <= 4 hours.</p><p>Pre-Approved - Tasks that are between 5 & 20 hours are pre-approved if CSC agrees with the hours that have been estimated by Client. CSC returns the work order file to Client with the hours shown in the Resource Estimation sheet and work can begin without any further client approval. The estimate is not considered to be pre-approved if CSC estimates a higher cost than Client expects. (ASP Bulletin 24)</p><p>Collective - This work order is considered to be a collection of smaller related tasks (ASP Bulletin 25)</p><p>Quick Start - CSC needs to begin work before the PSD has been completed. Client will approve an amount of funding for CSC to use until the PSD is finished and a change order is issued to get the complete estimate for the project. (ASP Bulletin 26)</p>"),
                new KeyValuePair<string, string>("Requested Work Order Type",@"<p><=4 Hours - Tasks that are less than or equal to 4 hours are pre-approved if CSC agrees the work is <= 4 hours.</p><p>Pre-Approved - Tasks that are between 5 & 20 hours are pre-approved if CSC agrees with the hours that have been estimated by Client. CSC returns the work order file to Client with the hours shown in the Resource Estimation sheet and work can begin without any further client approval. The estimate is not considered to be pre-approved if CSC estimates a higher cost than Client expects. (ASP Bulletin 24)</p><p>Collective - This work order is considered to be a collection of smaller related tasks (ASP Bulletin 25)</p><p>Quick Start - CSC needs to begin work before the PSD has been completed. Client will approve an amount of funding for CSC to use until the PSD is finished and a change order is issued to get the complete estimate for the project. (ASP Bulletin 26)</p>"),
                new KeyValuePair<string, string>("Requested Billing Type","Most work orders will be billed at a fixed price, so that is the default value for Billing Type. Select Variable if the work is to be paid by the hour (e.g. time and materials)."),
                new KeyValuePair<string, string>(
                    "Client Authorized Approver of DXC Estimate",
                    "Select the Client Authorized Approver of the estimate from the drop-down list. After all CSC approvals are complete, this field wil be used to send estimate to Client for completion of their approval process."
                    ),
                new KeyValuePair<string, string>(
                    "Assignee",
                    @"<p>For a task, this is the dispatcher responsible for this work order. For a project, this is the project manager responsible for the work order.</p><p>In the case where this work order is transferred to another person, this other person's name is captured in this field.</p>"
                    ),
                new KeyValuePair<string, string>(
                    "Number of Hours",
                    "This field contains the number of hours on the client approved estimate. When an estimate moves to a client approved status, this field is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Resource Allocation",
                    "This table contains the same information as the Summary Resource Allocation table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "WO Milestone Summary",
                    "This table contains the same information as the WO Milestone Summary table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Schedule Change History",
                    "This table contains the same information as the Summary Resource Allocation table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Function Point Estimate",
                    "This field contains the function point estimate on the client approved estimate. When an estimate moves to a client approved status, this field is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Local Currency",
                    "This field contains the local currency on the client approved estimate. When an estimate moves to a client approved status, this field is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Finance Extract Billing Schedule",
                    "This table contains the same information as the Fixed Price Billing Schedule table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically. The table also contains details of the finance extract."
                    ),
                new KeyValuePair<string, string>(
                    "Resource Hours and Costs per Month",
                    "This table contains the same information as the Resource Hours and Costs by Month table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "WO Hours Summary",
                    "This table contains the same information as the WO Hours Summary table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "WO Cost Summary",
                    "This table contains the same information as the WO Cost Summary table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Fixed Price Billing Schedule",
                    "This table contains the same information as the Fixed Price Billing Schedule table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "Budget Change History",
                    "This table contains the same information as the Summary Resource Allocation table on the estimate that is approved by the client. When an estimate moves to a client approved status, this table is updated automatically."
                    ),
                new KeyValuePair<string, string>(
                    "ActualDCMPer",
                    "Enter the Direct Cost Margin percentage."
                    ),
                new KeyValuePair<string, string>(
                    "ActualDCMCost",
                    "Enter the Direct Cost Margin dollar amount."
                    ),
                new KeyValuePair<string, string>(
                    "Actual DCM in Local Currency",
                    "Enter the Direct Cost Margin amount in the local currency."
                    ),
                new KeyValuePair<string, string>(
                    "Actual DCM Timestamp",
                    "System populates with the date the DCM %, DCM $ or DCM Local Currency were entered/updated."
                    )
            };
        }
        #endregion
    }
}
