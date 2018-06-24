﻿using Poetry.UI.ControllerSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.ResourceSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    /// <summary>
    /// Represents a Poetry.UI component.
    /// 
    /// Note: Not to be inherited by your component classes as they should be POCOs, annotated with the [Component] attribute.
    /// </summary>
    public class Component
    {
        public string Id { get; }
        public AssemblyWrapper Assembly { get; }
        public IEnumerable<string> Dependencies { get; }
        public IEnumerable<Controller> Controllers { get; }
        public IEnumerable<Script> Scripts { get; }
        public IEnumerable<Style> Styles { get; }
        public IEnumerable<Resource> Resources { get; }

        public Component(string id, AssemblyWrapper assembly, IEnumerable<string> dependencies, IEnumerable<Controller> controllers, IEnumerable<Script> scripts, IEnumerable<Style> styles, IEnumerable<Resource> resources)
        {
            Id = id;
            Assembly = assembly;
            Dependencies = dependencies.ToList().AsReadOnly();
            Controllers = controllers.ToList().AsReadOnly();
            Scripts = scripts.ToList().AsReadOnly();
            Styles = styles.ToList().AsReadOnly();
            Resources = resources.ToList().AsReadOnly();
        }
    }
}
