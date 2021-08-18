using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle: Motorcycle
    {
        private readonly float r_MaxTimeEnergy = 1.8F;
        public ElectricMotorcycle() : base()
        {
            Engine = new ElectricEngine(r_MaxTimeEnergy);
        }
        public ElectricMotorcycle(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, eLicenseType i_licenseType, int i_engineCapacity, ElectricEngine i_engine) 
            : base(i_model, i_licenseNumber, i_energyLevel,  i_wheels,  i_licenseType,  i_engineCapacity)
        {
            Engine = new ElectricEngine(r_MaxTimeEnergy);
        }
        // getters & setters
        public float MaxEnergyTime
        {
            get
            {
                return r_MaxTimeEnergy;
            }
        }
        public override string ShowDetails()
        {
            string generalVehicleDetails = base.ShowDetails();
            // Info from members
            string electricEngineDetails = Engine.ShowDetails();

            // Final value to return
            StringBuilder strToReturn = new StringBuilder();
            strToReturn.AppendFormat(
                        @"{0}
						{1}",
                        generalVehicleDetails,
                        electricEngineDetails);

            return strToReturn.ToString();
        }

    }
}
