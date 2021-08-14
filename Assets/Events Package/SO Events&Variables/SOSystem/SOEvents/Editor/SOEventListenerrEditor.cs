using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SO
{
    [CustomEditor(typeof(EventListenerSO))]
    [CanEditMultipleObjects]
    public class GameEventListenerEditor : Editor
    {
#if UNITY_EDITOR
        int newSize = 0;
        bool defultEditor = false;
        EventListenerSO script;
        private void OnEnable()
        {
            script = (EventListenerSO)target;
            newSize = script.listeners.Count;
        }

        public override void OnInspectorGUI()
        {
            if (SO.Zisettings.Inistance.EventSOListenerDefultView)
            {
                DrawDefaultInspector();
            }
            else
            {
                EditorGUILayout.Separator();
                script = (EventListenerSO)target;
                serializedObject.Update();
                ShowList(serializedObject.FindProperty("listeners"), script.listeners, true, false);
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.Separator();
            GuiLine(1);
            GuiLine(1);
        }
        void GuiLine(int i_height = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }
        public void ShowList(SerializedProperty serialisedList, List<SOListener> list, bool showListSize = true, bool showListLabel = true)
        {
            if (showListLabel)
            {
                EditorGUILayout.PropertyField(serialisedList);
                EditorGUI.indentLevel += 1;
            }
            if (!showListLabel || serialisedList.isExpanded)
            {
                if (showListSize)
                {
                   EditorGUILayout.PropertyField(serialisedList.FindPropertyRelative("Array.size"),new GUIContent("Listeners Number"));
                }

                //draw list element
                var oldLableWidth = EditorGUIUtility.labelWidth;
                for (int i = 0; i < list.Count; i++)
                {
                    var soEvent = list[i].Event;
                    var evName = soEvent != null ? soEvent.name : ("Empty" + i);
                    EditorGUILayout.Separator();
                    if (i < serialisedList.arraySize)

                        EditorGUILayout.PropertyField(serialisedList.GetArrayElementAtIndex(i), new GUIContent(evName));
                    else
                        EditorGUILayout.HelpBox("OutOfIndex" + i, MessageType.Error);


                    if (i != serialisedList.arraySize - 1)
                    {
                        EditorGUILayout.Separator();
                        GuiLine(1);
                        GuiLine(1);

                    }
                }
                EditorGUIUtility.labelWidth = oldLableWidth;
            }

            if (showListLabel)
            {
                EditorGUI.indentLevel -= 1;
            }
        }
#endif
    }
}
