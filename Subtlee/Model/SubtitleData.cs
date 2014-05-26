using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtlee.Model
{
	class SubtitleData : ISubtitleData
	{
		private readonly string mFormat;
		private readonly string mName;
		private TimeSpan mLength = new TimeSpan();
		private readonly List<ISubtitlePassage> mPassages = new List<ISubtitlePassage>(); 

		public string Name { get { return mName; } }
		public string Format { get { return mFormat; } }
		public TimeSpan Length { get { return mLength; } }
		public ISubtitlePassage PassageAt(TimeSpan _time)
		{
			int left = 0;
			int right = mPassages.Count;

			while (left <= right)
			{
				int index = (left + right)/2;
				ISubtitlePassage e = mPassages[index];

				if (e.Begin <= _time && _time < e.End)
				{
					return e;
				}
				else
				{
					if (e.Begin > _time)
						right = index - 1;
					else /* (e.Begin < _tim) */
						left = index + 1;
				}
			}

			return null;
		}

		public IEnumerable<ISubtitlePassage> Passages { get { return mPassages;  } }

		public SubtitleData(string _name, string _format)
		{
			this.mName = _name;
			this.mFormat = _format;
		}

		public void AddPassage(ISubtitlePassage _passage)
		{
			if (mLength <= _passage.End)
			{
				mLength = _passage.End;
				mPassages.Add(_passage);
			}
			else
			{
				for (int i = 0; i < mPassages.Count; ++i)
				{
					if (_passage.Begin <= mPassages[i].Begin)
					{
						mPassages.Insert(i, _passage);
						return;
					}
				}

				throw new ArgumentException("_passage");
			}
		}
	}
}
