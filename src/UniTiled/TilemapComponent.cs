using UnityEngine;
using System.Collections;
using UniTiled;

namespace UniTiled {

	[AddComponentMenu("Tiled/Tilemap")]
	public class TilemapComponent : MonoBehaviour {

		// @TODO List os prefabs for tiles
		public TextAsset mapAsset;
		public Tilemap tilemap;
		
		public void LoadTilemap() {

			tilemap = new Tilemap(gameObject);
			tilemap.Load(mapAsset);
			tilemap.Build();

		}
		
	}

}