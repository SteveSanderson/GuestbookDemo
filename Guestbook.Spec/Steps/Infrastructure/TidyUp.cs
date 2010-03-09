using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DeleporterCore.Client;
using TechTalk.SpecFlow;

namespace Guestbook.Spec.Steps.Infrastructure
{
    /// <summary>
    /// Store the actions in a dictionary keyed by a CallContext-specific Guid
    /// The test appdomain will set a new guid at the start of each scenario
    /// </summary>
    [Binding]
    public static class TidyUp
    {
        private const string CallContextKey = "__tidyUp_scenarioId";
        private static readonly Dictionary<Guid, IList<Action>> TaskStores = new Dictionary<Guid, IList<Action>>();
        private static Guid ScenarioGuid
        {
            get { return (Guid) CallContext.LogicalGetData(CallContextKey); }
            set { CallContext.LogicalSetData(CallContextKey, value);}
        }

        public static IEnumerable<Action> Tasks
        {
            get {
                if (!TaskStores.ContainsKey(ScenarioGuid))
                    TaskStores[ScenarioGuid] = new List<Action>();
                return TaskStores[ScenarioGuid];
            }
        }

        public static void AddTask(Action task)
        {
            ((List<Action>) Tasks).Add(task);
        }

        [BeforeScenario]
        public static void Prep()
        {
            ScenarioGuid = Guid.NewGuid();
            AddTask(() => Deleporter.Run(PerformTidyUp));
        }

        [AfterScenario]
        public static void PerformTidyUp()
        {
            try {
                var exceptions = new List<Exception>();
                foreach (var task in Tasks) {
                    try { task(); }
                    catch (Exception ex) { exceptions.Add(ex); }
                }
                if (exceptions.Count == 1)
                    throw exceptions[0];
                else if (exceptions.Count > 1)
                    throw new MultiException(exceptions);
            }
            finally {
                TaskStores.Remove(ScenarioGuid);
            }
        }

        public class MultiException : Exception
        {
            protected IEnumerable<Exception> InnerExceptions { get; private set; }

            public MultiException(IEnumerable<Exception> exceptions)
            {
                InnerExceptions = exceptions;
            }
        }
    }
}