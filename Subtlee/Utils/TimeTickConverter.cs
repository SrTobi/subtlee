using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Subtlee.Utils
{
	[ValueConversion(typeof(TimeSpan), typeof(long))]
	public class TimeTickConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object _value, Type _targetType, object _parameter,
			System.Globalization.CultureInfo _culture)
		{
			if (_value == null || _value.GetType() != typeof (TimeSpan))
			{
				throw new ArgumentException("_value");
			}

			return ((TimeSpan)(_value)).Ticks;
		}

		public object ConvertBack(object _value, Type _targetType, object _parameter,
			System.Globalization.CultureInfo _culture)
		{
			if (_value == null)
			{
				throw new ArgumentException("_value");
			}

			return new TimeSpan(System.Convert.ToInt64(_value));
		}

		#endregion
	}
}
