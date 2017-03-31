using UnityEngine;
using UnityEditor;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using LitJson;
using System.Collections;

public class Anim_Import : EditorWindow {

    string text = "hiya :3 i jus 8 a pair it was gud";
    public string asepriteLoc = "C:/Program Files (x86)/Aseprite/";
    public static string artFolder = "";

    /// <summary>
    /// locates the art folder by assuming it is found in the same directory as the Unity project by the name "Art"
    /// </summary>
    void OnEnable() {
        string s = Application.dataPath;
        string key = s.Contains("/") ? "/" : "\\";
        s = s.Replace(key + "Assets","");
        artFolder = s.Substring(0, s.LastIndexOf(key)) + key + "Art" + key;
    }

    private string extractLoc =  "JSON/";
    private string[] options, files;
    private int index;
    private Vector2 scroll;
    public GameObject go;
    public bool update = true;
    public bool directImport = false;
    public bool showFrameData = true;

    public AseData animDat;

    [MenuItem("Tools/Anim Import")]
    private static void AnimImport() {
        EditorWindow.GetWindow(typeof(Anim_Import));
    }

    void OnGUI() {
        #region extract
        GUILayout.Label("Ase Extract Settings", EditorStyles.boldLabel);
        
        asepriteLoc = EditorGUILayout.TextField("aseprite.exe Location", asepriteLoc);
        if (File.Exists(asepriteLoc + "aseprite.exe")) {
            artFolder = EditorGUILayout.TextField("Art Folder", artFolder);

            if (Directory.Exists(artFolder)) {
                files = Directory.GetFiles(artFolder, "*.ase");
                options = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                    options[i] = files[i].Replace(artFolder, "").Replace(".ase", "");

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Aseprite File");
                index = EditorGUILayout.Popup(index, options);
                GUILayout.EndHorizontal();

                // extract data from ase file
                if (GUILayout.Button("Extract From .ase File"))
                    extractAse(options[index]);
                #endregion

                #region import
                GUILayout.Label("Import Settings", EditorStyles.boldLabel);
                directImport = EditorGUILayout.BeginToggleGroup("Apply Directly to Object", directImport);
                /*update = */ EditorGUILayout.Toggle("Update Existing Clips", update);
                showFrameData = EditorGUILayout.Toggle("Show Frame Data ", showFrameData);
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Gameobject");
                go = EditorGUILayout.ObjectField(go, typeof(GameObject), true, null) as GameObject;
                EditorGUILayout.EndToggleGroup();

                if (GUILayout.Button("Import Animation Data"))
                    importAnims();
                #endregion

                #region dat display
                EditorGUILayout.BeginToggleGroup("Anim Data", animDat != null);
                if (animDat != null) {
                    scroll = EditorGUILayout.BeginScrollView(scroll);
                    text = EditorGUILayout.TextArea(text);
                    EditorGUILayout.EndScrollView();
                }
                EditorGUILayout.EndToggleGroup();
                #endregion
            } else {
                GUILayout.Label("Cannot find specified folder.", EditorStyles.miniLabel);

            }
        } else {
            GUILayout.Label("Could not find aseprite.exe at \"" + asepriteLoc + "\".", EditorStyles.miniLabel);
        }
    }

    /// <summary>
    /// imports the sprites of a given animation into each AnimationClip with appropiate frame rates
    /// </summary>
    /// <param name="anim"></param>
    public void importAnims() {
        string objName = options[index];

        // load file containing values for frame durations
        string filename = artFolder + "/" + extractLoc + options[index] + ".json";
        if (!File.Exists(filename)) {
            UnityEngine.Debug.LogError("Please extract from " + options[index] + ".ase first" +
                "before attempting to import animation data.");
            return;
        }

        animDat = readJSON(filename);
        if (animDat == null) {
            UnityEngine.Debug.LogError("Error parsing \"" + filename + "\".");
            return;
        } else {
            // update text area
            text = "index: sample\tSprite Name\n\n";
            foreach(AseData.Clip clip in animDat.clips) {
                text += "Clip Name: " + clip.name;
                //text += "\ndynmaic rate: " + clip.dynamicRate;
                text += "\nsample rate: " + clip.sampleRate;
                if (showFrameData)
                    for (int j = 0; j <= clip.len; j++)
                        if (!clip.dynamicRate) {
                            if (j < clip.len)
                                text += "\n[" + j + "]; " + clip[j] + "\t" + options[index] + "_" + (clip.start + j);
                        } else {
                            text += "\n[" + j + "]; " + clip[j] + "\t" + options[index] + "_" +
                                (clip.start + j - ((j == clip.len) ? 1 : 0));
                        }
                text += "\n================================\n";
            }
        }

        // this was code I started when I tried to directly update the animation data
        if (directImport) {
            if(go == null) {
                UnityEngine.Debug.LogError("GameObject cannot be null!");
                return;
            }

            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/" + objName);
            if (update) updateClips(objName, sprites);
            else createClips(objName, sprites);
        }
    }
    
