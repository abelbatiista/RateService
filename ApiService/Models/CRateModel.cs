using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiService.Models
{
    public class CRateModel
    {

        public bool Success { get; set; }
        public string Base { get; set; }
        public Rates Rates { get; set; }

    }

    public class Rates
    {

        public double EUR { get; set; }
        public double JPY { get; set; }

    }
}
