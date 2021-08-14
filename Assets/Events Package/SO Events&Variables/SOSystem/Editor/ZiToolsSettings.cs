using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SO.Events;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace SO
{

    //settings serializable object 
    [System.Serializable]
    public class Zisettings
    {
        //singelton -------------------------------------
        private static Zisettings inistance = null;
        public static Zisettings Inistance { get { return Load(); } }
        private Zisettings() { }

        //Stored data -----------------------------------

        //ScriptableObjects Drawer
        public bool ShowAssignButton { get => showAssignButton; set { if (showAssignButton != value) { showAssignButton = value; isDirty = true; } } }
        [SerializeField]
        private bool showAssignButton = true;

        public string SOCreatePath { get => soCreatePath; set { if (soCreatePath != value) { soCreatePath = value; isDirty = true; } } }
        [SerializeField]
        private string soCreatePath = "";

        public string EventSOCreatePath { get => eventSOCreatePath; set { if (eventSOCreatePath != value) { eventSOCreatePath = value; isDirty = true; } } }
        [SerializeField]
        private string eventSOCreatePath = "";

        public string VarSOCreatePath { get => varSOCreatePath; set { if (varSOCreatePath != value) { varSOCreatePath = value; isDirty = true; } } }
        [SerializeField]
        private string varSOCreatePath = "";




        //eventSO Drawer
        public bool ShowEventDiscription { get => showEventDiscription; set { if (showEventDiscription != value) { showEventDiscription = value; isDirty = true; } } }
        [SerializeField]
        private bool showEventDiscription = true;
        public bool EventSOListenerDefultView { get => eventSOListenerDefultView; set { if (eventSOListenerDefultView != value) { eventSOListenerDefultView = value; isDirty = true; } } }
        [SerializeField]
        private bool eventSOListenerDefultView = true;
        public bool AllowEditListenersFromEvents { get => allowEditListenersFromEvents; set { if (allowEditListenersFromEvents != value) { allowEditListenersFromEvents = value; isDirty = true; } } }
        [SerializeField]
        private bool allowEditListenersFromEvents = true;

        //varSO
        public bool ShowVarSOValue { get => showVarSOValue; set { if (showVarSOValue != value) { showVarSOValue = value; isDirty = true; } } }

       

        [SerializeField]
        private bool showVarSOValue = true;



        //save & load ziSettings --------------------------------
        private bool isDirty = false;
        private static string playerPrefsKey = "ziSettings";


        internal static void Save()
        {
            //Zisettings save if inistance.isDirty
            if (inistance.isDirty)
            {
                var json = JsonUtility.ToJson(inistance, true);
                PlayerPrefs.SetString(playerPrefsKey, json);
                inistance.isDirty = false;
            }
        }
        private static Zisettings Load()
        {
            if (inistance == null)
            {
                if (PlayerPrefs.HasKey(playerPrefsKey))
                {
                    var json = PlayerPrefs.GetString(playerPrefsKey);
                    if (json == null) //json updated
                    {
                        //delete old deprecated data
                        PlayerPrefs.DeleteKey(playerPrefsKey);
                    }
                    else
                    {
                        //Zisettings was found"
                        inistance = JsonUtility.FromJson<Zisettings>(json);
                    }
                }
                else
                {
                    //Zisettings first time
                    inistance = new Zisettings();
                }
            }
            //Zisettings cashed inistance"
            return inistance;
        }
    }


    //Display all GameEvents in the current scene - show which objects are using them
    //Include a description of what the Event does
    [ExecuteInEditMode]
    public class ZiToolsSettings : EditorWindow
    {
#if UNITY_EDITOR
        //Values for scrollPos
        private Vector2 scrollPos;

        public static Zisettings ziSettings;

        //Toolbar variables
        private int toolbarInt = 0;
        private string[] toolbarStrings = { "Settings", "debug" };

        [MenuItem("Window/ZiToolsSettings")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one does not exist, make one
            ZiToolsSettings window = (ZiToolsSettings)EditorWindow.GetWindow(typeof(ZiToolsSettings));
            window.Show();
        }

        private void OnEnable()
        {
            ziSettings = Zisettings.Inistance;
        }

        private void OnDisable()
        {
            Zisettings.Save();
        }

        private void OnGUI()
        {
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
            ziSettings = Zisettings.Inistance;
            const float padding = 1f;
            GUILayout.BeginHorizontal();//layout 
            GUILayout.Label("", GUILayout.Width(padding));//left padding

            GUILayout.BeginVertical();//main content
            //Switch based on the current selected toolbar button
            switch (toolbarInt)
            {
                case 0:
                    GUILayout.Label("General");
                    ziSettings.ShowAssignButton = GUILayout.Toggle(ziSettings.ShowAssignButton, new GUIContent("Show Assign button"));

                    if (ziSettings.ShowAssignButton)
                    {
                        ziSettings.SOCreatePath = SelectPath("Create SO assets at:", ziSettings.SOCreatePath);
                        ziSettings.VarSOCreatePath = SelectPath("Create VarSO assets at:", ziSettings.VarSOCreatePath);
                        ziSettings.EventSOCreatePath = SelectPath("Create eventSo assets at:", ziSettings.EventSOCreatePath);
                    }

                    GUILayout.Space(5f);
                    GUILayout.Label("eventSO");
                    ziSettings.ShowEventDiscription = GUILayout.Toggle(ziSettings.ShowEventDiscription, new GUIContent("Show soEvent discription"));
                    ziSettings.EventSOListenerDefultView = GUILayout.Toggle(ziSettings.EventSOListenerDefultView, new GUIContent("EventSO listener native view"));
                    ziSettings.AllowEditListenersFromEvents = GUILayout.Toggle(ziSettings.AllowEditListenersFromEvents, new GUIContent("Allow Edit Listeners From Events directly"));

                    GUILayout.Space(5f);
                    GUILayout.Label("soVariable");
                    ziSettings.ShowVarSOValue = GUILayout.Toggle(ziSettings.ShowVarSOValue, new GUIContent("Show varSO value"));
                    break;
                case 1:
                    GUILayout.TextArea(JsonUtility.ToJson(ziSettings, true));
                    break;
            }
            GUILayout.EndVertical();

            GUILayout.Label("", GUILayout.Width(padding));//right padding
            GUILayout.EndHorizontal();
        }

        private static string SelectPath(string label, string originalPath, string buttonText = "select")
        {
            var _label = new GUIContent(label);
            var _originalPath = new GUIContent("Assets/" + originalPath);
            var _buttonText = new GUIContent(buttonText);
            GUIStyle style = new GUIStyle();
            float labelWIdth = 300f;
            float originalPathWIdth = 300f;
            float buttonTextWIdth = 300f;
            float temp;
            style.CalcMinMaxWidth(_label, out labelWIdth, out temp);
            style.CalcMinMaxWidth(_originalPath, out originalPathWIdth, out temp);
            style.CalcMinMaxWidth(_buttonText, out buttonTextWIdth, out temp);

            GUILayout.BeginHorizontal();
            GUILayout.Label(_label, style, GUILayout.Width(labelWIdth + 5));
            GUILayout.Label(_originalPath, style, GUILayout.Width(originalPathWIdth));
            GUILayout.Label("");

            if (GUILayout.Button(buttonText, GUILayout.Width(buttonTextWIdth + 20)))
            {
                string newPath = EditorUtility.OpenFolderPanel("Create SoObjects at?", "Assets/" + originalPath, "");
                newPath = ReformatThePath(newPath);
                if (newPath != null)
                {
                    originalPath = newPath;
                }
            }
            GUILayout.EndHorizontal();
            return originalPath;
        }

        private static string ReformatThePath(string path)
        {
            var assetsDir = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Assets";
            if (path.Length > 0)
            {
                if (path.Contains(assetsDir))
                {
                    path = path.Replace(assetsDir, "");
                    if (path.Length > 0)
                    {
                        if (path[0] == '/')
                        { path = path.Remove(0, 1); }
                        else
                        { path = ""; }
                    }
                    return path;
                }
                else
                {
                    Debug.LogError("You cant select folder outside Assets/");
                }
            }
            return null;
        }

        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("Assets/ZiTools/SetCreatePath/SoObjects")]
        static void setPathSO(MenuCommand command)
        {
            ziSettings.SOCreatePath = getSelectedPath();
        }

        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("Assets/ZiTools/SetCreatePath/varSO")]
        static void setPathVarSO(MenuCommand command)
        {
            ziSettings.VarSOCreatePath = getSelectedPath();
        }

        // Add a menu item called "Double Mass" to a Rigidbody's context menu.
        [MenuItem("Assets/ZiTools/SetCreatePath/eventSO")]
        static void setPath(MenuCommand command)
        {
            ziSettings.EventSOCreatePath = getSelectedPath();
        }

        private static string getSelectedPath()
        {
            var selected = Selection.activeObject;
            var path = AssetDatabase.GetAssetPath(selected);
            if (path.Contains("."))//is file
            {
                var fileNameSTartIndex = path.LastIndexOf('/');
                path = path.Remove(fileNameSTartIndex, path.Length - 1 - fileNameSTartIndex);
            }
            path = path.Remove(0, "Assets/".Length);
            return path;
        }



        /// <summary>
        /// If the scene is changed then call the GetAllScriptableEventsInScene
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        private void SceneUpdated(Scene current, Scene next)
        {

        }

        /// <summary>
        /// If the Hierarchy has changed then call the GetAllScriptableEventsInScene
        /// </summary>
        private void HierarchyChanged()
        {

        }


#endif
    }
}