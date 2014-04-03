using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniTiled {

	public class Tileset {

		public string Name { get; set; }
		public Sprite Image { get; set; }
		public Vector2 TileSize { get; set; }
		public Vector2 Spacing { get; set; }
		public float Margin { get; set; }
		public float Grid { get; set; }
		public int FirstGid { get; set; }
		public int LastGid { get; set; }

		public Tileset(string name, 
						Vector3 size,
						int first,
						int last = 0) {

			Name = name;
			TileSize = size;
			// @TODO calculate first and last
			FirstGid = first == 1 ? 0 : first;
			LastGid = last;

		}

		public bool HasTileGid(int gid) {

			if (LastGid != 0) {
				return (gid >= FirstGid && gid <= LastGid);
			}

			return (gid >= FirstGid);

		}
		
	}

}