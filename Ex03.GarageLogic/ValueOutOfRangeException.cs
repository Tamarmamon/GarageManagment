using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
	public class ValueOutOfRangeException : Exception
	{
		// Fields
		private float m_MaxValue;
		private float m_MinValue;

		// Constructor
		public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
		: base(string.Format("Out of range! Value should be between {0} to {1}", i_MinValue, i_MaxValue))
		{
			m_MaxValue = i_MaxValue;
			m_MinValue = i_MinValue;
		}
		public float MaxValue
		{
			get
			{
				return m_MaxValue;
			}

			set
			{
				m_MaxValue = value;
			}
		}
		public float MinValue
		{
			get
			{
				return m_MinValue;
			}

			set
			{
				m_MinValue = value;
			}
		}
	}
}
