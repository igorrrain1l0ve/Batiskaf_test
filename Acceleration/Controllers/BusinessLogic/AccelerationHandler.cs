using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Batiskaf.Models.State;
using Batiskaf.Models.State.Linear;

namespace Batiskaf.Controllers
{
    class AccelerationHandler
    {
        public static double integrate(double[] f, double dx, int countOfSteps)
        {
            double I = 0;
            for (int i = 2; i < countOfSteps; i += 2)
            {
                I += f[i] + 4 * f[i - 1] + f[i - 2];
            }
            I *= dx / 3;
            if (countOfSteps % 2 == 0)
            {
                I += ((f[countOfSteps - 1] + f[countOfSteps - 2]) * dx) / 2;
            }
            return I;
        }

        public static double velocity(double[] acceleration, double deltaTime, int countOfSteps, double zeroVelocity)
        {
            double v = zeroVelocity;

            v += integrate(acceleration, deltaTime, countOfSteps);

            return v;
        }

        public static double getNewDestination(double[] acceleration, double deltaTime, double zeroDestination, double zeroVelocity)
        {
            int n = acceleration.Count();
            double x = zeroDestination + (deltaTime * (n - 1)) * zeroVelocity;

            double[] v = new double[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = velocity(acceleration, deltaTime, i + 1, zeroVelocity);
                //Console.WriteLine("V{0} = {1}", i, v[i]);
            }
            x += integrate(v, deltaTime, n);

            return x;
        }


        public static INState getNewState(INLinearAcceleration[] accelerationArray, INState lastState)
        {
            INState newState;

            int size = accelerationArray.Count();
            double[] aX = new double[size];
            double[] aY = new double[size];
            double[] aZ = new double[size];

            for (int i = 0; i < size; i++)
            {
                aX[i] = accelerationArray[i].aX;
                aY[i] = accelerationArray[i].aY;
                aZ[i] = accelerationArray[i].aZ;
            }
            
            double x = getNewDestination

            return newState;
        }
    }
}
