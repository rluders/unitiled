using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UniTiled;

namespace UniTiled {

	public class Tilemap {

		XmlDocument xmlDoc;		
		GameObject mapObject;

		public Vector2 MapSize { get; set; }
		public Vector2 TileSize { get; set; }
		public List<Tileset> Tilesets { get; set; }
		public List<Layer> Layers { get; set; }

		public Tilemap(GameObject obj) {

			mapObject = obj;
			Tilesets = new List<Tileset>();
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
						float.Parse(tilesetNode.Attributes["tileheight"].Value)));

				Tilesets.Add(tileset);

			}

		}

		void LoadLayers(XmlNodeList nodes) {

			foreach (XmlNode layerNode in nodes) {

				Layer layer = new Layer(
					layerNode.Attributes["name"].Value,
					new Vector2(
						float.Parse(layerNode.Attributes["width"].Value),
						float.Parse(layerNode.Attributes["height"].Value)),
					layerNode.SelectNodes("/map/layer[@name='" 
										+ layerNode.Attributes["name"].Value 
										+ "']/data/tile"));

				Layers.Add(layer);
			}

		}

		public void Build() {

			Debug.Log("Build map");

			foreach (Layer layer in Layers) {
				layer.AttachTo(mapObject);
				layer.Render();
			}

		}

	}

}