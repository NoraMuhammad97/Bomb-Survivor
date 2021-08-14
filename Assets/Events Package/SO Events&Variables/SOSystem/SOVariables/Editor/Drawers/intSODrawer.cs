using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SO
{
    [CustomPropertyDrawer(typeof(intSO), true)]
    public class intSODrawer : IVariableSODrawer
    {
        protected override void VraiableField(Rect position, GUIContent label, VariableSO variableSO)
        {
            var varRefrence = (intSO)variableSO;
            varRefrence.Value = EditorGUI.IntField(position, label, varRefrence.Value);
        }
    }
}