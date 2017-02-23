using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Batiskaf.Models;
using Batiskaf.Models.State.Linear;
using Batiskaf.Models.State.Angular;

namespace Batiskaf.Controllers.DataSource
{
    interface INDataSourceInterface
    {
        INLinearAcceleration getLinearAcceleration();
        INAngularVelocity getAngularVelocity();
    }
}
