using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine: Engine
    {
        private readonly eFuelType r_FuelType;
        private const eEngineType k_EngineType = eEngineType.Fuel;
        public FuelEngine(eFuelType i_FuelType, float i_MaxFuelCapacity) : base(i_MaxFuelCapacity, k_EngineType)
        {
            r_FuelType = i_FuelType;
        }
        public override void FillEngine(float i_FuelToAdd)
        {
            // Check if user wants to add more fuel than max allowed
            bool isOverMax = EnergyLeft + i_FuelToAdd > MaxEnergyCapacity;

            // Check if fuel to add is negative
            try
            {
                bool isNegatice = i_FuelToAdd < 0;
            }
            catch (ArgumentException ae)
            {
                throw new ArgumentException(ae.Message);
            }
            // If user wants to add more fuel than max allowed, throw out of range exception
            if (isOverMax)
            {
                float maxValue = MaxEnergyCapacity - EnergyLeft;
                float minValue = 0;

                throw new ValueOutOfRangeException(maxValue, minValue);
            }
            // If fuel quantity and fuel type are valid, add fuel to vehicle
            EnergyLeft += i_FuelToAdd;
        }
        public void FillEngineFuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            bool isFuelTypeMatch = FuelType == i_FuelType;

            // If fuel type does not match car's fuel type, throw argument exception
            if (!isFuelTypeMatch)
            {
                throw new ArgumentException("The fuel type does not match!");
            }
            else
            {
                FillEngine(i_FuelToAdd);
            }

        }
        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }
        public override string ShowDetails()
        {
            // Info from members
            string fuelType = Enum.GetName(typeof(eFuelType), r_FuelType);
            string currentLitersOfFuel = EnergyLeft.ToString();

            // Final value to return
            StringBuilder engineDetails = new StringBuilder();

            // Build the string
            engineDetails.AppendFormat(
                @"Current liters of fuel : {0}
                Fuel type : {1}",
                currentLitersOfFuel,
                fuelType);

            return engineDetails.ToString();
        }

    }
}
