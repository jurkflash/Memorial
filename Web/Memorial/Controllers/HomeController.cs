using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Persistence;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Memorial.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var maritalTypes = _unitOfWork.MaritalTypes.GetAll();

            return View(maritalTypes);
        }

        
    }
}