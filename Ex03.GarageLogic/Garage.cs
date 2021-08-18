using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, Costumer> m_CurrentCostumers;
        public Garage()
        {
            m_CurrentCostumers = new Dictionary<string, Costumer>();
        }
        // Getters and setters
        public Dictionary<string, Costumer> CurrentCostumers
        {
            get
            {
                return m_CurrentCostumers;
            }
        }
        //Checking if the car is listed in the garage
        public bool IsLicenseNumberInGarage(string i_LicenseNumber)
        {
            return CurrentCostumers.ContainsKey(i_LicenseNumber);
        }
        public void ChangeVehicleStatus(string i_LicenseNumber, eGarageStatus i_NewStatus)
        {
            m_CurrentCostumers[i_LicenseNumber].VehicleStatus = i_NewStatus;
        }
        public void AddNewVehicle(Vehicle i_Vehicle, string i_lisenceNumber, string i_costumerName, 
            string  i_costumerPhoneNumber)
        {
            bool isVehicleInGarage = IsLicenseNumberInGarage(i_lisenceNumber);
            if(isVehicleInGarage)
            { 
                ChangeVehicleStatus(i_lisenceNumber, eGarageStatus.InRepair);
                StringBuilder exceptionMassege = new StringBuilder();
                exceptionMassege.Append(@"The vehicle does not exist in the garage, the vehicle will be taken care of");
                throw new ArgumentException(exceptionMassege.ToString());
            }
            Costumer costumerToAdd = new Costumer(i_costumerName, i_costumerPhoneNumber,i_Vehicle );
            m_CurrentCostumers.Add(i_lisenceNumber, costumerToAdd);
        }
        public void AddAirToWheels(string i_LicenseNumber)
        {
            bool isvehicleInGarage = IsLicenseNumberInGarage(i_LicenseNumber);
            if (!isvehicleInGarage)
            {
                StringBuilder sexceptionMassege = new StringBuilder();
                sexceptionMassege.Append(@"The vehicle does not exist in the garage!");
                throw new ArgumentException(sexceptionMassege.ToString());
            }

            Costumer pumpWheel = m_CurrentCostumers[i_LicenseNumber];

            foreach (Wheel  wheel in pumpWheel.Vehicle.Wheels)
            {
                // Calculate how many airpressure needs to be filled until max capacity
                float airPressureUntilMax = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.PumpingWheel(wheel, airPressureUntilMax);
            }
        }
        public void FillFuel(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountToFill)
        {
            bool isvehicleInGarage = IsLicenseNumberInGarage(i_LicenseNumber);
            if (!isvehicleInGarage)
            {
                StringBuilder sexceptionMassege = new StringBuilder();
                sexceptionMassege.Append(@"The vehicle does not exist in the garage!");
                throw new ArgumentException(sexceptionMassege.ToString());
            }

            Costumer costumerToFillTank = m_CurrentCostumers[i_LicenseNumber];
            FuelEngine engineToFill = costumerToFillTank.Vehicle.Engine as FuelEngine;
            engineToFill.FillEngineFuel(i_AmountToFill, i_FuelType);
        }
        public void ChargeBattery(string i_LicenseNumber, int i_HoursToCharge)
        {
            bool isvehicleInGarage = IsLicenseNumberInGarage(i_LicenseNumber);
            if (!isvehicleInGarage)
            {
                StringBuilder sexceptionMassege = new StringBuilder();
                sexceptionMassege.Append(@"The vehicle does not exist in the garage!");
                throw new ArgumentException(sexceptionMassege.ToString());
            }

            Costumer costumerToFillTank = m_CurrentCostumers[i_LicenseNumber];
            costumerToFillTank.Vehicle.Engine.FillEngine(i_HoursToCharge);
        }

        //the function gets array of the types of vehicle status to list 
        public List<string> ListOfVehicls(params string[] i_Types)
        {
            List<string> licenseNumberList = new List<string>();
            int numOfTypes = i_Types.Length;
            bool isAllTypes = numOfTypes == 0;

            foreach (KeyValuePair<string, Costumer> licenseNumber in m_CurrentCostumers)
            {
                if (isAllTypes)
                {
                    licenseNumberList.Add(licenseNumber.Key);
                }
                else
                {
                    eGarageStatus status = m_CurrentCostumers[licenseNumber.Key].VehicleStatus;
                    string typeNameOfStatus = Enum.GetName(typeof(eGarageStatus), licenseNumber.Value.VehicleStatus);
                    foreach (string type in i_Types)
                    {
                        bool isCareTypeMatch = typeNameOfStatus == type;
                        if (isCareTypeMatch)
                        {
                            licenseNumberList.Add(licenseNumber.Key);
                        }
                        break;
                    }
                }
            }
            return licenseNumberList;
        }
        public string GetVehicleDetails(string i_LicenseNumber)
        {
            StringBuilder vehicleDetails = new StringBuilder();
            bool isvehicleInGarage = IsLicenseNumberInGarage(i_LicenseNumber);
            if (!isvehicleInGarage)
            {
                StringBuilder sexceptionMassege = new StringBuilder();
                sexceptionMassege.Append(@"The vehicle does not exist in the garage!");
                throw new ArgumentException(sexceptionMassege.ToString());
            }
            string vehicleInfo = m_CurrentCostumers[i_LicenseNumber].Vehicle.ShowDetails();
            vehicleDetails.AppendFormat(@"{0}", vehicleInfo);
            return vehicleDetails.ToString();
        }
    }
}
