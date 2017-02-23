using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batiskaf.Models.State.Linear
{
    class INLinearAcceleration
    {
        public double aX;
        public double aY;
        public double aZ;

        public INLinearAcceleration(double aX, double aY, double aZ)
        {
            this.aX = aX;
            this.aY = aY;
            this.aZ = aZ;
        }
    }
}
