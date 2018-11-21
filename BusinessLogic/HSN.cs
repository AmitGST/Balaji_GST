using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.B2B.GST.GSTInvoices
{
    public class HSN
    {
        #region datamembers
        int hSNID;

      
        string HSNumber;
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
        
       #endregion endregion

        #region publichandlers
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int HSNID
        {
            get { return hSNID; }
            set { hSNID = value; }
        }

        public string HSNNumber
        {
            get { return HSNumber; }
            set { HSNumber = value; }
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
        public string UnitOfMeasurement
        {
            get { return unitOfMeasurement; }
            set { unitOfMeasurement = value; }
        }
        public decimal Cess
        {
            get { return cess; }
            set { cess = value; }
        }
       public List<Notified> NotificatioN
        {
            get { return Notification; }
            set { Notification = value; }
        }
       public bool IsCondition
         {
             get { return isCondition; }
             set { isCondition = value; }
         }
         public bool IsNotified
         {
             get { return isNotified; }
             set { isNotified = value; }
         }
        
        #endregion

    }

    public class Notified
    {
        #region members
        int notifiedID;

       
        int serialNo;
        string notificationNo;
        string notificationSerialNo;
        string description;
        decimal tax;
        bool isCondition;
        List<Condition> Conditions;

       

        
       
        #endregion

        #region publicHandlers
        public int NotifiedID
        {
            get { return notifiedID; }
            set { notifiedID = value; }
        }
        public int SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string NotificationNo
        {
            get { return notificationNo; }
            set { notificationNo = value; }
        }

        public string NotificationSerialNo
        {
            get { return notificationSerialNo; }
            set { notificationSerialNo = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public decimal Tax
        {
            get { return tax; }
            set { tax = value; }
        }
        public bool IsCondition
        {
            get { return isCondition; }
            set { isCondition = value; }
        }
        public List<Condition> Conditions1
        {
            get { return Conditions; }
            set { Conditions = value; }
        }
        #endregion

        public enum  NotificationCondtion
        {
            Single=1,
            Multiple=2
        }
    }

    public class Condition
    {
        #region members
        int conditionID;

        
        int serialNo;
        string conditionNo;
        string conditionSerialNo;
        string description;
        decimal tax;

        #endregion

        #region publicHandlers
        public int ConditionID
        {
            get { return conditionID; }
            set { conditionID = value; }
        }
        public int SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string ConditionNo
        {
            get { return conditionNo; }
            set { conditionNo = value; }
        }

        public string ConditionSerialNo
        {
            get { return conditionSerialNo; }
            set { conditionSerialNo = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public decimal Tax
        {
            get { return tax; }
            set { tax = value; }
        }
       
        #endregion
    }
}