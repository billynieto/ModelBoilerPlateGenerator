using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModelBoilerPlateGenerator.Models
{
    public class Enumeration
    {
        protected List<EnumerationItem> items;
        protected string name;

        [XmlArray("Items")]
        public List<EnumerationItem> Items { get { return this.items; } set { this.items = value; } }
        [XmlAttribute]
        public string Name { get { return this.name; } set { this.name = value; } }

        public Enumeration()
        {
            this.items = new List<EnumerationItem>();
        }
    }

    public class EnumerationItem
    {
        protected string name;
        protected int _value;

        [XmlAttribute]
        public string Name { get { return this.name; } set { this.name = value; } }
        [XmlAttribute]
        public int Value { get { return this._value; } set { this._value = value; } }

        public EnumerationItem()
        {
        }

        public EnumerationItem(string name, int value)
        {
            this.name = name;
            this._value = value;
        }
    }
}
