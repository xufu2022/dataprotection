using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmDataProtectionTest
{
    public class Sample<T> where T : System.Enum
    {

        public string GetName()
        {
            return "DataProtectionKey_" + Enum.GetValues(typeof(T));
        }
    }

    public enum Items
    {
        None,
        A,
        B,
        C
    }
}
