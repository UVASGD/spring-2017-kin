// Unity 2D Tile MapperCode by Kaisirak 
// https://www.youtube.com/watch?v=_x0bMTxP7Yw
// Modified to work for Unity 5.3.4f (April 11, 2016)
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;

enum DRAWOPTION {select, paint, paintover, erase};

public class TileWindow : EditorWindow
{
	private static bool isEnabled;
	private Vector2 _scrollPos;
	private static Vector2 gridSize = new Vector2(1f, 1f);
	private static bool isGrid;
	private static bool isDraw = true;
	private static bool addBoxCollider;
	private static bool isObjmode;
	private static DRAWOPTION selected;
	private static GameObject parentObj;
	private static int layerOrd;
	private static string tagName;
	private int index;
	private string[] options;
	private Sprite[] allSprites;
	private string[] files;
	private static Sprite activeSprite;
	private static GameObject activeGo;
	public GUIStyle textureStyle;
	public GUIStyle textureStyleAct;

	[MenuItem("Window/TilemapEditor")]
	private static void TilemapEditor()
	{
		EditorWindow.GetWindow(typeof (TileWindow));
	}

	void Awake()
	{

	}

	void Update()
	{

	}

	public void OnInspectorUpdate()
	{
		// This will only get called 10 times per second.
		Repaint();
	}

	void OnEnable() {

		isEnabled = true;
		selected = DRAWOPTION.paint;
		Editor.CreateInstance(typeof(SceneViewEventHandler));
	}

	void OnDestroy() {
		isEnabled = false;
	}
	
	public class SceneViewEventHandler : Editor
	{
		static SceneViewEventHandler()
		{
			SceneView.onSceneGUIDelegate += OnSceneGUI;
		}

