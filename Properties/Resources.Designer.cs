﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CodeBuilder.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CodeBuilder.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to public class [Code] : IDesignBrief
        ///    {
        ///
        ///        // Properties
        ///
        ///        public List&lt;Section&gt; Sections { get; set; }
        ///        public List&lt;Summary&gt; Summary { get; set; }
        ///        public string Title =&gt; [Name]
        ///
        ///        // Fields
        ///
        ///        [Fields]
        ///
        ///        // Constructors
        ///
        ///        public [Name]([Inputs])
        ///        {
        ///            [InitializeFields]
        ///
        ///            Summary = new List&lt;Summary&gt;();
        ///            Sections = new List&lt;Section&gt;();
        ///        }
        ///
        ///        // Methods 
        ///
        ///        public  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string BriefClassTemplate {
            get {
                return ResourceManager.GetString("BriefClassTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to class [Name] : ICalculation
        ///    {
        ///
        ///        // Fields
        ///
        ///        [Fields]
        ///
        ///        // Constructors
        ///
        ///        public [Name]([Variables])
        ///        {
        ///            [InitializeFields]
        ///            
        ///            Calculate();
        ///        }
        ///
        ///        // Methods
        ///
        ///        public void Calculate()
        ///        {
        ///            [CodeCalculation]
        ///        }
        ///
        ///        // Properties
        ///
        ///        public string Description =&gt; [Description]
        ///
        ///        public double Value =&gt; [Code]
        ///
        ///        public string Calculation =&gt; [MathCa [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CalculationClassTemplate {
            get {
                return ResourceManager.GetString("CalculationClassTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to class [Name]
        ///    {
        ///
        ///        // Properties
        ///
        ///        public List&lt;Summary&gt; Summary { get; set; }
        ///
        ///        [Properties]
        ///
        ///        // Fields
        ///
        ///        [Fields]
        ///        
        ///        private bool hasValues;
        ///
        ///        // Constructors
        ///
        ///        public [Name]([Inputs])
        ///        {
        ///            Summary = new List&lt;Summary&gt;();
        ///            
        ///            [InitializeFields]
        ///
        ///            Calculate();
        ///        }
        ///
        ///        // Method: calculate values
        ///
        ///        private void Calculate()
        ///        {
        ///            [Cal [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CheckClassTemplate {
            get {
                return ResourceManager.GetString("CheckClassTemplate", resourceCulture);
            }
        }
    }
}
