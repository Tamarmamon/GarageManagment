using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private readonly float r_MaxEnergyTime = 3.8F;
        public ElectricCar() : base ()
        {
            Engine = new ElectricEngine(r_MaxEnergyTime);
        }
        public ElectricCar(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, eCarColor i_carColor, eNumberOfDoors i_numOfDoors, ElectricEngine i_engine) 
            : base(i_model, i_licenseNumber, i_energyLevel, i_wheels, i_carColor, i_numOfDoors)
        {
            Engine = new ElectricEngine(r_MaxEnergyTime);
        }
        public float MaxEnergyTime
        {
            get
            {
                return r_MaxEnergyTime;
            }

        }
        public override string ShowDetails()
        {
            // General vehicle details
            string generalCarDetails = base.ShowDetails();

            // Info from members
            string engineDetails = Engine.ShowDetails();
           
            StringBuilder electricCarDetails = new StringBuilder();
            electricCarDetails.AppendFormat(
                        @"
						{0}
						Engine information {1}",
                        generalCarDetails,
                        engineDetails);

            return electricCarDetails.ToString();
        }
    }
}
