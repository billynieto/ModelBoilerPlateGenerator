using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModelBoilerPlateGenerator.Models
{
    public class Property
    {
        protected string name;
        protected string propertyType;
        
        [XmlAttribute]
        public string Name { get { return this.name; } set { this.name = value; } }
        [XmlAttribute]
        public string PropertyType { get { return this.propertyType; } set { this.propertyType = value; } }
    }
}
