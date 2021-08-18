using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public  class GarageUI
    {
        private readonly Garage r_Garage;
		private UIManager m_UIManager;

		public enum eVehicleDisplayOption
		{
			AllStatuses,
			SpecificStatuses,
		}

		// Constructor
		public GarageUI()
		{
			r_Garage = new Garage();
			m_UIManager = new UIManager();
		}

		// Getters and setters
		public Garage Garage
		{
			get
			{
				return r_Garage;
			}
		}

		public UIManager UIManager
		{
			get
			{
				return m_UIManager;
			}

			set
			{
				m_UIManager = value;
			}
		}

		// Methods
		public void RunGarage()
		{
			UIManager.eGarageServices chooseService;
			bool isUserWantsToQuit = false;
			do
			{
				try
				{
					chooseService = m_UIManager.PrintMenuAndGetAction();
					switch(chooseService)
					{
						case UIManager.eGarageServices.ChangeVehicleStatus:
							ChangeVehicleStatus();
							break;

						case UIManager.eGarageServices.FillAirInWheels:
							FillAirInWheels();
							break;

						case UIManager.eGarageServices.FillFuel:
							FillFuel();
							break;
						case UIManager.eGarageServices.ChargeEnergy:
							ChargeBattery();
							break;

						case UIManager.eGarageServices.GetDetailsOfVehicle:
							GetDetailsOfVehicle();
							break;

						case UIManager.eGarageServices.GetVehiclesList:
							GetVehiclesInGarage();
							break;

						case UIManager.eGarageServices.InsertVehicleToGarage:
							InsertNewVehicleToGarage();
							break;

						case UIManager.eGarageServices.Quit:
							isUserWantsToQuit = true;
							break;

						default:
							UIManager.PrintInputInvalidToUser();
							break;
					}
				}
				catch (Exception ex)
				{
					if (ex is FormatException || ex is OverflowException)
					{
						UIManager.PrintInputInvalidToUser();
					}
					else if (ex is ArgumentException || ex is ValueOutOfRangeException)
					{
						UIManager.PrintToUser(ex.Message);
					}
				}
			}
			while (!isUserWantsToQuit);
		}
		private void InsertNewVehicleToGarage()
		{
			bool isVehicleInGarage = false;
			string licenseNumber = GetLicenseNumber();
			Vehicle newVehicle;
			try
			{
				isVehicleInGarage = r_Garage.IsLicenseNumberInGarage(licenseNumber);
			}
			catch (ArgumentException)
			{
				isVehicleInGarage = false;
			}

			// If vehicle dosent exist in garage, insert it to garage
			if (!isVehicleInGarage)
			{
				string vehicleModel = m_UIManager.GetVehicleModel();
				string wheelManufacturer = m_UIManager.GetWheelsManufacturer();
				string costumerName = m_UIManager.GetCostumerName();
				string costumerPhoneNumber = m_UIManager.GetCostumerPhoneNumber();
				newVehicle = CreateVehicle(licenseNumber, vehicleModel, wheelManufacturer, costumerName, costumerPhoneNumber);
				newVehicle.SetWheels(wheelManufacturer);
				InsertWheelsAirPressure(newVehicle);
				newVehicle.Model = vehicleModel;
				newVehicle.LicenseNumber = licenseNumber;
				newVehicle.SetEnergyPercentage();
				r_Garage.AddNewVehicle(newVehicle, licenseNumber, costumerName, costumerPhoneNumber);
			}
			else
			{
				// If vehicle exist in garage, change its status to "in Repair"
				m_UIManager.PrintToUser("Vehicle already exists in garage! Vehicle status was set to: In Repair");
				r_Garage.ChangeVehicleStatus(licenseNumber, eGarageStatus.InRepair);
			}
		}
		private Vehicle CreateVehicle(string i_LicensNumber, string i_VehicleModel, string i_WheelManufacturer, string i_OwnerFullName, string i_OwnerPhoneNumber)
		{
			Vehicle vehicle = null;
			VehicleFactory.eVehicleType vehicleType = new VehicleFactory.eVehicleType();
			string strToPrint = "Please enter vehicle type: ";			
			// Get vehicle type from user
			vehicleType = (VehicleFactory.eVehicleType)m_UIManager.GetEnumInput(strToPrint, vehicleType);
			switch (vehicleType)
			{
				case VehicleFactory.eVehicleType.Truck:
					vehicle = VehicleFactory.CreateVehicle(vehicleType);
					InsertCargoWeight((Truck)vehicle);
					InsertIsCarryingHazardousMaterials((Truck)vehicle);
					break;
				case VehicleFactory.eVehicleType.ElectricCar:
					vehicle = VehicleFactory.CreateVehicle(vehicleType);
					InsertDoorsNumber((Car)vehicle);
					InsertCarColor((Car)vehicle);
					break;
				case VehicleFactory.eVehicleType.FuelCar:
					vehicle = VehicleFactory.CreateVehicle(vehicleType);
					InsertDoorsNumber((Car)vehicle);
					InsertCarColor((Car)vehicle);
					break;
				case VehicleFactory.eVehicleType.ElectricMotorcycle:
					vehicle = VehicleFactory.CreateVehicle(vehicleType);
					InsertMotorcycleLicenseType((Motorcycle)vehicle);
					InsertEngineCapacity((Motorcycle)vehicle);
					break;
				case VehicleFactory.eVehicleType.FuelMotorcycle:
					vehicle = VehicleFactory.CreateVehicle(vehicleType);
					InsertMotorcycleLicenseType((Motorcycle)vehicle);
					InsertEngineCapacity((Motorcycle)vehicle);
					break;
			}
			return vehicle;
		}

        private void InsertIsCarryingHazardousMaterials(Truck i_Truck)
		{
			m_UIManager.PrintToUser("Is the truck carrying hazardous materials?");
			i_Truck.DangerousMaterials = m_UIManager.ReadTrueOrFalseFromUser();
		}

		private void InsertWheelsAirPressure(Vehicle i_Vehicle)
		{
			float airPressureToAdd;
			bool isAirPressureValid = false;
			do
			{
				airPressureToAdd = m_UIManager.GetCurrentWheelsAirPressure();
				try
				{
					foreach (Wheel wheel in i_Vehicle.Wheels)
					{
						wheel.PumpingWheel(wheel ,airPressureToAdd);
					}
					isAirPressureValid = true;
				}
				catch (ValueOutOfRangeException voore)
				{
					m_UIManager.PrintToUser(voore.Message);
					isAirPressureValid = false;
				}
			}
			while (!isAirPressureValid);
		}

		private void InsertCarColor(Car i_Car)
		{
			int carColor;
			eCarColor color = new eCarColor();
			
			carColor = m_UIManager.GetEnumInput("Please enter vehicle color:", color);
			i_Car.carColor = (eCarColor)carColor;
		}
		private void InsertDoorsNumber(Car i_Car)
		{
			int userChoice;
			eNumberOfDoors numOfDoors = new eNumberOfDoors();
			userChoice = m_UIManager.GetEnumInput("Please enter doors number:", numOfDoors);
			i_Car.NumberOfDoors = (eNumberOfDoors)userChoice;
		}
		private void InsertCargoWeight(Truck i_Truck)
		{
			float cargoCapacity = m_UIManager.GetCargoWeight();
			i_Truck.MaxCargoWeight = cargoCapacity;
		}
		private eFuelType InsertFuelType()
		{
			int userChoice;
			eFuelType fuelType = new eFuelType();
			userChoice = m_UIManager.GetEnumInput("Please enter type of fuel:", fuelType);
			return (eFuelType)userChoice;
		}
		private void InsertEngineCapacity(Motorcycle i_Motorcycle)
		{
			int engineCapacity = m_UIManager.GetEnergySourceCapacity();
			i_Motorcycle.EngineCapacity = engineCapacity;
		}
		private void InsertMotorcycleLicenseType(Motorcycle i_Motorcycle)
		{
			int motorcycleLicenseType;
			eLicenseType licenseType = new eLicenseType();
			motorcycleLicenseType = m_UIManager.GetEnumInput("Please insert license type:", licenseType);
			i_Motorcycle.LicenseType = (eLicenseType)motorcycleLicenseType;
		}
		private void GetVehiclesInGarage()
		{
			string[] statusesTypes = ReadStatusesFromUser();
			// Get list of vehicles in garage			
			List<string> vehiclesInGarage;
			bool isStatuesTypesEmpty = IsArrayEmpty(statusesTypes);

			if (isStatuesTypesEmpty)
			{
				vehiclesInGarage = r_Garage.ListOfVehicls(); 
			}
			else
			{
				vehiclesInGarage = r_Garage.ListOfVehicls(statusesTypes);
			}
			UIManager.PrintList(vehiclesInGarage);
		}
		private bool IsArrayEmpty(string[] i_StringArray)
		{
			bool isCellEmpty;
			bool isAllArrayCellsBlank = true;			
			foreach (string cell in i_StringArray)
			{
				isCellEmpty = string.IsNullOrWhiteSpace(cell);
				if(!isCellEmpty)
				{
					isAllArrayCellsBlank = false;
					break;
				}
			}
			return isAllArrayCellsBlank;
		}
		private string[] ReadStatusesFromUser()
		{
			string[] statuses;
			string inputFromUser;
			StringBuilder strToPrint = new StringBuilder();
			strToPrint.AppendFormat(
							@"Please enter vehicle statuses you want to display with a comma separator (leave blank if you want them all) {0}For example: InRepair, Fixed, Paid",
							Environment.NewLine);
			m_UIManager.PrintToUser(strToPrint.ToString());
			inputFromUser = m_UIManager.ReadLineFromUser();
			inputFromUser = RemoveSpaces(inputFromUser);
			statuses = inputFromUser.Split(',');
			return statuses;
		}
		private string RemoveSpaces(string i_String)
		{
			string newString;
			newString = i_String.Replace(" ", string.Empty);
			newString = newString.Trim();

			return newString;
		}
		private void ChangeVehicleStatus()
		{
			string licenseNumber = GetLicenseNumber();
			string strToPrint = "Please enter desired vehicle status:";
			eGarageStatus vehicleStatus = new eGarageStatus();
			int userChoice;
			bool isVehicleExistInGarage = IsVehicleInGarage(licenseNumber);

			if (isVehicleExistInGarage)
			{
				// Get vehicle status from user
				userChoice = m_UIManager.GetEnumInput(strToPrint, vehicleStatus);
				vehicleStatus = (eGarageStatus)userChoice;
				r_Garage.ChangeVehicleStatus(licenseNumber, vehicleStatus);
			}
			else
			{
				m_UIManager.PrintToUser("Vehicle not in garage!");
			}
		}
		private void FillAirInWheels()
		{
			string licenseNumber = GetLicenseNumber();

			r_Garage.AddAirToWheels(licenseNumber);
		}
		private void FillFuel()
        {
			string licenseNumber = GetLicenseNumber();
			bool isVehicleExistInGarage = IsVehicleInGarage(licenseNumber);
			if (isVehicleExistInGarage)
			{
				Vehicle vehicle = r_Garage.CurrentCostumers[licenseNumber].Vehicle;
				if(vehicle.Engine.EngineType != Engine.eEngineType.Fuel)
                {
					throw new ArgumentException("Cant fuel an Electric Engine");
				}
				InsertFuelToAdd(vehicle);
				vehicle.SetEnergyPercentage();
			}
			else
			{
				m_UIManager.PrintToUser("Vehicle not in garage!");
			}
		}
		private void ChargeBattery() 
		{
			string licenseNumber = GetLicenseNumber();
			bool isVehicleExistInGarage = IsVehicleInGarage(licenseNumber);
			if (isVehicleExistInGarage)
			{
				Vehicle vehicle = r_Garage.CurrentCostumers[licenseNumber].Vehicle;
				if (vehicle.Engine.EngineType != Engine.eEngineType.Electric)
				{
					throw new ArgumentException("Cant charge a Fuel Engine");
				}
				InsertEnergy(vehicle);
				vehicle.SetEnergyPercentage();
			}
			else
			{
				m_UIManager.PrintToUser("Vehicle not in garage!");
			}
		}
		private void InsertFuelToAdd(Vehicle i_vehicle)
        {
			string inputFromUser;
			bool isInputValid = false;
			float amountOfEnergyToAdd;
			eFuelType fuelType = InsertFuelType();
			m_UIManager.PrintToUser("Refuel - Please enter amount to fill:");
			do
			{
				try
				{
					inputFromUser = m_UIManager.ReadLineFromUser();
					amountOfEnergyToAdd = float.Parse(inputFromUser);
					Garage.FillFuel(i_vehicle.LicenseNumber, fuelType, amountOfEnergyToAdd);
					isInputValid = true;
				}
				catch (ValueOutOfRangeException voore)
				{
					m_UIManager.PrintToUser(voore.Message);
				}
				catch (ArgumentException)
				{
					m_UIManager.PrintInputInvalidToUser();
				}
			}
			while (!isInputValid);
		}
		private void InsertEnergy(Vehicle i_vehicle)
        {
			string inputFromUser;
			bool isInputValid = false;
			m_UIManager.PrintToUser("Charge - Please enter number of minutes to charge:");
			do
			{
				try
				{
					int amountOfMinToAdd;
					inputFromUser = m_UIManager.ReadLineFromUser();
					amountOfMinToAdd = int.Parse(inputFromUser);
					Garage.ChargeBattery(i_vehicle.LicenseNumber, amountOfMinToAdd);
					isInputValid = true;
				}
				catch (ValueOutOfRangeException voore)
				{
					m_UIManager.PrintToUser(voore.Message);
				}
				catch (ArgumentException)
				{
					m_UIManager.PrintInputInvalidToUser();
				}
			}
			while (!isInputValid);
		}
		private void GetDetailsOfVehicle()
		{
			string vehicleDetails;
			string licenseNumber = GetLicenseNumber();
			vehicleDetails = Garage.GetVehicleDetails(licenseNumber);
			m_UIManager.PrintToUser(vehicleDetails);
		}	
		private string GetLicenseNumber()
		{
			string licenseNumber = string.Empty;
			UIManager.GetVehicleLicensePlate(r_Garage, ref licenseNumber);
			return licenseNumber;
		}
		private bool IsVehicleInGarage(string i_LicenseNumber)
		{
			bool isLicenseNumberInGarage = r_Garage.CurrentCostumers.ContainsKey(i_LicenseNumber);
			return isLicenseNumberInGarage;
		}
    }
}
