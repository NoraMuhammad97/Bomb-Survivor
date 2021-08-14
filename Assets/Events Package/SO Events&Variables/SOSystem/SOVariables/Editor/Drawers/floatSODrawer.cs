using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(floatSO), true)]
    public class floatSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, VariableSO variableSO)
        {
            var varRefrence = (floatSO)variableSO;
            varRefrence.Value = EditorGUI.FloatField(position, label, varRefrence.Value);
        }
    }
}