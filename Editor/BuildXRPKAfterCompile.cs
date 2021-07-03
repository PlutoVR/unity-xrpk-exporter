using System.Text;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using UnityEngine;



// the XRPK Manifest, necessary to building it from the WebXR build
[System.Serializable]
public class XRPKManifest
{
    public string name;
    public string repository;
    public string description;
    public string xr_type;
    public string start_url;
}

public class BuildXRPKAfterCompile
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {

        string[] s = Application.dataPath.Split('/');
        string projectName = s[s.Length - 2];
        string pathToBatchScript = Path.GetFullPath("Packages/com.pluto.xrpk-exporter/XRPKData/create-xrpk.bat");

        // copy default manifest
        // TODO: ingest JSON, change xrpk name, rewrite manifest
        XRPKManifest xrpkManifest = new XRPKManifest();
        xrpkManifest.name = projectName;
        xrpkManifest.start_url = "index.html";
        xrpkManifest.description = "A Unity-based XRPK";
        xrpkManifest.xr_type = "webxr-site@0.0.1";

        string xrpkManifestJSON = JsonUtility.ToJson(xrpkManifest);
        string JSONdestPath = pathToBuiltProject + "/manifest.json";
        File.WriteAllText(JSONdestPath, xrpkManifestJSON);
        UnityEngine.Debug.Log("<color=#FFAAFF> XRPK Manifest.json created in " + pathToBuiltProject + "</color>");

        Process XRPKScriptExecutor = new Process();
        string args = pathToBuiltProject + " " + projectName;

        // Unity logger for cmd line stuff
        var sb = new StringBuilder();
        sb.Append("XRPK Creator Log:\n_________________________________");
        XRPKScriptExecutor.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
        XRPKScriptExecutor.ErrorDataReceived += (sender, args) => sb.AppendLine(args.Data);
        XRPKScriptExecutor.StartInfo.RedirectStandardInput = true;
        XRPKScriptExecutor.StartInfo.RedirectStandardOutput = true;
        XRPKScriptExecutor.StartInfo.RedirectStandardError = true;
        XRPKScriptExecutor.StartInfo.UseShellExecute = false;
        XRPKScriptExecutor.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        XRPKScriptExecutor.StartInfo.FileName = pathToBatchScript;
        XRPKScriptExecutor.StartInfo.Arguments = args;

        UnityEngine.Debug.Log("<color=#FFAAFF> Creating XRPK at destination: " + pathToBuiltProject + "</color>");
        XRPKScriptExecutor.Start();
        XRPKScriptExecutor.BeginOutputReadLine();
        XRPKScriptExecutor.BeginErrorReadLine();
        XRPKScriptExecutor.WaitForExit();

        //remove manifest after compiling XRPK
        // File.Delete(JSONdestPath);
        // UnityEngine.Debug.Log("<color=#FFAAFF> Manifest.json deleted from " + pathToBuiltProject + "</color>");

        UnityEngine.Debug.Log(sb);
        UnityEngine.Debug.Log("<color=#FFAAFF> XRPK process done, check above for output </color>");
    }
}