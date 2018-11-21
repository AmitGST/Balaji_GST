using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.B2B.GST.GSTInvoices
{
    public class SAC
    {
        #region datamembers
        string SACnumber;
        string description;
        string unitOfMeasurement;
        // change this into advance taxes 
        decimal cess;
        decimal rateCGST;
        decimal rateSGST;
        decimal rateIGST;
        bool isNotified;
        bool isCondition;

        List<Notified> Notification;
      
        
        #endregion

        #region PublicHandlers

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string SACNumber
        {
            get { return SACnumber; }
            set { SACnumber = value; }
        }

        public string UnitOfMeasurement
        {
            get { return unitOfMeasurement; }
            set { unitOfMeasurement = value; }
        }

        public decimal RateCGST
        {
            get { return rateCGST; }
            set { rateCGST = value; }
        }

        public decimal RateSGST
        {
            get { return rateSGST; }
            set { rateSGST = value; }
        }

        public decimal RateIGST
        {
            get { return rateIGST; }
            set { rateIGST = value; }
        }

        public decimal Cess
        {
            get { return cess; }
            set { cess = value; }
        }

        public bool IsNotified
        {
            get { return isNotified; }
            set { isNotified = value; }
        }

        public bool IsCondition
        {
            get { return isCondition; }
            set { isCondition = value; }
        }

        public List<Notified> Notification1
        {
            get { return Notification; }
            set { Notification = value; }
        }

       

        #endregion
    }
}