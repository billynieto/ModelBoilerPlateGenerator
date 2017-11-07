//------------------------------------------------------------------------------
// <copyright file="ModelBoilerPlateVisualStudioWindow.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using ModelBoilerPlateGenerator.ViewModels;

namespace ModelBoilerPlateGenerator
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("a4fbfbe9-e34d-4911-8ee0-a2f974b26add")]
    public class ModelBoilerPlateVisualStudioWindow : ToolWindowPane
    {
        protected ModelBoilerPlateViewModel modelBoilerPlateViewModel;
        protected VisualStudioWorkspace workspace;

        public VisualStudioWorkspace Workspace { get { return this.workspace; } set { this.workspace = value;  this.Content = new ModelBoilerPlateVisualStudioWindowControl(this.workspace, this.modelBoilerPlateViewModel); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBoilerPlateVisualStudioWindow"/> class.
        /// </summary>
        public ModelBoilerPlateVisualStudioWindow()
            : base(null)
        {
            this.Caption = "Model Boiler Plate";

            this.modelBoilerPlateViewModel = new ModelBoilerPlateViewModel(new Models.Specification());
            
            IComponentModel componentModel = (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));
            Workspace = componentModel.GetService<VisualStudioWorkspace>();
        }
    }
}
