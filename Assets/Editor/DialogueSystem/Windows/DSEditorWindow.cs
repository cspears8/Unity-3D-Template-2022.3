using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace DS.Windows
{
    public class DSEditor : EditorWindow
    {
        [MenuItem("Window/DS/Dialogue Graph")]
        public static void Open()
        {
           GetWindow<DSEditor>("Dialogue Graph");
        }

        private void CreateGUI()
        {
            AddGraphView();

            AddStyles();
        }

        private void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView();
            
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {   
            StyleSheet styleSheet = EditorGUIUtility.Load("DialogueSystem/DSVariables.uss") as StyleSheet;
            
            rootVisualElement.styleSheets.Add(styleSheet);
        }
    }
}
