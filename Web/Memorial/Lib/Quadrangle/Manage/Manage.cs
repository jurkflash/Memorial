using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Lib.Quadrangle
{
    public class Manage : Transaction, IManage
    {
        private readonly IUnitOfWork _unitOfWork;

        public Manage(IUnitOfWork unitOfWork, IItem item, IQuadrangle quadrangle, IApplicant applicant, IDeceased deceased) : base(unitOfWork, item, quadrangle, applicant, deceased)
        {
            _unitOfWork = unitOfWork;
            _quadrangle = quadrangle;
        }

        public void SetManage(int id)
        {
            _item.SetItem(id);
        }

        public float GetPrice()
        {
            return _item.GetPrice();
        }

        public bool Create()
        {
            CreateNewTransaction();

            _quadrangle.SetApplicant(_transaction.ApplicantId);

            _unitOfWork.Complete();

            return true;
        }

        public float GetAmount(DateTime from, DateTime to)
        {
            if (from > to)
                return -1;

            var diff = (((to.Year - from.Year) * 12) + to.Month - from.Month) * GetPrice();

            return diff;
        }
    }
}