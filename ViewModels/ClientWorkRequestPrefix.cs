using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.ViewModels
{
    public class ClientWorkRequestPrefix
    {
        public string IdPrefix { get; set; }
        public string BusinessArea { get; set; }

        public string Description
        {
            get { return IdPrefix + " --- " + BusinessArea; }
        }
    }
}
