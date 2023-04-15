using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOglasiSource.Model.Advertisement
{
    public class VehicleBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VehicleModel> Models { get; set; }
    }
}
