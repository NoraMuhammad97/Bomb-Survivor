using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(boolSO), true)]
    public class boolSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, VariableSO variableSO)
        {
            var varRefrence = (boolSO)variableSO;
            varRefrence.Value = EditorGUI.Toggle(position, label, varRefrence.Value);
        }
    }
}