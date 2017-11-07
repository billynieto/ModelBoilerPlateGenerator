using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModelBoilerPlateGenerator.Models
{
    public class Specification
    {
        protected List<Enumeration> enumerations;
        protected List<Model> models;
        protected Settings settings;

        public List<Enumeration> Enumerations { get { return this.enumerations; } set { this.enumerations = value; } }
        public List<Model> Models { get { return this.models; } set { this.models = value; } }
        public Settings Settings { get { return this.settings; } set { this.settings = value; } }

        public Specification()
            : this(null)
        {
        }

        public Specification(string solutionName)
        {
            this.enumerations = new List<Enumeration>();
            this.models = new List<Model>();
            this.settings = new Models.Settings();
        }
    }
}
