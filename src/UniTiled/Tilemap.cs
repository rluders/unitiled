using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UniTiled;

namespace UniTiled {

	public class TilesetList : List<Tileset> {

		public Tileset Last() {

			return this[this.Count - 1];

		}

		public Tileset GetFromGid(int gid) {

			foreach (Tileset tileset in this) {

				if (tileset.HasTileGid(gid)) {
					return tileset;
				}

			}

			return null;

		}

	}

	public class Tilemap {

		XmlDocument xmlDoc;		
		GameObject mapObject;

		public Vector2 MapSize { get; set; }
		public Vector2 TileSize { get; set; }
		public static TilesetList Tilesets { get; set; }
		public List<Layer> Layers { get; set; }

		public Tilemap(GameObject obj) {

			mapObject = obj;
			Tilesets = new TilesetList();
			Layers = new List<Layer>();

		}

		public void Load(TextAsset mapAsset) {

			xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(mapAsset.text);
			
			// @TODO throw exception if file is not loaded

			XmlNode mapNode = xmlDoc.DocumentElement.SelectSingleNode("/map");
			
			MapSize = new Vector2(
				float.Parse(mapNode.Attributes["width"].Value),
				float.Parse(mapNode.Attributes["height"].Value));

			TileSize = new Vector2(
				float.Parse(mapNode.Attributes["tilewidth"].Value),
				float.Parse(mapNode.Attributes["tileheight"].Value));

			LoadTilesets(mapNode.SelectNodes("/map/tileset"));
			LoadLayers(mapNode.SelectNodes("/map/layer"));

		}

		void LoadTilesets(XmlNodeList nodes) {

			foreach (XmlNode tilesetNode in nodes) {

				Tileset tileset = new Tileset(
					tilesetNode.Attributes["name"].Value,
					new Vector2(
						float.Parse(tilesetNode.Attributes["tilewidth"].Value),
						float.Parse(tilesetNode.Attributes["tileheight"].Value)),
					int.Parse(tilesetNode.Attributes["firstgid"].Value));

				// update lastgird value on last loaded tileset
				if (Tilesets.Count > 0) {
					Tilesets.Last().LastGid = int.Parse(tilesetNode.Attributes["firstgid"].Value) - 1;
				}


				Tilesets.Add(tileset);

			}

		}

		void LoadLayers(XmlNodeList nodes) {

			int depth = 0;
			foreach (XmlNode layerNode in nodes) {

				Layer layer = new Layer(
					layerNode.Attributes["name"].Value,
					new Vector2(
						float.Parse(layerNode.Attributes["width"].Value),
						float.Parse(layerNode.Attributes["height"].Value)),
					layerNode.SelectNodes("/map/layer[@name='" 
										+ layerNode.Attributes["name"].Value 
										+ "']/data/tile"),
					depth);

				depth--;

				Layers.Add(layer);
			}

		}

		public void Build() {

			foreach (Layer layer in Layers) {
				layer.AttachTo(mapObject);
				layer.Render();
			}

		}

	}

}