		static void OnSceneGUI(SceneView aView)
		{
			Event hotkey_e = Event.current;
			switch (hotkey_e.type) {
			case EventType.KeyDown:
				if (hotkey_e.shift)
				{
					switch (hotkey_e.keyCode) {
					case KeyCode.P:
						selected = DRAWOPTION.paint;
						break;
					case KeyCode.E:
						selected = DRAWOPTION.erase;
						break;
					case KeyCode.O:
						selected = DRAWOPTION.paintover;
						break;
					case KeyCode.S:
						selected = DRAWOPTION.select;
						break;
					case KeyCode.F:
						isDraw = !isDraw;
						break;
					case KeyCode.G:
						isGrid = !isGrid;
						break;
					}
				}
				break;
			}

			if (isEnabled)
			{
				if(isDraw)
				{
					Event e = Event.current;
					if (selected != DRAWOPTION.select)
					{
						HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
						if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && e.button == 0 && activeSprite != null)
						{
							Vector2 mousePos = Event.current.mousePosition;
							mousePos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePos.y;
							Vector3 mouseWorldPos = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(mousePos).origin;
							mouseWorldPos.z = layerOrd;
							if (gridSize.x > 0.05f && gridSize.y > 0.05f)
							{
								mouseWorldPos.x = Mathf.Floor(mouseWorldPos.x / gridSize.x) * gridSize.x + gridSize.x / 2.0f;
								mouseWorldPos.y = Mathf.Ceil(mouseWorldPos.y / gridSize.y) * gridSize.y - gridSize.y / 2.0f;
							}
							if(isObjmode)
								mouseWorldPos.z = mouseWorldPos.y + (activeSprite.bounds.size.y / -2.0f);
							GameObject[] allgo = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[];
							int brk = 0;
							if (selected == DRAWOPTION.paint)
							{
								for (int i = 0; i < allgo.Length;i++)
								{
									if (Mathf.Approximately(allgo[i].transform.position.x, mouseWorldPos.x) && Mathf.Approximately(allgo[i].transform.position.y, mouseWorldPos.y) && Mathf.Approximately(allgo[i].transform.position.z, mouseWorldPos.z))
									{
										brk++;
										break;
									}
								}
								if (brk == 0)
								{
									GameObject newgo = new GameObject(activeSprite.name, typeof(SpriteRenderer));
									newgo.transform.position = mouseWorldPos;
									newgo.GetComponent<SpriteRenderer>().sprite = activeSprite;
									if (tagName!=null) newgo.tag = tagName;
									if (addBoxCollider)
										newgo.AddComponent<BoxCollider2D>();
									if (parentObj != null)
										newgo.transform.parent = parentObj.transform;
								}
							}
							else if (selected == DRAWOPTION.paintover)
							{
								for (int i = 0; i < allgo.Length;i++)
								{
									if (allgo[i].GetComponent<SpriteRenderer>() != null & Mathf.Approximately(allgo[i].transform.position.x, mouseWorldPos.x) && Mathf.Approximately(allgo[i].transform.position.y, mouseWorldPos.y) && Mathf.Approximately(allgo[i].transform.position.z, mouseWorldPos.z))
									{
										allgo[i].GetComponent<SpriteRenderer>().sprite = activeSprite;
										brk++;
									}
								}
								if (brk == 0)
								{
									GameObject newgo = new GameObject(activeSprite.name, typeof(SpriteRenderer));
									newgo.transform.position = mouseWorldPos;
									newgo.GetComponent<SpriteRenderer>().sprite = activeSprite;
									if (tagName!=null) newgo.tag = tagName;
									if (addBoxCollider)
										newgo.AddComponent<BoxCollider2D>();
									if (parentObj != null)
										newgo.transform.parent = parentObj.transform;
								}
							}
							else if (selected == DRAWOPTION.erase)
							{
								for (int i = 0; i < allgo.Length;i++)
								{
									if (Mathf.Approximately(allgo[i].transform.position.x, mouseWorldPos.x) && Mathf.Approximately(allgo[i].transform.position.y, mouseWorldPos.y) && Mathf.Approximately(allgo[i].transform.position.z, mouseWorldPos.z))
										GameObject.DestroyImmediate(allgo[i]);
								}
							}
						}
					}
					else
					{
						HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
						Vector2 mousePos = Event.current.mousePosition;
						mousePos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePos.y;
						Vector3 mouseWorldPos = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(mousePos).origin;
						mouseWorldPos.z = layerOrd;		

						if (e.type == EventType.MouseDown && e.button == 0)
						{
							Selection.activeGameObject = null;
							GameObject[] allgo = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[];
							int brk = 0;
							for (int i = 0; i < allgo.Length;i++)
							{
								if (allgo[i].GetComponent<SpriteRenderer>() != null && allgo[i].GetComponent<SpriteRenderer>().bounds.Contains(mouseWorldPos))
								{
									brk++;
									activeGo = allgo[i];
									break;
								}
							}
							if (brk == 0)
								activeGo = null;

						}
						if (e.type == EventType.MouseDrag && e.button == 0 && activeGo != null)
						{
							if (gridSize.x > 0.05f && gridSize.y > 0.05f)
							{
								mouseWorldPos.x = Mathf.Floor(mouseWorldPos.x / gridSize.x) * gridSize.x + gridSize.x / 2.0f;
								mouseWorldPos.y = Mathf.Ceil(mouseWorldPos.y / gridSize.y) * gridSize.y - gridSize.y / 2.0f;
							}
							activeGo.transform.position = mouseWorldPos;
						}
					}
				}
			}
		}
	}

//	[CustomEditor(typeof(GameObject))]
//	public class SceneGUITest : Editor
//	{
//		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
//		static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
//		{
//			if (isEnabled && isGrid)
//			{
//				Gizmos.color = Color.white;
//				Vector3 minGrid = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(new Vector2(0f, 0f)).origin;
//				Vector3 maxGrid = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(new Vector2(SceneView.currentDrawingSceneView.camera.pixelWidth, SceneView.currentDrawingSceneView.camera.pixelHeight)).origin;
//				for (float i = Mathf.Round(minGrid.x / gridSize.x) * gridSize.x; i < Mathf.Round(maxGrid.x / gridSize.x) * gridSize.x && gridSize.x > 0.05f; i+=gridSize.x)
//					Gizmos.DrawLine(new Vector3(i,minGrid.y,0.0f), new Vector3(i,maxGrid.y,0.0f));
//				for (float j = Mathf.Round(minGrid.y / gridSize.y) * gridSize.y; j < Mathf.Round(maxGrid.y / gridSize.y) * gridSize.y && gridSize.y > 0.05f; j+=gridSize.y)
//					Gizmos.DrawLine(new Vector3(minGrid.x,j,0.0f), new Vector3(maxGrid.x,j,0.0f));
//				SceneView.RepaintAll();
//			}
//		}
//
//	}

