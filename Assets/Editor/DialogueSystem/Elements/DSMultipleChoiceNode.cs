using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Enumerations;
    using Utilites;
    using Windows;
    
    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(dsGraphView, position);

            DialogueType = DSDialogueType.MultipleChoice;
            
            Choices.Add("New Choice");
        }

        public override void Draw()
        {
            base.Draw();

            //Ouput Container
            Button addChoiceButton = DSElementUtility.CreateButton("Add Choice", () =>
            {
                Port choicePort = CreateChoicePort("New Choice");

                Choices.Add("New Choice");
                
                outputContainer.Add(choicePort);
            });
            
            addChoiceButton.AddToClassList("ds-node_button");
            
            mainContainer.Insert(1, addChoiceButton);
            
            foreach (string choice in Choices)
            {
                Port choicePort = CreateChoicePort(choice);
                
                outputContainer.Add(choicePort);
            }
            
            RefreshExpandedState();
        }

        #region Element Creation
        private Port CreateChoicePort(string choice)
        {
            Port choicePort = this.CreatePort();

            choicePort.portName = "";

            Button deleteChoiceButton = DSElementUtility.CreateButton("X");

            deleteChoiceButton.AddToClassList("ds-node_button");

            TextField choiceTextField = DSElementUtility.CreateTextField(choice);

            choiceTextField.AddClasses(
                "ds-node_textfield",
                "ds-node_choice-textfield",
                "ds-node_textfield_hidden"
                );
                
            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceButton);
            return choicePort;
        }
        #endregion
    }    
}

