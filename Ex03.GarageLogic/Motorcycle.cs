using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        //const
        private const int k_NumOFWheels = 2;
        private readonly int r_MaxAirPressure = 30;
       
        //field members
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle()
        {
            m_LicenseType = new eLicenseType();
            m_EngineCapacity = 0;
        }

        //constructor
        public Motorcycle(string i_model, string i_licenseNumber, float i_energyLevel, List<Wheel> i_wheels, eLicenseType i_licenseType, int i_engineCapacity) 
            : base(i_model, i_licenseNumber, i_energyLevel, i_wheels)
        {
            m_LicenseType = i_licenseType;
            m_EngineCapacity = i_engineCapacity;
        }
		// Get & Set
		public int EngineCapacity
		{
			get
			{
				return m_EngineCapacity;
			}
			set
			{
				m_EngineCapacity = value;
			}
		}
		public eLicenseType LicenseType
		{
			get
			{
				return m_LicenseType;
			}

			set
			{
				m_LicenseType = value;
			}
		}
		public override void SetWheels(string i_manufacturer)
		{
			Wheel wheelToAdd;
			float currentAirPressure = 0;

			for (int i = 0; i < k_NumOFWheels; i++)
			{
				wheelToAdd = new Wheel(i_manufacturer, currentAirPressure,r_MaxAirPressure);

				Wheels.Add(wheelToAdd);
			}
		}
		public override string ShowDetails()
		{
			// General vehicle details
			string generalVehicleDetails = base.ShowDetails();

			// Info from members
			string licenseType = Enum.GetName(typeof(eLicenseType), m_LicenseType);

			// Final value to return
			StringBuilder strToReturn = new StringBuilder();

			// Build the string
			strToReturn.AppendFormat(
						@"{0}
						Engine capacity: {1},
						License type: {2}",
						generalVehicleDetails,
						m_EngineCapacity,
						licenseType);

			// Return the result
			return strToReturn.ToString();
		}
	}
}
