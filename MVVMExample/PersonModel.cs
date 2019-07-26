using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMExample
{
    public class PersonModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }

        public bool Selected { get; set; }

        public int ID { get; set; }

        public override string ToString()
        {
            return String.Format("[Name: {0}  | Age: {1}  | Job: {2}  | Selected: {3} ]",
                Name, Age, Job, Selected);
        }



    }
}
