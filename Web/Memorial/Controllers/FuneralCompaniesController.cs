using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Controllers
{
    public class FuneralCompaniesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FuneralCompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<FuneralCompany>, IEnumerable<FuneralCompanyDto>>(_unitOfWork.FuneralCompanies.GetAllActive()));
        }

        public ActionResult New()
        {
            return View("Form");
        }

        public ActionResult Edit(int id)
        {
            return View("Form", Mapper.Map<FuneralCompany, FuneralCompanyDto>(_unitOfWork.FuneralCompanies.GetActive(id)));
        }

        public ActionResult Save(FuneralCompanyDto funeralCompanyDto)
        {
            if (funeralCompanyDto.Id==0)
            {
                var funeralCompany = Mapper.Map<FuneralCompanyDto, FuneralCompany>(funeralCompanyDto);
                funeralCompany.CreateDate = System.DateTime.Now;
                _unitOfWork.FuneralCompanies.Add(funeralCompany);
            }
            else
            {
                var funeralCompanyInDb = _unitOfWork.FuneralCompanies.GetActive(funeralCompanyDto.Id);
                AutoMapper.Mapper.Map(funeralCompanyDto, funeralCompanyInDb);
                funeralCompanyInDb.ModifyDate = System.DateTime.Now;
            }
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}