using System;
using System.Collections.Generic;
using System.Linq;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Aspects.Orm.Testing.Checks.Update
{
    /// <summary>
    /// Update dictionary values check
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdateDictionaryCheck<T> : CommandCheck<Commands.Update.Update>
    {
        private readonly Dictionary<string, object> _expectedValues;
        private readonly string _explanation;

        internal UpdateDictionaryCheck(Dictionary<string, object> expectedValues, string explanation)
        {
            _explanation = explanation;
            _expectedValues = expectedValues;
        }

        /// <inheritdoc />
        protected override string GetMessage(Commands.Update.Update command)
        {
            if (command == null) return $"expected updated entity {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected updated entity of type {typeof(T).Name} and {_explanation}, but got one of {command.EntityType.Name}";
            }
            List<(string, object, object)> propExpectedActual = new List<(string, object, object)>();
            var actual = command.UpdateValuesStringKeys;
            foreach (var kv in _expectedValues)
            {

                if (!actual.ContainsKey(kv.Key))
                {
                    propExpectedActual.Add((kv.Key, kv.Value, null));
                }

                var v = actual[kv.Key];
                if (!Equals(v, kv.Value))
                {
                    propExpectedActual.Add((kv.Key, kv.Value, v));
                }
            }

            var propsText = string.Join(", ",
                propExpectedActual.Select(x => $"{x.Item1}: '{x.Item2}' expected, '{x.Item3}' got"));

            if (string.IsNullOrEmpty(_explanation)) return $"updated {typeof(T).Name} does not meet conditions: {propsText}";
            return $"update '{_explanation}' does not satisfy conditions: {propsText}";
        }

        /// <inheritdoc />
        protected override bool IsActuallyValid(Commands.Update.Update effect)
        {
            if (effect == null) return false;
            if (effect.EntityType != typeof(T)) return false;
            var actual = effect.UpdateValuesStringKeys;
            foreach (var kv in _expectedValues)
            {

                if (!actual.ContainsKey(kv.Key))
                {
                    return false;
                }

                var v = actual[kv.Key];
                if (!Equals(v, kv.Value)) return false;
            }
            return true;
        }
    }
}
