using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilesGame_CTIN
{
	public class Tile
	{
		public int id { get; set; }
		public string imagePath { get;set; }
		public Tile(int i,string imagepth) 
		{
			id = i;
			imagePath = imagepth;
		}
		
		
	}
}
