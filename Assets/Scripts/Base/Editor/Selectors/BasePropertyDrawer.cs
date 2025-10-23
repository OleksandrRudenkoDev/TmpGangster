#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Base.Editor.Selectors
{
    public class BasePropertyDrawer<T> : PropertyDrawer
        where T : class
    {
        private static string [] _constants;
        private static bool _initialized;

        private static void InitializeIfNeeded()
        {
            if (!_initialized)
            {
                List<string> constantsList = new List<string>();
                Type propertyType = typeof(T);

                FieldInfo [] fields = propertyType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                foreach (FieldInfo field in fields)
                {
                    if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
                    {
                        string value = (string)field.GetValue(null);

                        if (!string.IsNullOrEmpty(value))
                        {
                            constantsList.Add(value);
                        }
                    }
                }

                _constants = constantsList.ToArray();
                _initialized = true;
            }
        }

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use class with string fields only");
                return;
            }

            InitializeIfNeeded();

            string currentValue = property.stringValue;
            int currentIndex = GetIndex(currentValue);

            EditorGUI.BeginChangeCheck();
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, _constants);

            if (EditorGUI.EndChangeCheck() && newIndex >= 0 && newIndex < _constants.Length)
            {
                property.stringValue = _constants[newIndex];
            }
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        private int GetIndex (string value)
        {
            for (int i = 0; i < _constants.Length; i++)
            {
                if (_constants[i] == value)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
#endif