#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PrefabReplacer : EditorWindow
    {

        [MenuItem("Tools/Prefab Replacer")]
        public static void ShowWindow()
        {
            GetWindow<PrefabReplacer>("Prefab Replacer");
        }

        private GameObject _prefabTemplate;

        private void OnGUI()
        {
            GUILayout.Label("Prefab replacing", EditorStyles.boldLabel);

            _prefabTemplate = (GameObject)EditorGUILayout.ObjectField("Prefab template", _prefabTemplate, typeof(GameObject), false);


            if (GUILayout.Button("Replace objects with prefab"))
            {
                ReplaceAllWithPrefab();
            }
        }

        private void ReplaceAllWithPrefab()
        {
            if (_prefabTemplate == null)
            {
                return;
            }

            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            List<GameObject> similarObjects = new List<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj != _prefabTemplate && obj.name.StartsWith(_prefabTemplate.name))
                {
                    similarObjects.Add(obj);
                }
            }

            int replacedCount = 0;

            foreach (GameObject obj in similarObjects)
            {
                ReplaceObjectWithPrefab(obj, _prefabTemplate);
                replacedCount++;
            }

            UnityEngine.Debug.Log($"Replaced amount: {replacedCount}");

            void ReplaceObjectWithPrefab(GameObject target, GameObject prefab)
            {
                Vector3 position = target.transform.position;
                Quaternion rotation = target.transform.rotation;
                Vector3 scale = target.transform.localScale;
                Transform parent = target.transform.parent;

                GameObject newInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                newInstance.transform.SetParent(parent);
                newInstance.transform.position = position;
                newInstance.transform.rotation = rotation;
                newInstance.transform.localScale = scale;
                newInstance.name = target.name;

                DestroyImmediate(target);
            }
        }
    }
}
#endif