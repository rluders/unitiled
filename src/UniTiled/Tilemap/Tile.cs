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
			// @TEST
			tileObject.AddComponent("SpriteRenderer");
			SpriteRenderer sr = (SpriteRenderer)tileObject.GetComponent("SpriteRenderer");
			
			Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/Tiles/Test");
			string[] names = new string[sprites.Length];

			for (int i = 0; i < names.Length; i++) {
				names[i] = sprites[i].name;
			}

			if (gid > 0) {
				sr.sprite = sprites[Array.IndexOf(names, "Test_" + (gid - 1))];
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