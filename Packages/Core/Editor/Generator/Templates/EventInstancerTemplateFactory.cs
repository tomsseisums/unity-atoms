using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;

namespace UnityAtoms.Editor
{
    [InitializeOnLoad]
    public static class EventInstancerTemplateFactory
    {
        public const string atomName = "Event Instancer";

        static EventInstancerTemplateFactory()
        {
            AtomGenerator.populators += AtomPopulator;
            AtomGenerator.evaluators += AtomEvaluator;
        }

        private static void AtomPopulator(Type generatedType, Dictionary<string, Template> templates)
        {
            var atomTemplate = GetTemplate(generatedType);
            templates.Add(atomName, atomTemplate);

            var atomPairTemplate = PairTemplateFactory.GetTemplate(generatedType, GetTemplate);
            templates.Add($"{atomName} {PairTemplateFactory.atomName}", atomPairTemplate);
        }

        private static void AtomEvaluator(Type generatedType, ReadOnlyDictionary<string, Generator.Solver> solvers)
        {
            if (!solvers[EventTemplateFactory.atomName].option)
            {
                solvers[atomName].option = false;
            }

            if (!solvers[$"{EventTemplateFactory.atomName} {PairTemplateFactory.atomName}"].option)
            {
                solvers[$"{atomName} {PairTemplateFactory.atomName}"].option = false;
            }
        }

        public static Template GetTemplate(Type type) => AtomTemplateFactory.GetComponentTemplate(typeof(AtomEventInstancer<>), type, atomName, "atom-icon-sign-blue");
    }
}
