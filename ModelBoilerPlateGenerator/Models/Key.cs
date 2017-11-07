using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModelBoilerPlateGenerator.Models
{
    public class Key
    {
        protected List<Property> properties;

        [XmlArray("Properties")]
        public List<Property> Properties { get { return this.properties; } set { this.properties = value; } }

        public Key()
        {
            this.properties = new List<Property>();
        }
    }
}
