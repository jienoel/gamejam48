using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ExportScenePrefabs
{
	[MenuItem ("Sandbox/Export Scene Prefabs")]
	public static void Export ()
	{		
		//UIRoot ur = UIRoot.list [0];
		ExportPrefab[] ep = GameObject.FindObjectsOfType<ExportPrefab> ();
		foreach (var e in ep) {
			GameObject obj = (GameObject)GameObject.Instantiate (e.gameObject);
			obj.transform.parent = e.gameObject.transform.parent;

			string finalpath = Path.Combine ("Assets", e.path);
			finalpath = Path.Combine (finalpath, e.gameObject.name + ".prefab").Replace ("\\", "/");

			GameObject prefab = CreateNew (obj, finalpath);
			EditorUtility.SetDirty (prefab);
			//ExportPrefab re = prefab.GetComponent<ExportPrefab>();
			//GameObject.DestroyImmediate(re, true);            
			GameObject.DestroyImmediate (obj, true);


		}
		AssetDatabase.SaveAssets ();
	}

	public static GameObject CreateNew (GameObject obj, string localPath)
	{
		string path = Path.GetDirectoryName (localPath);
		if (!Directory.Exists (path)) {
			Directory.CreateDirectory (path);
		}
		Object prefab = AssetDatabase.LoadAssetAtPath (localPath, typeof(GameObject));
		if (prefab == null) {
			prefab = PrefabUtility.CreateEmptyPrefab (localPath);
		}
		GameObject res = PrefabUtility.ReplacePrefab (obj, prefab, ReplacePrefabOptions.ReplaceNameBased);
		return res;
		//GameObject res = PrefabUtility.CreatePrefab(localPath, obj, ReplacePrefabOptions.Default);

		//return res;
	}

	public static string GetGameObjectPath (GameObject obj)
	{
		string path = "/" + obj.name;
		while (obj.transform.parent != null) {
			obj = obj.transform.parent.gameObject;
			path = "/" + obj.name + path;
		}
		return path;
	}
}

