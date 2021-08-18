using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
		public enum eVehicleType
        {
			ElectricCar, FuelCar, ElectricMotorcycle, FuelMotorcycle, Truck
        }
		public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
		{
			// Final value to return
			Vehicle newVehicle = null;

			// Check the vehicle type, and create a new instance of it
			switch (i_VehicleType)
			{
				case eVehicleType.ElectricCar:
					newVehicle = new ElectricCar();
					break;

				case eVehicleType.FuelCar:
					newVehicle = new FuelCar();
					break;

				case eVehicleType.ElectricMotorcycle:
					newVehicle = new ElectricMotorcycle();
					break;

				case eVehicleType.FuelMotorcycle:
					newVehicle = new FuelMotorcycle();
					break;

				case eVehicleType.Truck:
					newVehicle = new Truck();
					break;
			}

			// Return the result
			return newVehicle;
		}
	}
}
