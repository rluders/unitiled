using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UniTiled {

	public class Tile {

		GameObject tileObject;

		public int Gid { get; set; }

		public Tile(int gid) {

			Gid = gid;
			tileObject = new GameObject("tile_" + gid);

			// GameObject go = GameObject.Find("_Tilemap");
			// Tileset tileset = go.GetComponent<TilemapComponent>().tilemap.Tilesets.GetFromGid(gid);
			Tileset tileset = Tilemap.Tilesets.GetFromGid(gid);
			Debug.Log("Gid: " + gid);

			// @TEST
			tileObject.AddComponent("SpriteRenderer");
			SpriteRenderer sr = (SpriteRenderer)tileObject.GetComponent("SpriteRenderer");
			
			Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/Tiles/" + tileset.Name);
			string[] names = new string[sprites.Length];

			for (int i = 0; i < names.Length; i++) {
				names[i] = sprites[i].name;
			}

			if (gid > 0) {
				sr.sprite = sprites[Array.IndexOf(names, tileset.Name + "_" + (gid))];
			}

		}

		public void SetPosition(Vector2 position, Quaternion rotation) {

			tileObject.transform.position = position;
			tileObject.transform.rotation = rotation;

		}

		public void AttachTo(GameObject map) {

			tileObject.transform.parent = map.transform;

		}
		
	}

}