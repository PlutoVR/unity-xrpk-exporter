using System.Text;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEngine;

public class BuildXRPKAfterCompile
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {

        string[] s = Application.dataPath.Split('/');
        string projectName = s[s.Length - 2];

        string pathToManifest = Directory.GetCurrentDirectory() + "/Packages/com.pluto.xrpk-exporter/XRPKData/manifest.json";
        string pathToBatchScript = Directory.GetCurrentDirectory() + "/Packages/com.pluto.xrpk-exporter/XRPKData/create-xrpk.bat";

        // copy default manifest
        // TODO: ingest JSON, change xrpk name, rewrite manifest
        File.Copy(pathToManifest, pathToBuiltProject + "/manifest.json", true);
        UnityEngine.Debug.Log("<color=#FF00FF> XRPK Manifest.json copied to " + pathToBuiltProject + "</color>");


        Process XRPKExecutor = new Process();
        string args = pathToBuiltProject + " " + projectName;

        // Unity logger for cmd line stuff
        var sb = new StringBuilder();
        sb.Append("XRPK Creator Log:\n_________________________________");
        XRPKExecutor.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
        XRPKExecutor.ErrorDataReceived += (sender, args) => sb.AppendLine(args.Data);
        XRPKExecutor.StartInfo.RedirectStandardInput = true;
        XRPKExecutor.StartInfo.RedirectStandardOutput = true;
        XRPKExecutor.StartInfo.RedirectStandardError = true;
        XRPKExecutor.StartInfo.UseShellExecute = false;
        XRPKExecutor.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        XRPKExecutor.StartInfo.FileName = pathToBatchScript;
        XRPKExecutor.StartInfo.Arguments = args;

        UnityEngine.Debug.Log("<color=#FF00FF> Creating XRPK at destination: " + pathToBuiltProject + "</color>");
        XRPKExecutor.Start();
        XRPKExecutor.BeginOutputReadLine();
        XRPKExecutor.BeginErrorReadLine();
        XRPKExecutor.WaitForExit();
        File.Delete(pathToBuiltProject + "/manifest.json");
        UnityEngine.Debug.Log("<color=#FF00FF> Manifest.json deleted from " + pathToBuiltProject + "</color>");

        UnityEngine.Debug.Log(sb);
        UnityEngine.Debug.Log("<color=#FF00FF> XRPK process done, check above for command line output </color>");
    }
}