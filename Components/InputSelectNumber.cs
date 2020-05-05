using System;
using Microsoft.AspNetCore.Components.Forms;

namespace SkoorCard.Components
{
	public class InputSelectNumber<T> : InputSelect<T> {
		protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage) {
			if (typeof(T) == typeof(int)) {
				if (int.TryParse(value, out var resultInt)) {
					result = (T)(object) resultInt;
					validationErrorMessage = null;
					return true;
				} else {
					result = default;
					validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
					return false;
				}
			} else {
				throw new InvalidOperationException($"{GetType()} does not support the type {typeof(T)}.");
			}
		}
	}
}