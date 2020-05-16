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

namespace SRS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            List<WorkflowViewModel> workflows = new List<WorkflowViewModel>()
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
            workflows = workflows.OrderBy(w => w.Code).ToList();
            ViewData["Workflows"] = new SelectList(workflows, "Code", "Code");

            List<ClientWorkRequestPrefix> cwRequestPrefixes = new List<ClientWorkRequestPrefix>()
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
            ViewData["CWQRequestPrefixes"] = new SelectList(cwRequestPrefixes, "IdPrefix", "IdPrefix");

            List<KeyValuePair<string, string>> lSAs = new List<KeyValuePair<string, string>>()
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
            ViewData["LSAs"] = new SelectList(lSAs, "Key", "Value");

            List<KeyValuePair<string, string>> segments = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("General Insurance","General Insurance"),
                new KeyValuePair<string, string>("Global Life","Global Life"),
                new KeyValuePair<string, string>("Farmers","Farmers"),
                new KeyValuePair<string, string>("Group Operations","Group Operations")
            };
            ViewData["Segments"] = new SelectList(segments, "Key", "Value");

            RequestViewModel request = new RequestViewModel
            {
                Id = 53278,
                WOTitle = "Test the ZET 3.43 Version",
                RequestPriority = Priority.Medium.GetDescription(),
                WOStatus = "Calculate AEDO",
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
                SegmentCode = "V1"
            };
            return View(request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
