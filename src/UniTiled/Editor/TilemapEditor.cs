using UnityEngine;
using System.Collections;
using UnityEditor;
using UniTiled;

namespace UniTiled {

	[CustomEditor(typeof(TilemapComponent))]
	public class TilemapEditor : Editor {

		public override void OnInspectorGUI() {

			DrawDefaultInspector();

			TilemapComponent component = (TilemapComponent)target;

			if (GUILayout.Button("Load tilemap")) {
				
				component.LoadTilemap();

			}

		}

	}

}