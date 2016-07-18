using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    class IdGenerator
    {
        private static IdGenerator m_instance = null;
        int m_nextId = 0;
    
        private IdGenerator()
        {
        }

        public static IdGenerator getInstance()
        {
            if (m_instance == null)
                m_instance = new IdGenerator();
            return m_instance;
        }

        public int GetNextId()
        {
           return  ++m_nextId;
        }

    }
}
