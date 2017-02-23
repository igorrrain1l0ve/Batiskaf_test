using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Batiskaf.Controllers.DataSource;
using Batiskaf.Models;
using Batiskaf.Models.State.Linear;

namespace Batiskaf.Controllers
{
    class INController
    {
        private INJoystick joystick;
        private INDataSourceAdapter dataSource;
        private int joystickPort = 5001;
        private int maxCount = 20;
        private int count;

        private List<INLinearAcceleration> accelerationValues;
        public INController()
        {
            this.dataSource = new INDataSourceAdapter(this.joystickPort, this.addNewValue);

            this.joystick = new INJoystick();


        }

        private void addNewValue(INLinearAcceleration acceleration)
        {
            count++;
            this.accelerationValues.Add(acceleration);

            if (count == maxCount)
            {
                this.joystick.state = AccelerationHandler.getNewState(accelerationValues, joystick.state);
                this.accelerationValues = new List<INLinearAcceleration>();
            }
        }
    }
}
