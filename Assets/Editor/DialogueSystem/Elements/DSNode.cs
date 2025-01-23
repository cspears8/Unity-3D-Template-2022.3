using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DS.Elements
{
    using Enumerations;
    using Utilites;
    using Windows;
    
    public class DSNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DSDialogueType DialogueType { get; set; }
        public Group Group { get; set; }

        private DSGraphView graphView;
        private Color defaultBackgroundColor;
        
        public virtual void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            DialogueName = "Dialogue Name";
            Choices = new List<string>();
            Text = "Dialogue text.";

            graphView = dsGraphView;
            defaultBackgroundColor = new Color(29f / 255, 29f / 255, 30f / 255);
            
            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList("ds-node_main-container");
            extensionContainer.AddToClassList("ds-node_extension-container");
        }

        public virtual void Draw()
        {
            //Title Container
            TextField dialogueNameTextField = DSElementUtility.CreateTextField(DialogueName, callback =>
            {
                if (Group == null)
                {
                    graphView.RemoveUngroupedNode(this);

                    DialogueName = callback.newValue;

                    graphView.AddUngroupedNode(this);
                    
                    return;
                }

                Group currentGroup = Group;
                
                graphView.RemoveGroupedNode(this, Group);

                DialogueName = callback.newValue;
                
                graphView.AddGroupedNode(this, currentGroup);
            });

            dialogueNameTextField.AddClasses(
                "ds-node_textfield",
                "ds-node_filename-textfield",
                "ds-node_textfield_hidden"
                );
            
            titleContainer.Insert(0, dialogueNameTextField);

            /* INPUT CONTAINER */
            Port inputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);

            inputPort.portName = "Dialogue Connection";

            inputContainer.Add(inputPort);
            
            /* EXTENSIONS CONTAINER */
            VisualElement customDataContainer = new VisualElement();
            
            customDataContainer.AddToClassList("ds-node_custom-data-container");

            Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text");
            
            TextField textTextField = DSElementUtility.CreateTextArea(Text);
            
            textTextField.AddClasses(
                "ds-node_textfield",
                "ds-node_quote-textfield"
                );
            
            textFoldout.Add(textTextField);

            customDataContainer.Add(textFoldout);
            
            extensionContainer.Add(customDataContainer);
        }

        public void SetErrorStyle(Color color)
        {
            mainContainer.style.backgroundColor = color;
        }

        public void ResetStyle()
        {
            mainContainer.style.backgroundColor = defaultBackgroundColor;
        }
    }
}
