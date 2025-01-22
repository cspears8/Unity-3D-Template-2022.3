using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Enumerations;
    
    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            DialogueType = DSDialogueType.MultipleChoice;
            
            Choices.Add("New Choice");
        }

        public override void Draw()
        {
            base.Draw();

            //Ouput Container
            Button addChoiceButton = new Button()
            {
                text = "Add Choice"
            };
            
            addChoiceButton.AddToClassList("ds-node_button");
            
            mainContainer.Insert(1, addChoiceButton);
            
            foreach (string choice in Choices)
            {
                Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

                choicePort.portName = "";

                Button deleteChoiceButton = new Button()
                {
                    text = "X"
                };

                deleteChoiceButton.AddToClassList("ds-node_button");
                
                TextField choiceTextField = new TextField()
                {
                    value = choice
                };
                
                choiceTextField.AddToClassList("ds-node_textfield");
                choiceTextField.AddToClassList("ds-node_choice-textfield");
                choiceTextField.AddToClassList("ds-node_textfield_hidden");
                
                choicePort.Add(choiceTextField);
                choicePort.Add(deleteChoiceButton);
                
                outputContainer.Add(choicePort);
            }
            
            RefreshExpandedState();
        }
    }    
}

