using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Editor.SceneSelection
{
    [InitializeOnLoad]
    public static class ToolbarScenesDropdown
    {
        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            GUIStyle toolbarButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageLeft,
                fixedWidth = 120
            };

            Rect buttonRect = GUILayoutUtility.GetRect(new GUIContent(SceneManager.GetActiveScene().name), toolbarButtonStyle);

            if (GUILayout.Button(new GUIContent(SceneManager.GetActiveScene().name, "Выбрать сцену"), toolbarButtonStyle))
            {
                GenericMenu menu = new GenericMenu();
                EditorBuildSettingsScene [] scenes = EditorBuildSettings.scenes;

                foreach (EditorBuildSettingsScene scene in scenes)
                {
                    string scenePath = scene.path;
                    string sceneName = Path.GetFileNameWithoutExtension(scenePath);
                    menu.AddItem(new GUIContent(sceneName), false, () => OpenScene(scenePath));
                }

                menu.DropDown(new Rect(buttonRect.x + buttonRect.width, buttonRect.y + buttonRect.height, 0, 0));
                GUIUtility.ExitGUI();
            }
        }

        private static void OpenScene (string scenePath)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }

        static ToolbarScenesDropdown()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }
    }
}