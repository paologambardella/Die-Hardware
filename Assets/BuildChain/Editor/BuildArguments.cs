using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BuildArguments
{
    public static string GetBuildDirectory ()
    {
        string dir = GetString ("-buildOutputPath");
        return dir;
    }

    public static BuildTarget GetBuildTarget()
    {
        string target = GetString ("-targetPlatform").ToLower ();

        if (target.Equals("ios")) return BuildTarget.iOS;
        else if (target.Equals("android")) return BuildTarget.Android;
        else if (target.Equals("tvos")) return BuildTarget.tvOS;
        else if (target.Equals("osx")) return BuildTarget.StandaloneOSXUniversal;
        else if (target.Equals("windows")) return BuildTarget.StandaloneWindows;
        else if (target.Equals("windows64")) return BuildTarget.StandaloneWindows64;

        Debug.LogError("No build target specified");
        return BuildTarget.StandaloneOSXUniversal;
    }

    public static int GetAndroidBundleVersionCode()
    {
        return GetInt("-androidBundleVersionCode", 0);
    }

    public static string GetBundleVersion()
    {
        return GetString("-bundleVersion");
    }
    
    public static EditorBuildSettingsScene[] GetScenesToBuild()
    {
        List<string> sceneArgs = new List<string> ();
        string[] ARGS = System.Environment.GetCommandLineArgs ();

        for (int i = 0; i < ARGS.Length; i++) 
        {
            if (ARGS [i].StartsWith ("-scene")) 
            {
                while (ARGS.Length > (i+i))
                {
                    string nextArg = ARGS [i + 1];

                    if (nextArg.StartsWith("-"))
                    {
                        //we've reached the next argument
                        break;
                    }
                    else
                    {
                        sceneArgs.Add(nextArg);
                        ++i;
                    }
                }
            }
        }

        if (sceneArgs.Count == 0) 
        {
            return EditorBuildSettings.scenes;
        }
        else
        {
            //convert each string in sceneArgs to a EditorBuildSettingsScene
            List<EditorBuildSettingsScene> editorScenes = new List<EditorBuildSettingsScene>();

            foreach (string path in sceneArgs)
            {
                EditorBuildSettingsScene scene = new EditorBuildSettingsScene(path, true);
                editorScenes.Add(scene);
            }

            return editorScenes.ToArray();
        }
    }

    public static string AndroidKeyAliasPass()
    {
        return GetString("-keyaliasPass", "");
    }

    public static string AndroidKeyAliasName()
    {
        return GetString("-keyaliasName", "");
    }

    public static string AndroidKeystorePass()
    {
        return GetString("-keystorePass", "");
    }

    public static string AndroidKeystoreName()
    {
        return GetString("-androidKeystore", "");
    }

    static string GetString (string flag, string defaultValue)
    {
        //is there a command line arg?
        string finalArg = "";
        string[] ARGS = System.Environment.GetCommandLineArgs ();
        bool inFlag = false;
        for (int i = 0; i < ARGS.Length; i++) {
            if (inFlag) {
                if (ARGS [i].StartsWith ("-")) {
                    // we are done
                    inFlag = false;
                    break;
                }
                if (finalArg.Length > 0) {
                    finalArg += " ";
                }
                finalArg += ARGS [i];
                continue;
            }
            if (ARGS [i].ToLower() == flag.ToLower()) {
                inFlag = true;
            }
        }
        if (finalArg.Length == 0) {
            return defaultValue;
        }
        finalArg = finalArg.Replace ("\"", "");
        return finalArg;
    }

    static string GetString (string flag)
    {
        return GetString (flag, "");
    }

    static int GetInt (string flag, int defaultValue)
    {
        string rawArg = GetString (flag, "");
        if (rawArg == "") {
            return defaultValue;
        }
        // need to parse it here
        int argValue = 0;
        if (!int.TryParse (rawArg, out argValue)) {
            return defaultValue;
        }
        return argValue;
    }

    static bool GetBool (string flag, bool defaultValue)
    {
        string rawArg = GetString (flag, "");
        if (rawArg == "") {
            return defaultValue;
        }
        if (rawArg.ToLower () == "yes" || rawArg.ToLower() == "true") {
            return true;
        }
        return false;
    }

    static float GetFloat (string flag, float defaultValue)
    {
        string rawArg = GetString (flag, "");
        if (rawArg == "") {
            return defaultValue;
        }
        // need to parse it here
        float argValue = 0f;
        if (!float.TryParse (rawArg, out argValue)) {
            return defaultValue;
        }
        return argValue;
    }
}
