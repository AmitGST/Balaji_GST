using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.B2B.GST.GSTIN
{

    /// <summary>
    /// Transhipment is the major class that houses OwnershipType (enum),IntermediaryWareHouse(class) , rest all r simple attributes
    /// </summary>
    public class TransShipment
    {
        #region ConstructorArea
        public TransShipment()
        {
            vehicleRegistraitonNumber = "";
            typeOfOwnerShip = OwnershipType.NoData;
            ownerName = "";
            driverLicenseNumber = "";
            typeOfGoods = null;
            isIntermediarywareHouseFacilityAvailed = false;
            wareHouseDetails = null;
            fromLocation = "";
            toLocation = "";
            date = new DateTime();
            kilometers = "";
            duration = "";
        }
        #endregion

        #region dataMembers
        string vehicleRegistraitonNumber;
        OwnershipType typeOfOwnerShip;
        string ownerName;
        string driverLicenseNumber;
        List<string> typeOfGoods;
        bool isIntermediarywareHouseFacilityAvailed;
        IntermediaryWareHouse wareHouseDetails;
        string fromLocation;
        string toLocation;
        DateTime date;
        string kilometers;
        string duration;
        #endregion

        #region AccessorMethod
        public string VehicleRegistraitonNumber
        {
            get { return vehicleRegistraitonNumber; }
            set { vehicleRegistraitonNumber = value; }
        }
        public OwnershipType TypeOfOwnerShip
        {
            get { return typeOfOwnerShip; }
            set { typeOfOwnerShip = value; }
        }
        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }
        public List<string> TypeOfGoods
        {
            get { return typeOfGoods; }
            set { typeOfGoods = value; }
        }
        public bool IsIntermediarywareHouseFacilityAvailed
        {
            get { return isIntermediarywareHouseFacilityAvailed; }
            set { isIntermediarywareHouseFacilityAvailed = value; }
        }

        public IntermediaryWareHouse WareHouseDetails
        {
            get { return wareHouseDetails; }
            set { wareHouseDetails = value; }
        }
        public string FromLocation
        {
            get { return fromLocation; }
            set { fromLocation = value; }
        }
        public string ToLocation
        {
            get { return toLocation; }
            set { toLocation = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Kilometers
        {
            get { return kilometers; }
            set { kilometers = value; }
        }
        public string Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        #endregion


    }

    /// <summary>
    /// enum type which is OwnershipType, it tells whether the transporter 
    /// who is providing the commercial vehicle for transportation , the vehicle used is owned or hired
    /// </summary>
    public enum OwnershipType
    {
        Owned = 1,
        Hired,
        NoData
    }

    /// <summary>
    /// This class houses another class called address , which shows from point of consingnor to consignee 
    /// whether any warehouse facility is availed or not
    /// </summary>
    public class IntermediaryWareHouse
    {
        Address address;
    }

    /// <summary>
    /// this class is used in the IntermediaryWareHouse
    /// </summary>
    public class Address
    {
        int address_id;// (PK)    
        string unit;
        string building;
        string street;
        string city;
        string region;
        string pincode;
        string country;
    }
}

