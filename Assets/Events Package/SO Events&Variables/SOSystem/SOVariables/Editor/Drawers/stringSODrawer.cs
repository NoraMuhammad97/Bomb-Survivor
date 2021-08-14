using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(stringSO), true)]
    public class stringSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, VariableSO variableSO)
        {
            var varRefrence = (stringSO)variableSO;
            varRefrence.Value = EditorGUI.TextField(position, label, varRefrence.Value);
        }
    }
}