using UnityEngine;
using UnityEditor;
using System.Linq;

[InitializeOnLoad]
public class AddUnityAPI : Editor
{
    static AddUnityAPI()
    {
        string[] assemblyPaths = AssetDatabase.FindAssets("UnityEngine.CoreModule")
            .Select(AssetDatabase.GUIDToAssetPath)
            .SelectMany(path => AssetDatabase.LoadAssetAtPath<MonoScript>(path).GetClass().Assembly.GetReferencedAssemblies())
            .Select(name => {
                try {
                    return AssetDatabase.FindAssets(name + " t:asmdef")
                        .Select(AssetDatabase.GUIDToAssetPath)
                        .First();
                } catch {
                    return null;
                }
            })
            .Where(path => path != null)
            .Distinct()
            .ToArray();
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.WebGL, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.WSA, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.PS4, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.XboxOne, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.tvOS, ApiCompatibilityLevel.NET_4_6);
PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Switch, ApiCompatibilityLevel.NET_4_6);
        foreach (string assemblyPath in assemblyPaths)
        {
            PluginImporter pluginImporter = AssetImporter.GetAtPath(assemblyPath) as PluginImporter;
            if (pluginImporter != null)
            {
                pluginImporter.SetCompatibleWithEditor(true);
                pluginImporter.SetCompatibleWithAnyPlatform(false);
                pluginImporter.SetPlatformData(BuildTarget.StandaloneWindows, "CPU", "AnyCPU");
                pluginImporter.SetPlatformData(BuildTarget.StandaloneWindows64, "CPU", "AnyCPU");
            }
        }
    }
}