    /// <summary>
    /// Updates AnimationClips attached to the GameObject go to reflect Sprite Animations from
    /// the respective ase file. Note that if a loop with the name of the AnimationClip does not exist
    /// in the ase file (case insensitive), the animation will not be updated. 
    /// </summary>
    /// <param name="objName"> name of the sprites to add references to </param>
    /// <param name="sprites"> list of all sprites available in the project</param>
    public void updateClips(string objName, Sprite[] sprites) {
        // for each clip, adjust frames
        foreach (AnimationClip aC in AnimationUtility.GetAnimationClips(go)) {
            AseData.Clip clip = animDat[aC.name];
            if (clip == null) {
                UnityEngine.Debug.LogError(objName + ": Animation Clip \"" + aC.name + "\" does not exist in the .ase file."
                    + "\nCannot import its animation data.");
                continue;
            }
            aC.frameRate = clip.sampleRate;
            ObjectReferenceKeyframe[] k = new ObjectReferenceKeyframe[clip.len + 1];
            Sprite sprite = null;
            for (int j = 0; j <= clip.len; j++) {
                if (!clip.dynamicRate) {
                    if (j < clip.len)
                        sprite = sprites[clip.start + j];
                } else {
                    sprite = sprites[clip.start + j - ((j == clip.len) ? 1 : 0)];
                }
                
                k[j] = new ObjectReferenceKeyframe();
                k[j].time = clip[j]*(clip.l0 / 1000f); //time is in secs? WTF!!!
                k[j].value = sprite;
            }
            AnimationUtility.SetObjectReferenceCurve(aC, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), k);
        }
    }

    /// <summary>
    /// This function has not been properly coded. It is supposed to create AnimationClips with proper references to objects and attach it to GameObject go
    /// </summary>
    /// <param name="objName"> name of the sprites to add references to </param>
    /// <param name="sprites"> list of all sprites available in the project</param>
    public void createClips(string objName, Sprite[] sprites) {
        //Sprite sprite = null;
        //foreach (AseData.Clip clip in animDat.clips) {
            //AnimationClip aC = new AnimationClip();
            //aC.frameRate = clip.sampleRate;
            //ObjectReferenceKeyframe[] k = new ObjectReferenceKeyframe[clip.len + 1];

            //for (int j = 0; j <= clip.len; j++) {
            //    if (!clip.dynamicRate) {
            //        if (j < clip.len)
            //            sprite = sprites[clip.start + j];
            //    } else {
            //        sprite = sprites[clip.start + j - ((j == clip.len) ? 1 : 0)];
            //    }

            //    k[j] = new ObjectReferenceKeyframe();
            //    k[j].time = clip[j] * (clip.l0 / 1000f); //time is in secs? WTF!!!
            //    k[j].value = sprite;
            //}
            //aC.SetCurve("", typeof(SpriteRenderer), "Sprite", null);
            //AnimationUtility.SetObjectReferenceCurve(aC, EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"), k);
            //AssetDatabase.CreateAsset(aC, "Assets/Resources/Animations/TESTING/" + objName + "_" + clip.name + "_test.anim");
            //aC.wrapMode = WrapMode.Loop;
            //go.AddComponent<UnityEngine.Animation>();
            //UnityEngine.Animation an = go.GetComponent<UnityEngine.Animation>();
            //an.AddClip(aC, "TstCurve");
        //}
    }

    /// <summary>
    /// Copies the .ase file into a readable .json at a temp folder in the Art directory 
    /// </summary>
    /// <param name="aseName"></param>
    void extractAse(string aseName) {
        string ae = artFolder + "/" + extractLoc;
        if (!Directory.Exists(ae))
            Directory.CreateDirectory(ae);

        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = asepriteLoc + "aseprite.exe";
        startInfo.Arguments = "-b --list-tags --ignore-empty --data \"" +
           ae + aseName + ".json\" \"" + artFolder + "/" + aseName + ".ase";
        process.StartInfo = startInfo;
        process.Start();
    }

    /// <summary>
    /// Reads animation data from an extracted json file.
    /// </summary>
    /// <param name="file"> location of the json file </param>
    /// <returns></returns>
    private AseData readJSON(string file) {
        AseData anim = new AseData();
        JsonData dat = JsonMapper.ToObject(File.ReadAllText(file));
        JsonData frames = dat["frames"];
        JsonData tags = dat["meta"]["frameTags"];
        anim.clips = new List<AseData.Clip>();

        anim.durations = new int[frames.Count];
        for (int i = 0; i < frames.Count; i++) {
            anim.durations[i] = int.Parse(frames[i]["duration"].ToString());
        }

        // load loop names
        if (tags.Count == 0) {
            anim.clips.Add(new AseData.Clip(anim, "base", 0, frames.Count - 1));
        } else {
            for (int i = 0; i < tags.Count; i++) {
                anim.clips.Add(new AseData.Clip(anim,
                    tags[i]["name"].ToString(),
                    int.Parse(tags[i]["from"].ToString()),
                    int.Parse(tags[i]["to"].ToString())));
            }
        }
        return anim;
    }

    #region math
    /// <summary>
    /// determines the max from a subset of a list of integers
    /// </summary>
    /// <param name="f"></param>
    /// <param name="i0"></param>
    /// <returns></returns>
    public static int sum(int[] f, int i0) {
        int sum = 0;
        for (int i = 0; i < i0; i++) sum += f[i];
        return sum;
    }

    /// <summary>
    /// Determines the Greatest Common Denominator from a list of integers
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static int GCD(int[] numbers) { return numbers.Aggregate(GCD); }
    public static int GCD(int a, int b) { return b == 0 ? a : GCD(b, a % b); }
    #endregion
}

