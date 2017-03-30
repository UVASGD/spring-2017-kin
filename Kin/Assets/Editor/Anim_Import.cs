using UnityEngine;
using UnityEditor;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using LitJson;

public class Anim_Import : EditorWindow {

    string text = "hiya :3 i jus 8 a pair it was gud";
    public string asepriteLoc = "C:/Program Files (x86)/Aseprite/";
    public string artFolder = "C:/Users/Jay/OneDrive/Documents/UVA/SGD/spring-2017-kin/Art/";
    private string extractLoc =  "Temp/";
    private string[] options, files;
    private int index;

    [IODescription("The object's whose animation should be modified")]
    public GameObject go;
    [IODescription("if true, the import function will modify existing animation loops. " +
    "\notherwise, it will create new loops from the corresponding ase file")]
    public bool update = true;
    public bool directImport = false;

    public AseData animDat;

    [MenuItem("Tools/Anim Import")]
    private static void AnimImport() {
        EditorWindow.GetWindow(typeof(Anim_Import));
    }

    Vector2 scroll;
    void OnGUI() {
        #region extract
        GUILayout.Label("Ase Extract Settings", EditorStyles.boldLabel);
        artFolder = EditorGUILayout.TextField("Art Folder", artFolder);
        //if (!artFolder.EndsWith("/")) artFolder += "/";

        files = Directory.GetFiles(artFolder, "*.ase");
        options = new string[files.Length];
        for (int i = 0; i < files.Length; i++)
            options[i] = files[i].Replace(artFolder, "").Replace(".ase","");

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Aseprite File");
        index = EditorGUILayout.Popup(index, options);
        GUILayout.EndHorizontal();

        // extract data from ase file
        if (GUILayout.Button("Extract from Ase File"))
            extractAse(options[index]);
        #endregion

        #region import
        GUILayout.Label("Import Settings", EditorStyles.boldLabel);
        asepriteLoc = EditorGUILayout.TextField("aseprite.exe Location", asepriteLoc);
        if (!asepriteLoc.EndsWith("/")) asepriteLoc += "/";

        directImport = EditorGUILayout.BeginToggleGroup("Apply Directly to Object", directImport);
        update = EditorGUILayout.BeginToggleGroup("Update Existing Clips", update);
        EditorGUILayout.EndToggleGroup();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Gameobject");
        go = EditorGUILayout.ObjectField(go, typeof(GameObject), true, null) as GameObject;
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Import Animation Data"))
            importAnims();
        #endregion

        #region dat display
        EditorGUILayout.BeginToggleGroup ("Anim Data", animDat != null);
        if (animDat != null) {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            text = EditorGUILayout.TextArea(text);
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndToggleGroup();
        #endregion
    }

    /// <summary>
    /// imports the sprites of a given animation with appropiate frame rates
    /// </summary>
    /// <param name="anim"></param>
    public void importAnims() {
        if (go != null) {
            Animator anim = go.GetComponent<Animator>();
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        }

        string objName = options[index];

        // load file containing values for frame durations
        string filename = artFolder + "/" + extractLoc + options[index] + ".json";
        if (!File.Exists(filename)) {
            UnityEngine.Debug.LogError("Please extract data from the ase files using Anim_Extractor.exe" +
                "\nbefore attempting to import animation data.");
            return;
        }

        animDat = readJSON(filename);
        if (animDat == null) {
            UnityEngine.Debug.LogError("Error parsing \"" + filename + "\".");
            return;
        } else {
            text = "index: sample\tSprite Name\n\n";
            foreach(AseData.Clip clip in animDat.clips) {
                text += "Clip Name: " + clip.name;
                text += "\ndynmaic rate: " + clip.dynamicRate;
                text += "\nsample rate: " + clip.sampleRate;
                for (int j = 0; j < clip.samples.Length; j++)
                    if (!clip.dynamicRate) {
                        if (j < clip.samples.Length - 1)
                            text += "\n[" + j + "]; " + clip.samples[j] + "\t" + options[index] + "_" + (clip.start + j);
                    } else {
                        int x = (j == clip.samples.Length - 1) ? 1 : 0;
                        text += "\n[" + j + "]; " + clip.samples[j] + "\t" + options[index] + "_" + (clip.start + j - x);
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
    
    public void updateClips(string objName, Sprite[] sprites) {
        // for each clip, adjust frames
        foreach (AnimationClip aC in AnimationUtility.GetAnimationClips(go)) {
            AseData.Clip clip = animDat[aC.name];
            if (clip == null) {
                UnityEngine.Debug.LogError(objName + ": Animation Clip \"" + aC.name + "\" does not exist in the .ase file."
                    + "\nCannot import its animation data.");
                continue;
            }

            bool dynamicRate = false;
            foreach (int i in clip.frames)
                if (i != clip.frames[0]) {
                    dynamicRate = true;
                    break;
                }

            float l0 = dynamicRate ? GCD(clip.frames) : clip.frames[0], // sample duration
            s0 = (int)Math.Round(1000f / l0); //sample rate
            UnityEngine.Debug.Log(objName + ": " + clip.name + "\ts0: " + s0);

            // for each frame, place event at sample index
            int end = (dynamicRate) ? clip.len + 1 : clip.len;
            for (int i0 = 0; i0 < end; i0++) {
                int sample = i0;
                if (dynamicRate) {
                    sample = (int)(sum(clip.frames, i0) / l0);
                    if ((i0 == clip.len)) sample--;
                }

                Sprite sprite = (dynamicRate && i0 == end - 1) ? sprites[clip.start + i0 - 1] :
                    sprites[clip.start + i0];
                UnityEngine.Debug.Log(sprite.name + ": " + sample);
            }
        }
    }

    public void createClips(string objName, Sprite[] sprites) {

    }

    void getShit(FileInfo f) {
        string filename = f.FullName;
        Console.WriteLine("loading Clip data from: " + f.Name);

        string[] lines = File.ReadAllLines(filename)[0].
            Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int frameCount = lines.Length;
        int[] frmDurs = new int[frameCount];
        for (int i = 0; i < frameCount; i++)
            frmDurs[i] = int.Parse(lines[i].Trim());

        int start = 0, len = 4;
        int[] frames = frmDurs.SubArray(start, len);

        for (int i = 0; i < frames.Length; i++)
            Console.WriteLine("frames[" + i + "]: " + frames[i]);

        bool dynamicRate = false;
        foreach (int i in frames)
            if (i != frames[0]) {
                dynamicRate = true;
                break;
            }

        float l0 = dynamicRate ? GCD(frames) : frames[0],
        s0 = (int)Math.Round(1000f / l0);
        Console.WriteLine("l0: " + l0);

        // for each frame, place event at sample index
        int end = (dynamicRate) ? frames.Length + 1 : frames.Length;
        for (int i0 = 0; i0 < end; i0++) {
            int sample = i0;
            if (dynamicRate) {
                sample = (int)(sum(frames, i0) / l0);
                if ((i0 == frames.Length)) sample--;
            }

            Console.WriteLine("f[" + i0 + "]: " + sample);
        }
    }

    void extractAse(string aseName) {
        if (!File.Exists(asepriteLoc + "aseprite.exe")) {
            UnityEngine.Debug.LogError("Could not find aseprite.exe at \"" + asepriteLoc + "\".");
            return;
        }

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
        for (int i = 0; i < tags.Count; i++) {
            anim.clips.Add(new AseData.Clip(anim,
                tags[i]["name"].ToString(),
                int.Parse(tags[i]["from"].ToString()),
                int.Parse(tags[i]["to"].ToString())));
        }
        return anim;
    }

    #region math
    public static int sum(int[] f, int i0) {
        int sum = 0;
        for (int i = 0; i < i0; i++) sum += f[i];
        return sum;
    }

    public static int GCD(int[] numbers) { return numbers.Aggregate(GCD); }
    public static int GCD(int a, int b) { return b == 0 ? a : GCD(b, a % b); }
    #endregion
}

#region Extra Classes
public static class LinqHelper {
    public static T[] SubArray<T>(this T[] data, int index, int length) {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
}

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

    public class Clip {
        public string name;
        public int start, end, sampleRate;
        public bool dynamicRate;
        private AseData owner;

        public int len { get { return end - start + 1; } }
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

        public void init() {
            dynamicRate = false;
            foreach (int i in frames)
                if (i != frames[0]) {
                    dynamicRate = true;
                    break;
                }

            float l0 = dynamicRate ? Anim_Import.GCD(frames) : frames[0]; // sample duration
            sampleRate = (int)Math.Round(1000f / l0); //sample rate
            //UnityEngine.Debug.Log(name + "\ts0: " + sampleRate);

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
