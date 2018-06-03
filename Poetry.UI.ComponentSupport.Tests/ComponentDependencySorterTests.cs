using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentDependencySorterTests
    {
        [Fact]
        public void AsIs()
        {
            var input = new List<Component>
            {
                new Component("a", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("b", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("c", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("d", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
            };

            var expected = new List<string>
            {
                "a",
                "b",
                "c",
                "d",
            };

            Assert.Equal(expected, new ComponentDependencySorter().Sort(input).Select(c => c.Id));
        }

        [Fact]
        public void AsIsWithOneDependency()
        {
            var input = new List<Component>
            {
                new Component("a", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("b", null, new List<string>{ "d" }, Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("c", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("d", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
            };

            var expected = new List<string>
            {
                "a",
                "d",
                "b",
                "c",
            };

            Assert.Equal(expected, new ComponentDependencySorter().Sort(input).Select(c => c.Id));
        }

        [Fact]
        public void AsIsWithOneDependencyAtSafePosition()
        {
            var input = new List<Component>
            {
                new Component("a", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("b", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("c", null, new List<string>{ "b" }, Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("d", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
            };

            var expected = new List<string>
            {
                "a",
                "b",
                "c",
                "d",
            };

            Assert.Equal(expected, new ComponentDependencySorter().Sort(input).Select(c => c.Id));
        }

        [Fact]
        public void AsIsWithMultipleDependencies()
        {
            var input = new List<Component>
            {
                new Component("a", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("b", null, new List<string>{ "d", "c" }, Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("c", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("d", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
            };

            var expected = new List<string>
            {
                "a",
                "c",
                "d",
                "b",
            };

            Assert.Equal(expected, new ComponentDependencySorter().Sort(input).Select(c => c.Id));
        }

        [Fact]
        public void AsIsWithChainedDependencies()
        {
            var input = new List<Component>
            {
                new Component("a", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("b", null, new List<string>{ "c" }, Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("c", null, new List<string>{ "d" }, Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
                new Component("d", null, Enumerable.Empty<string>(), Enumerable.Empty<Controller>(), Enumerable.Empty<Script>(), Enumerable.Empty<Style>()),
            };

            var expected = new List<string>
            {
                "a",
                "d",
                "c",
                "b",
            };

            Assert.Equal(expected, new ComponentDependencySorter().Sort(input).Select(c => c.Id));
        }
    }
}
