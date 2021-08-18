using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public enum eEngineType
        {
            Fuel,
            Electric
        }
        private float m_EnergyLeft;
        private readonly float r_MaxEnergyCapacity;
        private readonly eEngineType r_EngineType;
        public Engine(eEngineType i_type, float r)
        {
            m_EnergyLeft = 0;
        }
        public Engine(float i_MaxEnergyCapacity, eEngineType i_EngineType)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            r_EngineType = i_EngineType;
        }
        public abstract string ShowDetails();
        public abstract void FillEngine(float i_HoursToAdd);
        public float EnergyLeft
        {
            get 
            {
                return m_EnergyLeft;
             }
            set
            {
                m_EnergyLeft = value;
            }
        }
        public float MaxEnergyCapacity => r_MaxEnergyCapacity;
        public eEngineType EngineType => r_EngineType;
    }
    
}
