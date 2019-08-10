using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (AssetPopUpAttribute))]
    public class AssetPopUpDrawer : PropertyDrawer
    {
        private List<Object> assets = new List<Object> ();
        private string[] assetNames;
        private int selectionIndex = -1;
        
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = fieldInfo.GetCustomAttribute<AssetPopUpAttribute> ();

            if (!attribute.Type.IsSubclassOf (typeof(Object)))
            {
                EditorGUI.LabelField (position, "Must be used on type derived from UnityEngine.Object!");
                return;
            }
            
            FindAssets (property, attribute);

            var index = EditorGUI.Popup (position, label.text, selectionIndex, assetNames);

            if (index != selectionIndex)
            {
                selectionIndex = index;
                property.objectReferenceValue = assets[index];
            }
        }
        
        private void FindAssets (SerializedProperty property, AssetPopUpAttribute attribute)
        {
            var guids = AssetDatabase.FindAssets ($"t:{attribute.Type.Name}");
            var assetReference = property.objectReferenceValue;
            
            assets.Clear ();
            
            assetNames = new string[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var guid = guids[i];
                var asset = AssetDatabase.LoadAssetAtPath<Object> (AssetDatabase.GUIDToAssetPath (guid));
                assets.Add (asset);
                assetNames[i] = asset.name;

                if (asset == assetReference)
                    selectionIndex = i;
            }
        }
    }

}