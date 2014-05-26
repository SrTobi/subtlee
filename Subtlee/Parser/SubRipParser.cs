using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Subtlee.Model;

namespace Subtlee.Parser
{
	class SubRipParser : ISubtitleParser
	{
		private readonly Regex mTimeFormat ;

		public string Name { get { return "TimedText"; } }

		public IEnumerable<string> Formats { get { return new string[] {".srt"}; } }


		public SubRipParser()
		{
			 mTimeFormat = new Regex(@"(?<start_hour>\d+)\:(?<start_min>\d+)\:(?<start_sec>\d+)\,(?<start_milli>\d+)\s+\-+\>\s+(?<end_hour>\d+)\:(?<end_min>\d+)\:(?<end_sec>\d+)\,(?<end_milli>\d+).*");
		}


		public ISubtitleData ParseSubtitle(Stream _data)
		{
			var subtutitle = new SubtitleData("", "SubRip");

			using (var reader = new StreamReader(_data))
			{
				while (reader.Peek() >= 0)
				{
					// Skip empty lines
					string line = null;
					while (line == null)
					{
						line = reader.ReadLine();
						if (line == null)
						{
							return subtutitle;
						}
						if (string.IsNullOrEmpty(line))
							line = null;
					}


					TimeSpan begin;
					TimeSpan end;

					_parseNumber(line);
					_parseTimeCode(reader.ReadLine(), out begin, out end);
					string content = _parseContent(reader);

					subtutitle.AddPassage(new SubtitlePassage(content, begin, end));
				}
			}

			return subtutitle;
		}

		private void _parseNumber(string _line)
		{
			
		}

		private void _parseTimeCode(string _line, out TimeSpan _begin, out TimeSpan _end)
		{
			Match match = mTimeFormat.Match(_line);

			_begin = new TimeSpan(	0,
									Convert.ToInt32(match.Groups["start_hour"].Value),
									Convert.ToInt32(match.Groups["start_min"].Value),
									Convert.ToInt32(match.Groups["start_sec"].Value),
									Convert.ToInt32(match.Groups["start_milli"].Value));

			_end = new TimeSpan(0,
								Convert.ToInt32(match.Groups["end_hour"].Value),
								Convert.ToInt32(match.Groups["end_min"].Value),
								Convert.ToInt32(match.Groups["end_sec"].Value),
								Convert.ToInt32(match.Groups["end_milli"].Value));
		}

		private string _parseContent(StreamReader _reader)
		{
			return String.Join("\n", _enumContent(_reader));
		}

		private IEnumerable<string> _enumContent(StreamReader _reader)
		{
			string line;
			while (!string.IsNullOrEmpty(line = _reader.ReadLine()))
			{
				yield return line;
			}
		}
	}
}
