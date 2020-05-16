using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.ViewModels
{
    public class WorkflowViewModel : WorkflowScheme
    {
        public string DescriptionDisplay
        {
            get 
            {
                return Code + " --- " + Description;
            }
        }
    }
}
