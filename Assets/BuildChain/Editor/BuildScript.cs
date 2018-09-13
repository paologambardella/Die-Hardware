using UnityEngine;
using UnityEditor;
using System.Collections;

public class BuildScript 
{
    static void BuildForAndroid()
    {
        SetupAndroidBuild();

        int success = BuildPlayer();

        EditorApplication.Exit(success);
    }

    static void SetupAndroidBuild()
    {
        PlayerSettings.Android.keyaliasPass = BuildArguments.AndroidKeyAliasPass();
        PlayerSettings.Android.keyaliasName = BuildArguments.AndroidKeyAliasName();
        PlayerSettings.Android.keystorePass = BuildArguments.AndroidKeystorePass();
        PlayerSettings.Android.keystoreName = BuildArguments.AndroidKeystoreName();

        PlayerSettings.Android.bundleVersionCode = BuildArguments.GetAndroidBundleVersionCode();
    }

    static int BuildPlayer()
    {
        Debug.Log("\n\nSTARTING BUILD\n\n");

        EditorBuildSettingsScene[] scenes = BuildArguments.GetScenesToBuild();

        Debug.Log("BuildScenes:");
        foreach (var scene in scenes)
        {
            Debug.Log("\t" + scene.path);
        }

        string buildOutputPath = BuildArguments.GetBuildDirectory();
        Debug.Log("Build Directory: " + buildOutputPath);

        BuildOptions buildOptions = BuildOptions.None;
        BuildTarget buildTarget = BuildArguments.GetBuildTarget();

        PlayerSettings.bundleVersion = BuildArguments.GetBundleVersion();

        string errorMessage = BuildPipeline.BuildPlayer(scenes, buildOutputPath, buildTarget, buildOptions);

        Debug.Log("\n\nFINISHED BUILD\n\n");

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Debug.Log("Finished build with pipline error: " + errorMessage);
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
