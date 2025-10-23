using System.IO;
using UnityEditor;

namespace Base.Editor
{
    public static class MetaRemoval
    {
        [MenuItem("Tools/Remove Meta Files In Folder")]
        public static void RemoveMetaFiles()
        {
            string folderPath = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");

            if(string.IsNullOrEmpty(folderPath))
            {
                return;
            }

            string [] metaFiles = Directory.GetFiles(folderPath, "*.meta", SearchOption.AllDirectories);

            foreach(string metaFile in metaFiles)
            {
                File.Delete(metaFile);
            }

            AssetDatabase.Refresh();
        }
    }
}