using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModelBoilerPlateGenerator.Models
{
    public class Model
    {
        protected Key key;
        protected string name;
        protected List<Property> properties;

        public Key Key { get { return this.key; } set { this.key = value; } }
        [XmlAttribute]
        public string Name { get { return this.name; } set { this.name = value; } }
        [XmlArray("Properties")]
        public List<Property> Properties { get { return this.properties; } set { this.properties = value; } }

        public Model()
            : this(null)
        {
        }

        public Model(Key key)
        {
            this.key = key;
            this.properties = new List<Property>();
        }
    }
}
