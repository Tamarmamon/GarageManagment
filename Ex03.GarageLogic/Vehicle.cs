using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_Model;
        private string m_LicenseNumber;
        private float m_PercentageOfEnergyLeft;
        private List<Wheel> m_Wheels;
		protected Engine r_Engine;
        public Vehicle()
        {
            m_Model = string.Empty;
            m_LicenseNumber = string.Empty;
            m_PercentageOfEnergyLeft = 0;
			m_Wheels = new List<Wheel>();
        }
        public Vehicle(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels)
        {
            m_Model = i_model;
            m_LicenseNumber = i_licenseNumber;
            m_PercentageOfEnergyLeft = i_energyLevel;
            m_Wheels = i_wheels;
        }
		public string Model
        {
			get
			{
				return m_Model;
			}

			set
			{
				m_Model = value;
			}
		}
		public string LicenseNumber
		{
			get
			{
				return m_LicenseNumber;
			}

			set
			{
				m_LicenseNumber = value;
			}
		}
		public float PercentageOfEnergyLeft
		{
			get
			{
				return m_PercentageOfEnergyLeft;
			}

			set
			{
				m_PercentageOfEnergyLeft = value;
			}
		}
		public List<Wheel> Wheels
		{
			get
			{
				return m_Wheels;
			}

			set
			{
				m_Wheels = value;
			}
		}
		public Engine Engine
        {
			get
			{
				return r_Engine;
			}
			set
            {
				r_Engine = value;
            }
       }
		public void SetEnergyPercentage()
		{
			PercentageOfEnergyLeft = (Engine.EnergyLeft / Engine.MaxEnergyCapacity) * 100;
		}
		public abstract void SetWheels(string i_manufacturer);
		public static void CheckValidLicenseNumber(string i_LicenseNumber)
		{
			if (string.IsNullOrEmpty(i_LicenseNumber))
			{
				throw new FormatException();
			}
		}
		public virtual string ShowDetails()
		{
			// Final value to return
			StringBuilder vehicleDetails = new StringBuilder();

			// Build string
			vehicleDetails.AppendFormat(@"
						Model name: {0}
						License number: {1}
						Remaining energy: {2}
						Wheels details:
						{3}
						",
						m_Model,
						m_LicenseNumber,
						m_PercentageOfEnergyLeft,
						m_Wheels[0].WheelDetails());
						
			return vehicleDetails.ToString();
		}
	}
}
