using System;
using System.Windows;
using System.Windows.Data;

namespace Subtlee.Utils
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BooleanVisibilityConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object _value, Type _targetType, object _parameter,
			System.Globalization.CultureInfo _culture)
		{
			var value = (bool) _value;
			if (_parameter != null && object.Equals(_parameter, "invert"))
			{
				value = !value;
			}
			return value ? Visibility.Visible : Visibility.Hidden;
		}

		public object ConvertBack(object _value, Type _targetType, object _parameter,
			System.Globalization.CultureInfo _culture)
		{
			return Convert(_value, _targetType, _parameter, _culture);
		}

		#endregion
	}
}
