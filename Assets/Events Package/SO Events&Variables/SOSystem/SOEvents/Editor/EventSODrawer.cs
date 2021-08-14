using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO.Events
{
    [CustomPropertyDrawer(typeof(EventSO), true)]
    public class EventSODrawer : ScriptableObjectDrawer
    {
        float helpHeight;
        protected override void OnBuildGui(ref Rect _position, SerializedProperty property, GUIContent label)
        {
            SetPosition(_position);
            EventSO variableSORef = (EventSO)SORef;
            if (variableSORef && SO.Zisettings.Inistance.ShowEventDiscription)//hase value
            {
                Rect prefixPos = EditorGUI.PrefixLabel(position, new GUIContent(" "));
                AddProberty(property);

                prefixPos.y = position.y;
                helpHeight = AddHelp(variableSORef.eventDescription, MessageType.Info);
            }
            else
            {
                base.OnBuildGui(ref position, property, label);
            }
        }

        protected override string getCreatePath()
        {
            return SO.Zisettings.Inistance.EventSOCreatePath;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (SORef && SO.Zisettings.Inistance.ShowEventDiscription)//hase value
            {
                return EditorGUIUtility.singleLineHeight + helpHeight;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }
    }
}