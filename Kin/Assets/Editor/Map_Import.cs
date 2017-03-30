using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class MapImportEditor : EditorWindow {
    
    int PPU = 100;
    int l = 2000;
    int gX, gY; // grid units
    float scale = 1;
    string MapName = "main";
    string[] layers = { "grass", "wall", "trees" };
    string loc = "Resources/Sprites/Maps/Grid/";
    Vector2 mapSize = new Vector2(8000, 8000);
    Vector3 start = new Vector3(0, 0, 0);

    [MenuItem("Tools/Map Import")]
    private static void MapImport() {
        EditorWindow.GetWindow(typeof(MapImportEditor));
    }
    
    void OnGUI() {
        GUILayout.Label("Import Settings", EditorStyles.boldLabel);
        loc = EditorGUILayout.TextField("Asset Location", loc);
        if (!loc.EndsWith("/")) loc += "/";

        mapSize = EditorGUILayout.Vector2Field("Map Size", mapSize);
        l = EditorGUILayout.IntField("Grid Size", l);

        gX = (int)(mapSize.x) / l;
        gY = (int)(mapSize.y) / l;

        GUILayout.Label("Map Placement Settings", EditorStyles.boldLabel);
        MapName = EditorGUILayout.TextField("Map Name", MapName);
        start = EditorGUILayout.Vector3Field("Start Pos", start);
        scale = EditorGUILayout.FloatField("Map Scale", scale);
        PPU = EditorGUILayout.IntField("Pixels per Unit", PPU);

        if (GUILayout.Button("Place Map")) {
            place();
        }
    }
    
    void place() {
        string path = Application.dataPath + "/" + loc;
        if (!Directory.Exists(path)) {
            Debug.Log(path + " does not exist.");
            return;
        }

        GameObject map = GameObject.Find("MapGrid_"+MapName);
        if(map == null) map = new GameObject("MapGrid_"+MapName);

        string[] files = Directory.GetFiles(Application.dataPath + "/" + loc, "*png");
        foreach(string f in files) {
            Sprite[] all = AssetDatabase.LoadAllAssetsAtPath("Assets/" + loc + f.Replace(path, ""))
            .Select(x => x as Sprite).ToArray();
            for (int i = 0; i < all.Length; i++) {
                Sprite s = all[i];
                if(s!= null) {
                    string layer = null; int order = 0, j = 0;

                    // determine layer as well as sprite sorting order
                    foreach (string l in layers) {
                        if (s.name.Contains(l)) {
                            layer = l;
                            order = j-2;
                            break;
                        }
                        j++;
                    }

                    if (layer != null) {
                        string num = s.name
                            .Replace(MapName, "")
                            .Replace(layer, "")
                            .Replace("_", "")
                            .Replace(".png", "");
                        
                        int t = int.Parse(num)-1;
                        int y = (int)Mathf.Floor(t / (float)gY);
                        int x = t - gX * y;
                        Vector2 gI = new Vector2(x, y);
                        string gridLoc = getAlphaName(y) + x;

                        // find parent object
                        GameObject parent = GameObject.Find(MapName + "_" + layer);
                        if (parent == null) {
                            parent = new GameObject(MapName + "_" + layer);
                            parent.transform.parent = map.transform;
                        }

                        // check if object exists
                        bool created = false;
                        GameObject mapObj = GameObject.Find(MapName + "_" + layer + "_" + gridLoc);
                        if (mapObj == null) {
                            created = true;
                            mapObj = new GameObject(MapName + "_" + layer + "_" + gridLoc, typeof(SpriteRenderer));
                        }

                        // place Object
                        mapObj.transform.position = new Vector3((start.x + l * scale / (2 * PPU)) + gI.x * l * scale / PPU,
                            (start.y - l * scale / (2 * PPU)) - gI.y * l * scale / PPU, start.z);
                        mapObj.transform.parent = parent.transform;
                        SpriteRenderer sr = mapObj.GetComponent<SpriteRenderer>();
                        sr.sortingOrder = order;

                        // set sprite
                        if (created) sr.sprite = s;
                    }
                }
            }
        }
    }

    string getAlphaName(int x) {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var res = "";

        if (x >= letters.Length)
            res += letters[x / letters.Length - 1];
        res += letters[x % letters.Length];

        return res;
    }
}

