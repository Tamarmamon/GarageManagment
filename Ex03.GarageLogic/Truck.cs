using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
	public class Truck : Vehicle
	{
		//const
		private const int k_NumberOfWheels = 16;
		private const float k_MaxPressure = 26;
		private const eFuelType k_FuelType = eFuelType.Soler;
		private const int k_FuelTunkLiters = 120;

		//fields
		private bool r_DangerousMaterials;
		private float r_MaxCargoWeight;
		
		//contractors
		public Truck()
			: base()
		{
			r_DangerousMaterials = false;
			r_MaxCargoWeight = 0;
			Engine = new FuelEngine(k_FuelType,k_FuelTunkLiters);
		}
		public Truck(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, bool i_dangerousMaterials, float i_maxCargoWeight)
			: base(i_model, i_licenseNumber, i_energyLevel, i_wheels)
		{
			r_DangerousMaterials = i_dangerousMaterials;
			r_MaxCargoWeight = i_maxCargoWeight;
			Engine = new FuelEngine(k_FuelType, k_FuelTunkLiters);
		}
		// Get & Set
		public bool DangerousMaterials
		{
			get
			{
				return r_DangerousMaterials;
			}

			set
			{
				r_DangerousMaterials = value;
			}
		}
		public float MaxCargoWeight
		{
			get
			{
				return r_MaxCargoWeight;
			}

			set
			{
				r_MaxCargoWeight = value;
			}
		}
		public override void SetWheels(string i_manufacturer)
		{
			Wheel wheelToAdd;
			float currentAirPressure = 0;

			for (int i = 0; i < k_NumberOfWheels; i++)
			{
				wheelToAdd = new Wheel(i_manufacturer, currentAirPressure, k_MaxPressure);
				Wheels.Add(wheelToAdd);
			}
		}
		public override string ShowDetails()
		{
			// General car details
			string generalCarDetails = base.ShowDetails();

			// Info from members
			string maxCargoWeight = r_MaxCargoWeight.ToString();
			string dangerousMaterials = r_DangerousMaterials.ToString();
			string fuelEngineDetails = Engine.ShowDetails();

			// Final value to return
			StringBuilder truckDetails = new StringBuilder();

			// Build the string
			truckDetails.AppendFormat(
						@"{0}
						Is truck carrying hazardous materials: {1}
						Cargo capacity: {2},
						Engine Details:{3}",
						generalCarDetails,
						dangerousMaterials,
						maxCargoWeight,
						fuelEngineDetails);

			// Return the result
			return truckDetails.ToString();
		}
	}
}
