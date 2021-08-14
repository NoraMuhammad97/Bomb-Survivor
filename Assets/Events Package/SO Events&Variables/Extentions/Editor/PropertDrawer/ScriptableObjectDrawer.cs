using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawerExtended
{

    protected ScriptableObject SORef;
    protected int OriginalIndent;
    protected Rect leftRow;
    protected Rect rightRow;
    protected Color color;
    protected bool isInArrayView;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //init --------------------------------------------------
        label = EditorGUI.BeginProperty(position, label, property);
        SORef = (ScriptableObject)property.objectReferenceValue;
        isInArrayView = OriginalIndent > 0 && label.text.StartsWith("Element");
        label.text = (SORef && isInArrayView) ? SORef.name : label.text; // check if the property is in arrray or if no variable assigned to show the variable name as label instead of 'Element' word
        OriginalIndent = EditorGUI.indentLevel;

        //build GUI--------------------------------------------------
        OnBuildGui(ref position, property, label);

        //finalize --------------------------------------------------
        EditorGUI.indentLevel = OriginalIndent;
        EditorGUI.EndProperty();
    }

    protected virtual void OnBuildGui(ref Rect position, SerializedProperty property, GUIContent label)
    {
        if (!SORef && SO.Zisettings.Inistance.ShowAssignButton)
        {
            CalculateLAyout(ref position);

            //  uncomment to debug draw area ------------------------------------------------------
            // Debug(position);

            //first field ----------------------------------------------
            CreateFirstFIeld(ref position, property, label);

            //secound field-------------------------------------------
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = .1f;
            CreateSecoundField(property, label);
        }
        else
        {
            AddProberty(ref position, property, label);
        }
    }

    protected void CalculateLAyout(ref Rect position)
    {
        float padding = 2f;
        //left row rect
        leftRow = position;
        var totalWidth = position.width;
        leftRow.width = totalWidth * .80f - padding;
        //right row rect
        rightRow = position;
        rightRow.width = totalWidth - padding - leftRow.width;
        rightRow.x += padding + leftRow.width;
    }

    protected void DebugDrawArea(Rect position)
    {
        //if (debug)
        //{
        color = Color.green;
        color.a = .5f;
        DrawColorBox(position, color);
        color = Color.blue;
        color.a = .5f;
        DrawColorBox(leftRow, color);
        color = Color.red;
        color.a = .5f;
        DrawColorBox(rightRow, color);
        //}
    }

    private void CreateFirstFIeld(ref Rect position, SerializedProperty property, GUIContent label)
    {
        if (!isInArrayView)
        {
            AddProberty(ref leftRow, property, label);
        }
        else
        {
            AddProberty(ref position, property, label);
        }
    }
    private void CreateSecoundField(SerializedProperty property, GUIContent label)
    {
        if (getSoType(property) != null && !isInArrayView)
        {
            if (GUI.Button(rightRow, "Assign"))
            {
                UnityEngine.Object soVar = AutoAssignSO(label.text, getCreatePath(), property);
                //assing to the current property
                property.objectReferenceValue = soVar;
            }
        }
    }

    protected virtual string getCreatePath()
    {
        return SO.Zisettings.Inistance.SOCreatePath;
    }
}
