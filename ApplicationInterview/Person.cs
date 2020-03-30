using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInterview
{
    public class Person
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Score { get; set; }

        public Person(string name, string country, string city, string score)
        {
            Name = name;
            Country = country;
            City = city;
            Score = score;
        }

    }
}
