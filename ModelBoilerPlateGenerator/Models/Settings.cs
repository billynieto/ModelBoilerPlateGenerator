using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ModelBoilerPlateGenerator.Models
{
    public class Settings
    {
        protected string frameworkProjectName;
        protected string integrationTestsProjectName;
        protected string modelsProjectName;
        protected string repositoriesProjectName;
        protected string testProjectName;
        protected string unitTestsProjectName;

        public string FrameworkProjectName { get { return this.frameworkProjectName; } set { this.frameworkProjectName = value; } }
        public string IntegrationTestsProjectName { get { return this.integrationTestsProjectName; } set { this.integrationTestsProjectName = value; } }
        public string ModelsProjectName { get { return this.modelsProjectName; } set { this.modelsProjectName = value; } }
        public string RepositoriesProjectName { get { return this.repositoriesProjectName; } set { this.repositoriesProjectName = value; } }
        public string TestProjectName { get { return this.testProjectName; } set { this.testProjectName = value; } }
        public string UnitTestsProjectName { get { return this.unitTestsProjectName; } set { this.unitTestsProjectName = value; } }
        
        public Settings()
        {
        }
    }
}
