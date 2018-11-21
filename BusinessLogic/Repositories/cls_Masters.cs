using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class cls_Masters
    {

        UnitOfWork unitOfWork;

        public cls_Masters()
        {
            unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Get All StateList with state code
        /// </summary>
        /// <returns>State Code with State Name</returns>
        public IEnumerable<GST_MST_STATE> GetStateList()
        {
            var state = unitOfWork.StateRepository.Filter(f => f.Status == true);
            return state;
        }

        /// <summary>
        /// Get All HSN/SAC list 
        /// </summary>
        /// <returns>Item Code with Description</returns>
        public IEnumerable<GST_MST_ITEM> GetItemList()
        {
            var list = unitOfWork.ItemRepository.Filter(f => f.Status == true);
            return list;
        }

        /// <summary>
        /// Get All Vendor list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GST_MST_VENDOR> GetVendorList()
        {
            var vendorList = unitOfWork.VendorRepository.Filter(f => f.Status == true);
            return vendorList;
        }

        /// <summary>
        /// Get Purchase Register data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GST_MST_PURCHASE_REGISTER> GetPurchaseRegister()
        {
            var purchaseRegister = unitOfWork.PurchaseRegisterDataRepositry.Filter(f => f.Status == true);
            return purchaseRegister;
        }
    }
}
