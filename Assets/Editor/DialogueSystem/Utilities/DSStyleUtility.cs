using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Utilites
{
    public static class DSStyleUtility
    {
        public static VisualElement AddClasses(this VisualElement element, params string[] classNames)
        {
            foreach(string className in classNames)
            {
                element.AddToClassList(className);
            }

            return element;
        }
        
        public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetNames)
        {
            foreach (string styleSheetName in styleSheetNames)
            {
                StyleSheet styleSheet = EditorGUIUtility.Load(styleSheetName) as StyleSheet;
            
                element.styleSheets.Add(styleSheet);
            }

            return element;
        }
    }
}


