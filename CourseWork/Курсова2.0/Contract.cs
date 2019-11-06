using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Курсова2._0
{
    [Serializable] public class Contract
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private string adress;
        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double tax;
        public double Tax
        {
            get { return tax; }
            set { tax = value; }
        }


    }
}
