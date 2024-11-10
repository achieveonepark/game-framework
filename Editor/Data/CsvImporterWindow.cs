using System.IO;
using Gilzoide.SqliteAsset.Csv;
using SQLite;
using UnityEditor;
using UnityEngine;

public class CsvImporterWindow : EditorWindow
{
    private static CsvImporterWindow _instance;
    private string _csvContent;
    private string _csvPath;
    private DefaultAsset _dbAsset;
    private string _tableName;

    private void OnEnable()
    {
        ValidateAssets();
    }

    private void OnGUI()
    {
        DrawFilePath();
        DrawCsvContent();
        DrawInsertButton();
    }

    [MenuItem("GameFramework/Data/CsvImporter")]
    private static void ShowWindow()
    {
        _instance = GetWindow<CsvImporterWindow>("CSV Importer");
        _instance.minSize = _instance.maxSize = new Vector2(400, 300);
    }

    private void DrawFilePath()
    {
        ValidateAssets();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("SQLite DB", EditorStyles.boldLabel);
        _dbAsset = (DefaultAsset)EditorGUILayout.ObjectField("Database Asset", _dbAsset, typeof(DefaultAsset), false);

        EditorGUILayout.Space(5);
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("CSV File Path", GUILayout.Width(150));

            GUI.enabled = false;
            EditorGUILayout.TextField(_csvPath.Replace(Application.dataPath, ""));
            GUI.enabled = true;

            if (GUILayout.Button("Select"))
                _csvPath = EditorUtility.OpenFilePanel("CSV File", Application.dataPath, "csv");
        }

        _tableName = EditorGUILayout.TextField("Table Name", _tableName);
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetString("csvAssetPath", _csvPath);
            SaveAssetPath("dbAssetPath", _dbAsset);
        }
    }

    private void DrawCsvContent()
    {
        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("CSV 데이터 미리보기", EditorStyles.boldLabel);
        GUI.enabled = false;
        EditorGUILayout.TextField(_csvContent);
        GUI.enabled = true;
    }

    private void DrawInsertButton()
    {
        GUILayout.FlexibleSpace();
        GUI.enabled = string.IsNullOrEmpty(_csvPath) is false && _dbAsset != null &&
                      string.IsNullOrEmpty(_tableName) is false;
        if (GUILayout.Button("Insert!"))
        {
            var reader = File.OpenText(_csvPath);
            var dbAssetPath = AssetDatabase.GetAssetPath(_dbAsset);
            var db = new SQLiteConnection(dbAssetPath);
            db.ImportCsvToTable(_tableName, reader);

            EditorUtility.DisplayDialog("CSV Importer", $"{_tableName}데이터를 임포트했어요.", "OK");
        }

        GUI.enabled = true;
    }

    private void SaveAssetPath(string key, Object asset)
    {
        var path = AssetDatabase.GetAssetPath(asset);
        EditorPrefs.SetString(key, path);
    }

    private void ValidateAssets()
    {
        _csvPath ??= EditorPrefs.GetString("csvAssetPath", "");
        _dbAsset ??= AssetDatabase.LoadAssetAtPath<DefaultAsset>(EditorPrefs.GetString("dbAssetPath", ""));
    }
}