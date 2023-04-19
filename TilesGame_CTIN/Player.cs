using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilesGame_CTIN
{
	public class Player
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public int Level { get; set; }
		public int TimesHeLost { get; set; }
		public int Clicked { get; set; }

		public Player(string name, string password, int level, int timesHeLost, int clicked)
		{
			Name = name;
			Image = password;
			Level = level;
			TimesHeLost = timesHeLost;
			Clicked = clicked;
		}
	}
}
