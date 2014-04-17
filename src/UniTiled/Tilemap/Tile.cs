using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UniTiled {

	// @todo optimize this class
	public class Tile {

		GameObject tileObject;

		public int Gid { get; set; }

		public Tile(int gid) {

			Gid = gid;
			tileObject = new GameObject("tile_" + gid);

			// if it have no texture so we destroy the tile
			if (!LoadTexture()) {
				// @todo make it better
				GameObject.DestroyImmediate(tileObject);
				tileObject = null;
			}

		}

		protected bool LoadTexture() {

			Tileset tileset = Tilemap.Tilesets.GetFromGid(Gid);

			tileObject.AddComponent("SpriteRenderer");
			SpriteRenderer sr = (SpriteRenderer)tileObject.GetComponent("SpriteRenderer");
			
			Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/Tiles/" + tileset.Name);
			string[] names = new string[sprites.Length];

			for (int i = 0; i < names.Length; i++) {
				names[i] = sprites[i].name;
			}

			if (Gid > 0) {

				// every tileset for unity starts with index zero, so I neet to calculate the tile index based on gid
				int first = tileset.FirstGid == 0 ? 1 : tileset.FirstGid,
					tile_gid = Math.Abs(first - Gid),
					index = Array.IndexOf(names, tileset.Name + "_" + tile_gid);

				if (index <= sprites.Length) {

					sr.sprite = sprites[index];
					return true;
				
				}

			}

			return false;

		}

		public void SetPosition(Vector3 position, Quaternion rotation) {

			if (tileObject) {
				tileObject.transform.position = position;
				tileObject.transform.rotation = rotation;
			}

		}

		public void AttachTo(GameObject layer) {

			// attach only if tile object exists
			if (tileObject) {
				tileObject.transform.parent = layer.transform;
			}

		}
		
	}

}