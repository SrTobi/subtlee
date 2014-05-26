using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subtlee.Model;

namespace Subtlee.Parser
{
	interface ISubtitleParser
	{
		string Name { get; }

		IEnumerable<string> Formats { get; }

		ISubtitleData ParseSubtitle(Stream _data);
	}
}