#region Extra Classes
public static class LinqHelper {
    /// <summary>
    /// splits an array into a subset
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"> the original array</param>
    /// <param name="index"> the start of the subset </param>
    /// <param name="length"> the length of the subset </param>
    /// <returns></returns>
    public static T[] SubArray<T>(this T[] data, int index, int length) {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
}

/// <summary>
/// A class containing data pulled from a .ase file
/// </summary>
public class AseData {
    public int[] durations;
    public List<Clip> clips;
    public int frameCount { get { return durations.Length; } }
    public Clip this[string clipName] {
        get {
            foreach (Clip c in clips)
                if (c.name.ToLower().Equals(clipName.ToLower()))
                    return c;
            return null;
        }
    }

    /// <summary>
    /// A class containing data for an animation clip found in a .ase file
    /// </summary>
    public class Clip {
        public string name;
        public int start, end, sampleRate;
        public bool dynamicRate;
        private AseData owner;

        public int len { get { return end - start + 1; } }
        public float l0;
        public int[] frames { get { return owner.durations.SubArray(start, len); } }
        public int[] samples;

        public Clip(AseData owner, string name, int s, int e) {
            this.owner = owner;
            this.name = name;
            start = s;
            end = e;

            samples = new int[len+1];
            init();
        }

        /// <summary>
        /// gets the sample index for the given frame
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int this[int i] { get { return samples[i]; } }

        /// <summary>
        /// calculates animation relavent data
        /// </summary>
        public void init() {
            dynamicRate = false;
            foreach (int i in frames)
                if (i != frames[0]) {
                    dynamicRate = true;
                    break;
                }

             l0 = dynamicRate ? Anim_Import.GCD(frames) : frames[0]; // frame duration
            sampleRate = (int)Math.Round(1000f / l0); 

            // for each frame, place event at sample index
            int end = (dynamicRate) ? len + 1 : len;
            for (int i0 = 0; i0 < end; i0++) {
                samples[i0] = i0;
                if (dynamicRate) {
                    samples[i0] = (int)(Anim_Import.sum(frames, i0) / l0);
                    if ((i0 == len)) samples[i0]--;
                }
            }
        }
    }
}
#endregion
