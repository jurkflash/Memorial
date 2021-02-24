using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Lib.Quadrangle
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;

        public Order(IUnitOfWork unitOfWork, IItem item, IQuadrangle quadrangle, IApplicant applicant, IDeceased deceased) : base(unitOfWork, item, quadrangle, applicant, deceased)
        {
            _unitOfWork = unitOfWork;
            _quadrangle = quadrangle;
            _deceased = deceased;
        }

        public void SetOrder(int id)
        {
            _item.SetItem(id);
        }

        public float GetPrice()
        {
            return _item.GetPrice();
        }

        private void SetDeceased(int id)
        {
            _deceased.SetById(id);
        }

        public bool Create()
        {
            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);

            if (_transaction.DeceasedId != null)
            {
                SetDeceased((int)_transaction.DeceasedId);
                if (_deceased.GetQuadrangle() != null)
                    return false;
            }

            CreateNewTransaction();

            _quadrangle.SetApplicant(_transaction.ApplicantId);

            if (_transaction.DeceasedId != null)
            {
                _deceased.SetById((int)_transaction.DeceasedId);
                if (_deceased.SetQuadrangle(_transaction.QuadrangleId))
                {
                    _quadrangle.SetHasDeceased(true);
                }
                else
                    return false;
            }

            _unitOfWork.Complete();

            return true;
        }

    }
}