#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace TAG.Editor
{
    public static class AndroidBuild
    {
        private const string OutputPath = "Builds/Android/TAG.apk";

        [MenuItem("TAG/Build/Android APK")]
        public static void BuildAndroidApk()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.tagstudio.tag");
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

            var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
            var report = BuildPipeline.BuildPlayer(scenes, OutputPath, BuildTarget.Android, BuildOptions.None);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new System.Exception($"Android build failed: {report.summary.result}");
            }
        }
    }
}
#endif