//
	void OnGUI()
	{
		textureStyle = new GUIStyle(GUI.skin.button);
		textureStyle.margin = new RectOffset(2,2,2,2);
		textureStyle.normal.background = null;
		textureStyleAct = new GUIStyle(textureStyle);
		textureStyleAct.margin = new RectOffset(0,0,0,0);
		textureStyleAct.normal.background = textureStyle.active.background;

		if (!Directory.Exists(Application.dataPath + "/Tilemaps/"))
		{
			//Directory.CreateDirectory(Application.dataPath + "/Tilemaps/");
			AssetDatabase.CreateFolder("Assets", "Tilemaps");
			AssetDatabase.Refresh();
			Debug.Log("Created Tilemaps Directory");
		}
		files = Directory.GetFiles(Application.dataPath + "/Tilemaps/", "*.png");
		options = new string[files.Length];
		EditorGUILayout.LabelField("Tile Map", GUILayout.Width(256));
		for(int i = 0; i < files.Length; i++)
		{
			options[i] = files[i].Replace(Application.dataPath + "/Tilemaps/", "");
		}
		index = EditorGUILayout.Popup(index, options, GUILayout.Width(256));
		GUILayout.BeginHorizontal();
		isGrid = EditorGUILayout.Toggle(isGrid, GUILayout.Width(16));
		gridSize = EditorGUILayout.Vector2Field("Grid Size (0.05 minimum)", gridSize,  GUILayout.Width(236));
		GUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Parent Object", GUILayout.Width(256));
		parentObj = (GameObject)EditorGUILayout.ObjectField(parentObj, typeof(GameObject),true,GUILayout.Width(256));

		GUILayout.BeginHorizontal();
		addBoxCollider = EditorGUILayout.Toggle(addBoxCollider, GUILayout.Width(16));
		EditorGUILayout.LabelField("Add Box Collider", GUILayout.Width(256));
		GUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Layer Order", GUILayout.Width(256));

		GUILayout.BeginHorizontal();
		layerOrd = EditorGUILayout.IntField(layerOrd,  GUILayout.Width(126));
		isObjmode = EditorGUILayout.Toggle(isObjmode, GUILayout.Width(16));
		EditorGUILayout.LabelField("Layer based on Y", GUILayout.Width(110));
		GUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Tag", GUILayout.Width(32));
		GUILayout.BeginHorizontal();
		tagName = EditorGUILayout.TagField(tagName, GUILayout.Width(236));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		isDraw = EditorGUILayout.Toggle(isDraw, GUILayout.Width(16));
		selected = (DRAWOPTION)EditorGUILayout.EnumPopup(selected, GUILayout.Width(236));
		GUILayout.EndHorizontal();

		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
		GUILayout.BeginHorizontal();
		float ctr = 0.0f;
		if (options.Length > index)
		{
		allSprites = AssetDatabase.LoadAllAssetsAtPath("Assets/Tilemaps/" + options[index]).Select(x => x as Sprite).Where(x => x != null).ToArray();	
		foreach(Sprite singsprite in allSprites)
		{
			if (ctr > singsprite.textureRect.x)
			{
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
			}
			ctr = singsprite.textureRect.x;
			if (activeSprite == singsprite)
			{
				GUILayout.Button("", textureStyleAct, GUILayout.Width(singsprite.textureRect.width + 6), GUILayout.Height(singsprite.textureRect.height + 4));
				GUI.DrawTextureWithTexCoords(new Rect(GUILayoutUtility.GetLastRect().x + 3f,
				                                      GUILayoutUtility.GetLastRect().y + 2f,
				                                      GUILayoutUtility.GetLastRect().width - 6f,
				                                      GUILayoutUtility.GetLastRect().height - 4f),
				                             singsprite.texture,
				                             new Rect(singsprite.textureRect.x / (float)singsprite.texture.width,
				         singsprite.textureRect.y / (float)singsprite.texture.height,
				         singsprite.textureRect.width / (float)singsprite.texture.width,
				         singsprite.textureRect.height / (float)singsprite.texture.height));
			}
			else
			{
				if (GUILayout.Button("", textureStyle, GUILayout.Width(singsprite.textureRect.width + 2), GUILayout.Height(singsprite.textureRect.height + 2)))
					activeSprite = singsprite;
				GUI.DrawTextureWithTexCoords(GUILayoutUtility.GetLastRect(), singsprite.texture,
				                             new Rect(singsprite.textureRect.x / (float)singsprite.texture.width,
												         singsprite.textureRect.y / (float)singsprite.texture.height,
												         singsprite.textureRect.width / (float)singsprite.texture.width,
												         singsprite.textureRect.height / (float)singsprite.texture.height));
			}
		}
		}
		GUILayout.EndHorizontal();
		EditorGUILayout.EndScrollView();
		SceneView.RepaintAll();
	}
}
