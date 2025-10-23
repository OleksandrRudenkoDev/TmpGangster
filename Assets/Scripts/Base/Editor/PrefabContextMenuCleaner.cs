using UnityEditor;
using UnityEngine;

namespace Base.Editor
{
    public static class PrefabContextMenuCleaner
    {
        [MenuItem("Assets/Clean Prefab/All Prefabs in Folder", false, 32)]
        private static void CleanAllPrefabsInFolder()
        {
            var selectedObjects = Selection.objects;

            foreach (var selected in selectedObjects)
            {
                string path = AssetDatabase.GetAssetPath(selected);

                if (AssetDatabase.IsValidFolder(path))
                {
                    CleanPrefabsInFolder(path, true);
                }
            }
        }

        private static void CleanPrefabsInFolder(string folderPath, bool includeChildren)
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
            int totalRemoved = 0;

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                int removed = CleanPrefab(path, includeChildren);

                if (removed >= 0)
                {
                    totalRemoved += removed;
                }
            }

            AssetDatabase.Refresh();
            UnityEngine.Debug.Log($"Removal completed. Total removed: {totalRemoved}");
        }

        private static int CleanPrefab(string prefabPath, bool includeChildren)
        {
            try
            {
                GameObject prefabInstance = PrefabUtility.LoadPrefabContents(prefabPath);

                if (prefabInstance == null) return -1;

                int removedCount = RemoveMissingComponents(prefabInstance, includeChildren);

                if (removedCount > 0)
                {
                    PrefabUtility.SaveAsPrefabAsset(prefabInstance, prefabPath);
                }

                PrefabUtility.UnloadPrefabContents(prefabInstance);

                return removedCount;
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"Ошибка при очистке префаба {prefabPath}: {e.Message}");

                return -1;
            }
        }

        private static int RemoveMissingComponents(GameObject target, bool recursive)
        {
            int removedCount = 0;

            removedCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(target);

            if (recursive)
            {
                foreach (Transform child in target.transform)
                {
                    removedCount += RemoveMissingComponents(child.gameObject, true);
                }
            }

            return removedCount;
        }
    }
}