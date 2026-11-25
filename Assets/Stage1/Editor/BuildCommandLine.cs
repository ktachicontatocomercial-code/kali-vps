using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildCommandLine
{
    public static void BuildAndroid()
    {
        const string scenePath = "Assets/Stage1/Scenes/Stage1_Lobby.unity";
        const string outputPath = "Builds/Android/BattleRoyale-Stage1.apk";

        Directory.CreateDirectory("Builds/Android");
        Stage1ProjectBuilder.BuildStage1();
        AssetDatabase.Refresh();

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.ktachicontatocomercial.battleroyale");
        PlayerSettings.productName = "Battle Royale Stage 1";
        PlayerSettings.companyName = "Ktachicontatocomercial";
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel26;
        EditorUserBuildSettings.buildAppBundle = false;

        var options = new BuildPlayerOptions
        {
            scenes = new[] { scenePath },
            locationPathName = outputPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(options);
        var summary = report.summary;
        Debug.Log($"Android build result: {summary.result}; size: {summary.totalSize} bytes; path: {outputPath}");

        if (summary.result != BuildResult.Succeeded)
        {
            EditorApplication.Exit(1);
            return;
        }

        EditorApplication.Exit(0);
    }
}
