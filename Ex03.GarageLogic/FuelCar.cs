using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar: Car
    {
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private readonly float r_MaxFuelAmount = 45;
        public FuelCar() : base()
        {
            Engine = new FuelEngine(k_FuelType,r_MaxFuelAmount);
        }
        public FuelCar(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, eCarColor i_carColor, eNumberOfDoors i_numOfDoor, FuelEngine i_engine) 
            : base(i_model, i_licenseNumber, i_energyLevel, i_wheels, i_carColor, i_numOfDoor)
        {
            Engine = new FuelEngine(k_FuelType, r_MaxFuelAmount);
        }
        public float MaxFuelAmmount
        {
            get
            {
                return r_MaxFuelAmount;
            }
        }
        public override string ShowDetails()
        {
            // General vehicle details
            string generalCarDetails = base.ShowDetails();

            // Info from members
            string engineDetails = Engine.ShowDetails();

            // Final value to return
            StringBuilder fuelCarDetails = new StringBuilder();

            fuelCarDetails.AppendFormat(
                        @"
						{0}
						Engine information {1}",
                        generalCarDetails,
                        engineDetails);

            return fuelCarDetails.ToString();
        }
    }
}
