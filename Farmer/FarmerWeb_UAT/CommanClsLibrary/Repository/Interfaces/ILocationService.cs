using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommanClsLibrary.Repository.Interfaces
{
    interface ILocationService
    {
        double CalculateDistance(double sLatitude, double sLongitude, double eLatitude, double eLongitude);
    }
}
