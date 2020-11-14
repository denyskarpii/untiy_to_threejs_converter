using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YamlDotNet.Core;
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
        Debug.Log("Parsed first document=" + yamlDocuments.Documents[0].ToString());

        // Parse to data structure.

        // Create initial Three.js code to create a scene.

        // For each document (---), create code to recreate it in Three.js.
    }
}
