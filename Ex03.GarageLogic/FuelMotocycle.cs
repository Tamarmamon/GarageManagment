using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private readonly float r_MaxFuelAmount = 6;
        public FuelMotorcycle() : base()
        {
            Engine = new FuelEngine(k_FuelType, r_MaxFuelAmount);
        }
        public FuelMotorcycle(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, eLicenseType i_licenseType, int i_engineCapacity)
            : base(i_model, i_licenseNumber, i_energyLevel, i_wheels, i_licenseType, i_engineCapacity)
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
            string generalMotorcycleDetails = base.ShowDetails();

            // Info from members
            string engineDetails = Engine.ShowDetails();

            StringBuilder fuelCarDetails = new StringBuilder();
            fuelCarDetails.AppendFormat(
                        @"
						{0}
						Engine information {1}",
                        generalMotorcycleDetails,
                        engineDetails);
 
            return fuelCarDetails.ToString();
        }
    }
}

