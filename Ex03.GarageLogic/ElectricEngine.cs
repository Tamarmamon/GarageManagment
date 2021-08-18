using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine: Engine
    {
        private const eEngineType k_EngineType = eEngineType.Electric;
        public ElectricEngine(float i_MaxBatteryTime) : base(i_MaxBatteryTime, k_EngineType)
        {
        }
        public override void FillEngine(float i_HoursToAdd)
        {
            bool isOverMax = CheckIfHoursOverMax(i_HoursToAdd);

            // Check that user not adding negative amount of hours
            try
            {
                bool isNegative = i_HoursToAdd < 0;
            }
            catch (ArgumentException ae)
            {
                throw new ArgumentException(ae.Message);
            }

            // Fill battery until max capacity
            if (isOverMax)
            {
                EnergyLeft = MaxEnergyCapacity;
            }
            else
            {
                EnergyLeft += i_HoursToAdd;
            }
        }
        private bool CheckIfHoursOverMax(float i_HoursToAdd)
        {
            return i_HoursToAdd + EnergyLeft > MaxEnergyCapacity;
        }
        public override string ShowDetails()
        {

            // Info from members
            string energyLeft = EnergyLeft.ToString();
           
            // Final value to return
            StringBuilder engineDetails = new StringBuilder();

            engineDetails.AppendFormat(
                @"Energy time left : {0}",
                  energyLeft);

            return engineDetails.ToString();
        }
    }
}
