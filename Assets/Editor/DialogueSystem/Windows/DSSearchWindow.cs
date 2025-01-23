using System.Collections;
using System.Collections.Generic;
using DS.Windows;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    using Enumerations;
    using Elements;
    
    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView graphView;
        private Texture2D indentationIcon;
        
        public void Initialize(DSGraphView dsGraphView)
        {
            graphView = dsGraphView;

            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntires = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Node"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
                {
                    level = 2,
                    userData = DSDialogueType.SingleChoice
                },
                new SearchTreeEntry(new GUIContent("Mulitple Choice", indentationIcon))
                {
                level = 2,
                userData = DSDialogueType.MultipleChoice
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Group"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
                {
                    level = 2,
                    userData = new Group()
                }
            };

            return searchTreeEntires;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = graphView.GetLocalMousePosition(context.screenMousePosition, true);
            switch (searchTreeEntry.userData)
            {
                case DSDialogueType.SingleChoice:
                {
                    DSSingleChoiceNode singleChoiceNode = graphView.CreateNode(DSDialogueType.SingleChoice, localMousePosition) as DSSingleChoiceNode;
                    
                    graphView.AddElement(singleChoiceNode);
                    
                    return true;
                }
                case DSDialogueType.MultipleChoice:
                {
                    DSMultipleChoiceNode multipleChoiceNode = graphView.CreateNode(DSDialogueType.MultipleChoice, localMousePosition) as DSMultipleChoiceNode;

                    graphView.AddElement(multipleChoiceNode);
                    
                    return true;
                }
                case Group _:
                {
                    Group group = graphView.CreateGroup("DialogueGroup", localMousePosition);
                    
                    graphView.AddElement(group);
                   
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }
    }

}
