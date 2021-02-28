using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;

namespace Memorial.Lib.Quadrangle
{
    public class Order : Transaction, IOrder
    {
        private readonly IUnitOfWork _unitOfWork;

        public Order(
            IUnitOfWork unitOfWork,
            IItem item,
            IQuadrangle quadrangle,
            IApplicant applicant,
            IDeceased deceased,
            INumber number,
            Invoice.IQuadrangle quadrangleInvoice,
            Receipt.IQuadrangle quadrangleReceipt
            ) : 
            base(
                unitOfWork, 
                item, 
                quadrangle, 
                applicant, 
                deceased, 
                number, 
                quadrangleInvoice, 
                quadrangleReceipt
                )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _number = number;
            _quadrangleInvoice = quadrangleInvoice;
            _quadrangleReceipt = quadrangleReceipt;
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
            _deceased.SetDeceased(id);
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
                SetDeceased((int)_transaction.DeceasedId);
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

        public bool Update()
        {
            return true;
        }

        override
        public bool Delete()
        {
            _quadrangle.SetQuadrangle(_transaction.QuadrangleId);
            if (_quadrangle.HasDeceased())
                return false;

            _quadrangleInvoice.DeleteByApplication(GetAF());
            _transaction.DeleteDate = System.DateTime.Now;

            SetDeceased((int)_transaction.DeceasedId);
            _deceased.RemoveQuadrangle();

            _quadrangle.SetHasDeceased(false);
            _quadrangle.RemoveApplicant();
            _unitOfWork.Complete();

            return true;
        }


    }
}