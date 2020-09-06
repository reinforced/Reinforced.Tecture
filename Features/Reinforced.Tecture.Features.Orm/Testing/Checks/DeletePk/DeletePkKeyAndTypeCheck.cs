using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Testing.Validation;

namespace Reinforced.Tecture.Features.Orm.Testing.Checks.DeletePk
{
    public class DeletePkKeyAndTypeCheck<T> : CommandCheck<Commands.DeletePk.DeletePk>
    {
        private readonly object[] _primaryKeyObject;
        private readonly string _explanation;
        internal DeletePkKeyAndTypeCheck(object[] primaryKeyObject, string explanation)
        {
            _primaryKeyObject = primaryKeyObject;
            _explanation = explanation;
        }

        /// <inheritdoc />
        protected override string GetMessage(Commands.DeletePk.DeletePk command)
        {
            if (command == null) return $"expected delete by PK for {_explanation}, but story unexpectedly ends";
            if (command.EntityType != typeof(T))
            {
                return
                    $"expected deleted by PK entity of type {typeof(T).Name} for {_explanation}, but got one of {command.EntityType.Name}";
            }

            if (command.KeyValues.Length != _primaryKeyObject.Length)
                return $"expected delete by PK for {_explanation} with key consisting of {_primaryKeyObject.Length} fields, seems that key consists of {command.KeyValues.Length} fields";

            for (int i = 0; i < _primaryKeyObject.Length; i++)
            {
                if (!object.Equals(_primaryKeyObject[i], command.KeyValues[i]))
                {
                    if (_primaryKeyObject.Length == 1)
                    {
                        return $"key of removed {typeof(T).Name} must be '{_primaryKeyObject[i]}', but actually is '{command.KeyValues[i]}'";
                    }
                    else
                    {
                        return $"key field #{i} of removed {typeof(T).Name} must be '{_primaryKeyObject[i]}', but actually is '{command.KeyValues[i]}'";
                    }
                }
                    
            }

            return $"expected delete by PK for {_explanation}, but something went wrong";
        }

        /// <inheritdoc />
        protected override bool IsActuallyValid(Commands.DeletePk.DeletePk command)
        {
            if (command == null) return false;
            if (command.EntityType != typeof(T)) return false;
            if (_primaryKeyObject.Length != command.KeyValues.Length) return false;
            for (int i = 0; i < _primaryKeyObject.Length; i++)
            {
                if (!object.Equals(_primaryKeyObject[i], command.KeyValues[i])) return false;
            }
            return true;
        }
    }
}
