using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJD
{
    public class Device
    {
        public string DeviceUniqueName { get; set; }
        public string DeviceFriendlyName { get; set; }

        public override string ToString()
        {
            return DeviceFriendlyName;
        }
    }
}
