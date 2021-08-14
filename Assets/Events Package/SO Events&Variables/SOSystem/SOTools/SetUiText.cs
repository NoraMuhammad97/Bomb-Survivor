using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;

public class SetUiText : MonoBehaviour
{
    [Header("data Source")]
    public VariableSO[] DataSources;
    [Header("Settings")]
    [TextArea]
    public string OutputFormat = "{0}/{1}";
    [Header("Text to update")]
    public Text[] UiTextRefrences;

    public void UpdateText()
    {
        List<string> StringData = new List<string>();

        for (int i = 0; i < DataSources.Length; i++)
        {
            if(DataSources[i]  is stringSO)
            {
                StringData.Add(((stringSO)DataSources[i]).GetValue());
            }
            if (DataSources[i] is intSO)
            { 
                StringData.Add(((intSO)DataSources[i]).GetValue().ToString());
            }
        }

        for (int i = 0; i < UiTextRefrences.Length; i++)
        {
            UiTextRefrences[i].text = string.Format(OutputFormat, StringData.ToArray());
            Debug.Log(string.Format(OutputFormat, StringData.ToArray()));
        }
    }

}
