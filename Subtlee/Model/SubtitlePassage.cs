using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtlee.Model
{
	class SubtitlePassage : ISubtitlePassage
	{
		private readonly TimeSpan mBegin;
		private readonly TimeSpan mEnd;
		private readonly string mText;

		public TimeSpan Begin { get { return mBegin; } }
		public TimeSpan End { get { return mEnd; }}
		public TimeSpan Length { get { return End - Begin; } }
		public string Text { get { return mText; } }

		public SubtitlePassage(string _text, TimeSpan _begin, TimeSpan _end)
		{
			this.mText = _text;
			this.mBegin = _begin;
			this.mEnd = _end;
		}
	}
}
