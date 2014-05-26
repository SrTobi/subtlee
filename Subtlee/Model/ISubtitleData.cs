using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtlee.Model
{
	public interface ISubtitlePassage
	{
		TimeSpan Begin { get; }

		TimeSpan End { get; }

		TimeSpan Length { get; }

		string Text { get; }
	}

	public interface ISubtitleData
	{
		string Name { get; }

		string Format { get; }

		TimeSpan Length { get; }

		ISubtitlePassage PassageAt(TimeSpan _time);
	}
}
