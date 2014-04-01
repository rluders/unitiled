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

		public Tileset(string name, Vector3 size) {

			Name = name;
			TileSize = size;

		}
		
	}

}