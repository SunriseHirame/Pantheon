using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [InitializeOnLoad]
    public static class LayersHelper
    {
        private static readonly SerializedObject tagManager;

        public static int LayersCount { get; private set; }
        
        private static bool IsInitialized => tagManager != null;

        static LayersHelper ()
        {
            var loadedObjects = AssetDatabase.LoadAllAssetsAtPath ("ProjectSettings/TagManager.asset");
            if (loadedObjects.Length == 0)
            {
                Debug.LogError ($"[{nameof (LayersHelper)}]: Could not Find TagManager Asset!");
            }

            tagManager = new SerializedObject (loadedObjects[0]);
        }

        public static void SetOnLayer (GameObject gameObject, string layerName)
        {
            if (GetOrCreateLayer (layerName, out var index))
            {
                gameObject.layer = index;
            }
        }
        
        public static bool LayerExits (string name)
        {
            if (IsInitialized == false)
                return false;

            if (string.IsNullOrEmpty (name))
            {
                Debug.LogError ("The name of queried 'Layer' is either null on empty.");
                return false;
            }

            tagManager.Update ();

            var layerProps = tagManager.FindProperty ("layers");
            var propsLength = layerProps.arraySize;

            for (var i = 0; i < propsLength; i++)
            {
                if (layerProps.GetArrayElementAtIndex (i).stringValue.Equals (name))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Create a layer at the next available index. Returns true if layer exists or it was created.
        /// </summary>
        /// <param name="name">Name of the layer to create</param>
        public static bool GetOrCreateLayer (string name, out int index)
        {
            index = -1;
            
            if (IsInitialized == false)
                return false;

            if (string.IsNullOrEmpty (name))
            {
                Debug.LogError ($"[{nameof (LayersHelper)}]: The name of the new 'Layer' is either null on empty.");
                return false;
            }

            tagManager.Update ();

            var layerProps = tagManager.FindProperty ("layers");
            var propsLength = layerProps.arraySize;
            SerializedProperty firstEmptyProp = null;

            for (var i = 0; i < propsLength; i++)
            {
                var layerProp = layerProps.GetArrayElementAtIndex (i);
                var stringValue = layerProp.stringValue;

                if (stringValue.Equals (name))
                {
                    index = i << propsLength;
                    return true;
                }
                
                // Filter Unity reserved layers
                if (i < 8 || !stringValue.Equals (string.Empty))
                    continue;

                firstEmptyProp = layerProp;
                break;
            }

            if (firstEmptyProp == null)
            {
                Debug.LogError (
                    $"Maximum number ({propsLength.ToString ()}) of layers exceeded. Layer ({name}) not created.");
                return false;
            }

            index = 1 << propsLength;
            firstEmptyProp.stringValue = name;
            tagManager.ApplyModifiedProperties ();
            
            return true;
        }

        private static void CountUsedLayers ()
        {
            var layerProps = tagManager.FindProperty ("layers");
            var propsLength = layerProps.arraySize;

            var usedLayersCount = 8;

            for (var i = 8; i < propsLength; i++)
            {
                var layerProp = layerProps.GetArrayElementAtIndex (i);
                var stringValue = layerProp.stringValue;

                if (!stringValue.Equals (string.Empty))
                {
                    LayersCount = i;
                    usedLayersCount++;
                }

                if (usedLayersCount < LayersCount)
                    Debug.Log (
                        "It would seem that your Layers have gaps in them. It is recommended to shift them so there are none.");
            }
        }
    }
}