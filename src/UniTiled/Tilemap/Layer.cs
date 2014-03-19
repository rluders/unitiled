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

			foreach (Tile tile in Tiles) {
				
				tile.SetPosition(
					layerObject.transform.position,
					layerObject.transform.rotation);
				tile.AttachTo(layerObject);

			}

		}

	}

}