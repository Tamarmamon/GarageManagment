using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
		public enum eGarageServices
		{
			InsertVehicleToGarage,
			GetVehiclesList,
			ChangeVehicleStatus,
			FillAirInWheels,
			FillFuel,
			ChargeEnergy,
			GetDetailsOfVehicle,
			Quit,
		}
		// Constructor
		public UIManager()
		{
		}
		// Methods
		public void PrintToUser(string i_Input)
		{
			Console.WriteLine(i_Input);
		}
		public string ReadLineFromUser()
		{
			return Console.ReadLine();
		}
		internal string ReadStringFromUserWithMessage(string i_massege)
		{
			string inputFromUser;
			bool isInputValid = false;
			bool startProgram = true;
			do
			{
				if (!startProgram)
				{
					PrintInputInvalidToUser();
				}

				// From now on, for every input of the user, an error message will be printed on screen
				startProgram = false;

				PrintToUser(i_massege);
				inputFromUser = ReadLineFromUser();

				// Check if input is valid, by making sure it's not null, empty or only spaces
				isInputValid = !string.IsNullOrWhiteSpace(inputFromUser);
			}
			while (!isInputValid);
			return inputFromUser;
		}
		internal int ReadIntegerFromUserWithMessage(string i_Message)
		{
			string inputFromUser;
			bool isInputValid = false;
			int valueToReturn = 0;
			PrintToUser(i_Message);
			do
			{
				try
				{
					inputFromUser = ReadLineFromUser();
					valueToReturn = int.Parse(inputFromUser);
					isInputValid = true;
				}
				catch (FormatException)
				{
					isInputValid = false;
					PrintInputInvalidToUser();
				}
			}
			while (!isInputValid);

			return valueToReturn;
		}
		internal float ReadFloatFromUserWithMessage(string i_Message)
		{
			string inputFromUser;
			bool isInputValid = false;
			float valueToReturn = 0;
			PrintToUser(i_Message);
			do
			{
				try
				{
					inputFromUser = ReadLineFromUser();
					valueToReturn = float.Parse(inputFromUser);
					isInputValid = true;
				}
				catch (FormatException)
				{
					isInputValid = false;
					PrintInputInvalidToUser();
				}
			}
			while (!isInputValid);

			return valueToReturn;
		}
		public void PrintInputInvalidToUser()
		{
			PrintToUser("Input invalid!");
		}
		public string GetWheelsManufacturer()
		{
			string printToUser = "Please insert the wheel's manufacturer:";
			string inputFromUser = ReadStringFromUserWithMessage(printToUser);

			return inputFromUser;
		}
		public string GetVehicleModel()
		{
			string printToUser = "Please insert vehicle model:";
			string inputFromUser = ReadStringFromUserWithMessage(printToUser);

			return inputFromUser;
		}
		// Pass by reference because of the exception!
		public void GetVehicleLicensePlate(Garage i_Garage, ref string i_LicensePlate)
		{
			string inputFromUser = ReadStringFromUserWithMessage("Please insert a license plate:");
			i_LicensePlate = inputFromUser;
		}
		public string GetCostumerPhoneNumber()
		{
			string inputFromUser = string.Empty;
			bool isInputValid;
			PrintToUser("Please enter the costumer's phone number(10 digits only):");
			do
			{
				try
				{
					inputFromUser = ReadLineFromUser();
					isInputValid = Costumer.IsCostumerPhoneNumberValid(inputFromUser);
				}
				catch (FormatException)
				{
					isInputValid = false;
					PrintInputInvalidToUser();
				}
				catch (ValueOutOfRangeException voore)
				{
					isInputValid = false;
					PrintToUser(voore.Message);
				}
			}
			while (!isInputValid);
			return inputFromUser;
		}
		public float GetCurrentWheelsAirPressure()
		{
			string requestPressure = "Please enter wheel air pressure:";
			float airPressure = ReadFloatFromUserWithMessage(requestPressure);
			return airPressure;
		}
		private int AnalyzeUserChoice<T>(string i_PrintToUser, T i_Enum)
		{
			string inputFromUser;
			bool isInputValid = true;
			int userChoice = 0;

			PrintToUser(i_PrintToUser);

			do
			{
				if (!isInputValid)
				{
					PrintInputInvalidToUser();
				}

				try
				{
					inputFromUser = ReadLineFromUser();
					userChoice = int.Parse(inputFromUser);
					isInputValid = Enum.IsDefined(typeof(T), userChoice);
				}
				catch (FormatException)
				{
					isInputValid = false;
					PrintInputInvalidToUser();
				}
				catch (ArgumentException)
				{
					isInputValid = false;
					PrintToUser("Input out of range!");
				}
			}
			while (!isInputValid);

			return userChoice;
		}
		public string GetCostumerName()
		{
			string requestName = "Please insert your name:";
			string inputFromUser = ReadStringFromUserWithMessage(requestName);
			return inputFromUser;
		}
		public int GetEnergySourceCapacity()
		{
			string strToPrint = "Please enter engine capacity:";
			int capacity = ReadIntegerFromUserWithMessage(strToPrint);

			return capacity;
		}
		private string buildEnumList(string[] i_enumNames)
		{
			string newLine = Environment.NewLine;

			// Running index in foreach
			int index = 0;

			// Final value to return
			StringBuilder strToReturn = new StringBuilder();

			// Value to return
			foreach (string currentEnum in i_enumNames)
			{
				strToReturn.Append(string.Format("{0}. {1} {2}", index++, currentEnum, newLine));
			}

			return strToReturn.ToString();
		}
		public int GetEnumInput<T>(string i_Message, T i_EnumInput)
		{
			string[] choiceArray = Enum.GetNames(typeof(T));
			Console.Write(Environment.NewLine);
			PrintToUser(i_Message);
			int userChoice = AnalyzeUserChoice(buildEnumList(choiceArray), i_EnumInput);
			return userChoice;
		}
		public eGarageServices PrintMenuAndGetAction()
		{
			eGarageServices chooseService = new eGarageServices();
			int userChoice = GetEnumInput("Menu:", chooseService);
			Console.Clear();
			return (eGarageServices)userChoice;
		}
		public float GetCargoWeight()
		{
			return ReadFloatFromUserWithMessage("Please enter cargo weight:");
		}
		public void PrintList<T>(List<T> i_List)
		{
			foreach (T listObject in i_List)
			{
				PrintToUser(listObject.ToString());
			}
		}
		public bool ReadTrueOrFalseFromUser()
		{
			bool isInputValid = true;
			bool result;
			string inputFromUser;
			string allowedPattern1 = "Yes", allowedPattern2 = "No";

			PrintToUser("Please type yes or no:");
			do
			{
				// It will not print the first time of user input
				if (!isInputValid)
				{
					PrintInputInvalidToUser();
				}

				inputFromUser = ReadLineFromUser();
				isInputValid = isStringEqualsToAllowedPattern(inputFromUser, allowedPattern1, allowedPattern2);
			}
			while (!isInputValid);

			result = ConvertYesOrNoToTrueOrFalse(inputFromUser);
			return result;
		}
		private bool isStringEqualsToAllowedPattern(string i_GivenString, params string[] i_AllowedPatterns)
		{
			bool isStringAllowed = false;
			bool isCurrentStrEquals;
			string lowerCaseAllowedStr;

			// Remove spaces from given string, because "  Yes   " is logically equals to "Yes"
			i_GivenString = i_GivenString.Trim();

			// Check the lower case of given string, because "Yes" is logically equals to "yes" or "YES"
			i_GivenString = i_GivenString.ToLower();

			// Run thru the allowed patterns and compare is to the given string. If there is a match, return true
			foreach (string str in i_AllowedPatterns)
			{
				lowerCaseAllowedStr = str.ToLower();
				isCurrentStrEquals = i_GivenString.Equals(lowerCaseAllowedStr);

				if (isCurrentStrEquals)
				{
					isStringAllowed = true;
					break;
				}
			}
			return isStringAllowed;
		}
		private bool ConvertYesOrNoToTrueOrFalse(string i_GivenString)
		{
			bool isTrueOrFalse;
			bool isStringIsYes;

			isStringIsYes = i_GivenString.Trim().ToLower().Equals("yes");
			if (isStringIsYes)
			{
				isTrueOrFalse = true;
			}
			else
			{
				isTrueOrFalse = false;
			}

			return isTrueOrFalse;
		}
	}
}

