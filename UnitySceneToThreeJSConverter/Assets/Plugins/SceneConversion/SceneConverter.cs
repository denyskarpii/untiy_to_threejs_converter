using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

/// <summary>
/// TODO
/// </summary>
public class SceneConverter
{
    private const string CORRECT_SELECTION_INFO = "Please select a single Scene asset to convert.";

    [MenuItem("SceneConverter/Convert Selected Scene To Three.js equivalent")]
    private static void ConvertSelectedSceneToThreejs()
    {
        if (Selection.assetGUIDs.Length == 0)
        {
            Debug.LogWarning("An asset wasn't selected. " + CORRECT_SELECTION_INFO);
            return;
        }

        if (Selection.assetGUIDs.Length > 1)
        {
            Debug.LogWarning("More than one asset can't be converted at the same time. " + CORRECT_SELECTION_INFO);
            return;
        }

        if (!(Selection.activeObject is SceneAsset))
        {
            Debug.LogWarning("A Scene asset wasn't selected to be converted to Three.js. " + CORRECT_SELECTION_INFO);
            return;
        }

        // Read scene file.
        SceneAsset sceneAssetToConvert = Selection.activeObject as SceneAsset;

        string sceneAssetToConvertPath = AssetDatabase.GetAssetPath(sceneAssetToConvert);
        StreamReader input = new StreamReader(sceneAssetToConvertPath);

        IDeserializer deserializer = new DeserializerBuilder().Build();

        Parser parser = new Parser(input);

        YamlStream yamlDocuments = new YamlStream();
        yamlDocuments.Load(parser);

        Debug.Log("Parsed number of documents=" + yamlDocuments.Documents.Count);


        YamlMappingNode[] mappings = new YamlMappingNode[yamlDocuments.Documents.Count];

        for (int i = 0; i < yamlDocuments.Documents.Count; i++)
        {
            mappings[i] = (YamlMappingNode)yamlDocuments.Documents[i].RootNode;
        }

        string test = mappings[0]["OcclusionCullingSettings"]["m_ObjectHideFlags"].ToString();
        Debug.Log(test);

        // Parse to data structure.

        // Create initial Three.js code to create a scene.

        // For each document (---), create code to recreate it in Three.js.
    }

    private class UnityScene
    {
        public OcclusionCullingSettings OcclusionCullingSettings { get; set; }
    }

    private class OcclusionCullingSettings
    {
        public short m_ObjectHideFlags { get; set; }
        public int serializedVersion { get; set; }
        public Dictionary<string, string> m_OcclusionBakeSettings { get; set; }
    }
}
