using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Utilites;
    
    public class DSEditorWindow : EditorWindow
    {
        [MenuItem("Window/DS/Dialogue Graph")]
        public static void Open()
        {
           GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void CreateGUI()
        {
            AddGraphView();

            AddStyles();
        }

        #region Elements Addition
        private void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView(this);
            
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }
        #endregion


    }
}
