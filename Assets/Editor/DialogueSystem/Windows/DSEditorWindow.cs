using DS.Utilities;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace DS.Windows
{
    using System;
    using Utilites;
    
    public class DSEditorWindow : EditorWindow
    {
        private DSGraphView graphView;
        
        private readonly string defaultFileName = "DialoguesFileName";
        private TextField fileNameTextField;
        
        private Button saveButton;
        
        [MenuItem("Window/DS/Dialogue Graph")]
        public static void Open()
        {
           GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void CreateGUI()
        {
            AddGraphView();

            AddToolbar();
            
            AddStyles();
        }

        #region Elements Addition
        private void AddGraphView()
        {
            graphView = new DSGraphView(this);
            
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
            });

            saveButton = DSElementUtility.CreateButton("Save", () => Save());
            
            toolbar.Add(fileNameTextField);
            toolbar.Add(saveButton);
            
            toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");
            
            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }
        #endregion

        #region Toolbar Actions
        private void Save()
        {
            if (string.IsNullOrEmpty(fileNameTextField.value))
            {
                EditorUtility.DisplayDialog(
                    "Invalid file name.",
                    "Please ensure the file name is valid.",
                    "OK"
                );

                return;
            }

            DSIOUtility.Initialize(graphView, fileNameTextField.value);
            DSIOUtility.Save();
        }
        #endregion
        
        #region Utility Methods
        public void EnableSaving()
        {
            saveButton.SetEnabled(true);
        }

        public void DisableSaving()
        {
            saveButton.SetEnabled(false);
        }

        #endregion
    }
}
