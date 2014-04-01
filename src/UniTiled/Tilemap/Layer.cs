using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace UniTiled {

	public class Layer {
		
		GameObject layerObject;

		public string Name { get; private set; }
		public Vector2 Size { get; private set; }
		public List<Tile> Tiles { get; private set; }

		public Layer(string name, Vector2 size, XmlNodeList tileNodes) {

			Name = name;
			Size = size;
			layerObject = new GameObject(name);

			LoadTiles(tileNodes);

		}

		void LoadTiles(XmlNodeList nodes) {

			Tiles = new List<Tile>();

			foreach (XmlNode tileNode in nodes) {

				Tile tile = new Tile(
					int.Parse(tileNode.Attributes["gid"].Value));

				Tiles.Add(tile);

			}

		}

		public void AttachTo(GameObject map) {

			layerObject.transform.parent = map.transform;

		}

		public void Render() {

			Transform go = layerObject.transform;

			// @TODO put it into Tile or/and Tilemap properties
			float x_size = .3f,
				  y_size = .3f;

			int col = 0,
				row = (int)Size.y;

			foreach (Tile tile in Tiles) {

				Vector3 position = new Vector3(
					go.position.x + (col * x_size),
					go.position.y + (row * y_size),
					go.position.z);
				
				tile.SetPosition(
					position,
					go.rotation);
				tile.AttachTo(layerObject);

				col++;

				if (col >= (int)Size.x) {
					col = 0;
					row--;
				}

			}

		}

	}

}