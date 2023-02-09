using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ODS.Web.Brokers.Loggings;
using ODS.Web.Models.Foundations;
using ODS.Web.Models.Foundations.Exceptions;
using ODS.Web.Services.Orchestrations.Employees;

namespace ODS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IEmployeeOrchestrationServices employeeOrchestrationServices;

        public HomeController(ILoggingBroker loggingBroker,
            IEmployeeOrchestrationServices employeeOrchestrationServices)
        {
            this.loggingBroker = loggingBroker;
            this.employeeOrchestrationServices = employeeOrchestrationServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async ValueTask<IActionResult> Index(IFormFile postedFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int rows =
                        await this.employeeOrchestrationServices.ImportExternalFileToTable(postedFile);

                    TempData["success"] = string.Format("{0} rows added successfuly", rows);

                    return RedirectToAction("Data");
                }

                TempData["error"] = "File is required";
                return View();
            }
            catch (NotSupportedFileException exception)
            {
                TempData["error"] = $"{exception.Message}";
                return View();
            }
            catch(InvalidCastException exception)
            {
                return RedirectToAction("Error");
            }
            catch (FormatException exception)
            {
                return RedirectToAction("Error");
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Data(string orderby = null)
        {
            IQueryable<Employee> ascendingOrderedEmployees =
                this.employeeOrchestrationServices.RetrieveEmployeeAscendingOrder(orderby);

            return View(ascendingOrderedEmployees);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}