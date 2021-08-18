using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private readonly int r_maxAirPressure = 32;
        private eNumberOfDoors m_NumberOfDoors;
        private eCarColor m_CarColor;
        public Car() : base()
        {
            m_NumberOfDoors = new eNumberOfDoors();
            m_CarColor = new eCarColor();
        }
        public Car(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, eCarColor i_carColor, eNumberOfDoors i_numOfDoors) 
            : base( i_model, i_licenseNumber,i_energyLevel, i_wheels)
        {
            m_NumberOfDoors = i_numOfDoors;
            m_CarColor = i_carColor;
        }
		public eCarColor carColor
		{
			get
			{
				return m_CarColor;
			}

			set
			{
				m_CarColor = value;
			}
		}
		public eNumberOfDoors NumberOfDoors
		{
			get
			{
				return m_NumberOfDoors;
			}

			set
			{
				m_NumberOfDoors = value;
			}
		}
		public override void SetWheels(string i_manufacturer)
		{
			Wheel wheelToAdd;
			float currentAirPressure = 0;

			for (int i = 0; i < k_NumOfWheels; i++)
			{
				wheelToAdd = new Wheel(i_manufacturer, currentAirPressure, r_maxAirPressure);

				Wheels.Add(wheelToAdd);
			}
		}
		public override string ShowDetails()
		{
			// General vehicle details
			string generalVehicleDetails = base.ShowDetails();

			// Info from members
			string carColor = Enum.GetName(typeof(eCarColor), m_CarColor);
			string numOfDoors = Enum.GetName(typeof(eNumberOfDoors), m_NumberOfDoors);
			StringBuilder carDetails = new StringBuilder();
			carDetails.AppendFormat(
						@"
						{0}
						Car color: {1}
						Num of doors: {2}",
						generalVehicleDetails,
						carColor,
						numOfDoors);

			return carDetails.ToString();
		}
	}
}
