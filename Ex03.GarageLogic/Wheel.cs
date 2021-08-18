using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;
        public Wheel(string i_manifacturer, float i_currentAirPressure, float i_maxAirPressure)
        {
            m_Manufacturer = i_manifacturer;
            m_CurrentAirPressure = i_currentAirPressure;
			r_MaxAirPressure = i_maxAirPressure;
        }
		public string ManufacturerName
		{
			get
			{
                return m_Manufacturer;

			}

			set
			{
				m_Manufacturer = value;
			}
		}
		public float CurrentAirPressure
		{
			get
			{
				return m_CurrentAirPressure;
			}

			set
			{
				m_CurrentAirPressure = value;
			}
		}
		public float MaxAirPressure
		{
			get
			{
				return r_MaxAirPressure;
			}
		
		}
		public void PumpingWheel(Wheel i_wheel, float i_airToAdd)
        {
			bool isOverMaxAirPressure = m_CurrentAirPressure + i_airToAdd > r_MaxAirPressure;
			bool isAirPressureToAddNegative = i_airToAdd < 0;

			// If user wants to add negative amount of air pressure, throw an exception
			if (isAirPressureToAddNegative)
			{
				throw new ArgumentException("Cannot add negative amount of air pressure!");
			}

			// If final quantity is over max, throw exception of out of range
			if (isOverMaxAirPressure)
			{
				float minValue = 0;
				float maxValue = r_MaxAirPressure;

				throw new ValueOutOfRangeException(maxValue, minValue);
			}

			// If quantity is valid, add pressure to wheel
			m_CurrentAirPressure += i_airToAdd;
        }
		public string WheelDetails()
		{
			StringBuilder wheelDetails = new StringBuilder();
			wheelDetails.AppendFormat(
						@"Manufacturer name: {0}
						Air pressure: {1}
						Max air pressure: {2}",
						m_Manufacturer,
						m_CurrentAirPressure,
						r_MaxAirPressure);

			return wheelDetails.ToString();
		}
	}
}