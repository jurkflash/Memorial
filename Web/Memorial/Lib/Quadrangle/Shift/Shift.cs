using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Lib.Quadrangle
{
    public class Shift : Transaction, IShift
    {
        private readonly IUnitOfWork _unitOfWork;

        public Shift(IUnitOfWork unitOfWork, IItem item, IQuadrangle quadrangle, IApplicant applicant, IDeceased deceased) : base(unitOfWork, item, quadrangle, applicant, deceased)
        {
            _unitOfWork = unitOfWork;
            _quadrangle = quadrangle;
        }

        public void SetShift(int id)
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

    }
}