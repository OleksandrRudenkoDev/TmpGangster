using UnityEditor;
using UnityEngine;

namespace Base.Editor
{
    public class FindByInstanceIDWindow : EditorWindow
    {

        [MenuItem("Tools/Find By Instance ID")]
        public static void ShowWindow()
        {
            GetWindow<FindByInstanceIDWindow>("Find by ID");
        }

        private string _idText = "";
        private Object _found;

        private void OnGUI()
        {
            GUILayout.Label("Find object by Instance ID", EditorStyles.boldLabel);

            // Ввод ID
            EditorGUI.BeginChangeCheck();
            _idText = EditorGUILayout.TextField("Instance ID", _idText);
            bool enterPressed = Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter);

            using(new EditorGUILayout.HorizontalScope())
            {
                Object obj = EditorGUILayout.ObjectField("Drag object to see ID", null, typeof(Object), true);

                if(obj != null)
                {
                    _idText = obj.GetInstanceID().ToString();
                }

                if(GUILayout.Button("Paste", GUILayout.Width(60)))
                {
                    _idText = EditorGUIUtility.systemCopyBuffer;
                    GUI.FocusControl(null);
                }
            }

            using(new EditorGUILayout.HorizontalScope())
            {
                if(GUILayout.Button("Select") || enterPressed)
                {
                    SelectById();
                }

                using(new EditorGUI.DisabledScope(_found == null))
                {
                    if(GUILayout.Button("Ping"))
                    {
                        EditorGUIUtility.PingObject(_found);
                    }

                    if(GUILayout.Button("Focus"))
                    {
                        Selection.activeObject = _found;
                        EditorGUIUtility.PingObject(_found);
                        Focus();
                    }
                }
            }

            if(_found == null && !string.IsNullOrEmpty(_idText))
            {
                EditorGUILayout.HelpBox("Object not found (maybe it was destroyed or ID is wrong).", MessageType.Info);
            } else if(_found != null)
            {
                EditorGUILayout.ObjectField("Result", _found, typeof(Object), true);
            }
        }

        private void SelectById()
        {
            _found = null;

            if(!int.TryParse(_idText, out int id))
            {
                ShowNotification(new GUIContent("Invalid ID"));

                return;
            }

            Object obj = EditorUtility.InstanceIDToObject(-id);

            if(obj == null)
            {
                obj = EditorUtility.InstanceIDToObject(id);
            }

            if(obj == null)
            {
                ShowNotification(new GUIContent("Object not found"));

                return;
            }

            _found = obj;
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }
    }
}