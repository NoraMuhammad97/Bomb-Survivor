#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SO
{
    [CustomEditor(typeof(floatSO))]
    [CanEditMultipleObjects]
    public class floatSOEditor : VariableSOEditor<float>
    {
        public override float GetEditorGUILayoutValue(float Value)
        {
           return EditorGUILayout.FloatField("Modify current Value by: ", Value); ;
        }
    }
}