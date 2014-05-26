using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace Subtlee.Utils
{
	[ValueConversion(typeof(string), typeof(List<Inline>))]
	class StyledStringToInlineConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string styledText = value as string;

			if (styledText == null)
			{
				throw new ArgumentException("value is not a string");
			}

			var inlines = new List<Inline>();

			Regex reg = new Regex(@"(\<.\>)|(\<\/.\>)");
			var parts = reg.Split(styledText);

			int bold = 0;
			int italic = 0;
			foreach (var token in parts)
			{
				switch (token)
				{
					case "<b>":
						++bold;
						break;
					case "</b>":
						--bold;
						break;
					case "<i>":
						++italic;
						break;
					case "</i>":
						--italic;
						break;
					default:
						var r = new Run(token);
						if (bold > 0)
							r.FontWeight = FontWeights.Bold;
						if (italic > 0)
							r.FontStyle = FontStyles.Italic;


						inlines.Add(r);
						break;
				}
			}

			return inlines;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
