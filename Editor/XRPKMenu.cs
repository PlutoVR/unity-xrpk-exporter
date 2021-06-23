using UnityEditor;
using UnityEngine;
using System.IO;

namespace WebXR.Editor
{
  public class XRPKMenu : UnityEditor.EditorWindow
  {
    public TextAsset packageReference;

    [MenuItem("Window/XRPK/Copy XRPK Template")]
    static void CopyXRPKTemplate()
    {
      if (!EditorUtility.DisplayDialog("Copy XRPK Template", "This action might override your XRPK template. Make sure to have a backup", "Continue", "Cancel"))
      {
        return;
      }
      // Ugly hack to get package path by asset reference
      XRPKMenu xrpkMenu = (XRPKMenu)ScriptableObject.CreateInstance("XRPKMenu");
      string packageAssetFullPath = Path.GetFullPath(AssetDatabase.GetAssetPath(xrpkMenu.packageReference));
      DestroyImmediate(xrpkMenu);
      string packagePath = Path.GetDirectoryName(packageAssetFullPath);

      if (packagePath == null)
      {
        Debug.LogError("Copy failed, could not find package");
        return;
      }
      CopyFolder(Path.Combine(packagePath, "Hidden~"), Application.dataPath);
      AssetDatabase.Refresh();
    }

    // modified version of https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
    private static void CopyFolder(string sourceFolderName, string destFolderName)
    {
      DirectoryInfo directory = new DirectoryInfo(sourceFolderName);

      DirectoryInfo[] directories = directory.GetDirectories();
      if (!Directory.Exists(destFolderName))
      {
        Directory.CreateDirectory(destFolderName);
      }

      FileInfo[] files = directory.GetFiles();
      // In the source repository, it'll throw an error,
      // as it'll try to copy from the same file, to the same file (symlink)
      foreach (FileInfo file in files)
      {
        string temppath = Path.Combine(destFolderName, file.Name);
        try
        {
          file.CopyTo(temppath, true);
        }
        catch (IOException exception)
        {
          Debug.LogError(exception.Message);
        }
      }

      foreach (DirectoryInfo subFolder in directories)
      {
        string temppath = Path.Combine(destFolderName, subFolder.Name);
        CopyFolder(subFolder.FullName, temppath);
      }
    }
  }
}
