using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Costumer
    {
		private const byte k_PhoneNumberDigits = 10;
		private string m_CostumerName;
        private string m_CostumerPhoneNumber;
        private eGarageStatus m_VehicleStatus;
        private Vehicle m_Vehicle;
		public Costumer()
        {
			m_CostumerName = string.Empty;
			m_CostumerPhoneNumber = string.Empty;
			m_VehicleStatus = eGarageStatus.InRepair;
        }
        public Costumer(string i_name, string i_phoneNumber, Vehicle i_vehicle)
        {
            m_CostumerName = i_name;
            m_CostumerPhoneNumber = i_phoneNumber;
            m_Vehicle = i_vehicle;
			m_VehicleStatus = eGarageStatus.InRepair;
		}
		// Getters and setters
		public string CostumerName
		{
			get
			{
				return m_CostumerName;
			}

			set
			{
				m_CostumerName = value;
			}
		}
		public string CostumerPhonenumber
		{
			get
			{
				return m_CostumerPhoneNumber;
			}

			set
			{
				m_CostumerPhoneNumber = value;
			}
		}
		public eGarageStatus VehicleStatus
		{
			get
			{
				return m_VehicleStatus;
			}

			set
			{
				m_VehicleStatus = value;
			}
		}
		public Vehicle Vehicle
		{
			get
			{
				return m_Vehicle;
			}

			set
			{
				m_Vehicle = value;
			}
		}
		public static void IsCostumerNameValid(string i_CustomerName)
		{
			if (string.IsNullOrEmpty(i_CustomerName))
			{
				throw new FormatException();
			}
		}
		public static bool IsCostumerPhoneNumberValid(string i_PhoneNumber)
		{
			bool isParsingSuccess;
			bool isPhoneNumberTenDigits = i_PhoneNumber.Length == 10;
			if (!isPhoneNumberTenDigits)
			{
				throw new ValueOutOfRangeException(k_PhoneNumberDigits, k_PhoneNumberDigits);
			}
			isParsingSuccess = ulong.TryParse(i_PhoneNumber, out ulong phoneNumber);
			return isParsingSuccess;
		}
	}
}
