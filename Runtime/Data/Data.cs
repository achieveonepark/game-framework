using System.IO;
using GameFramework;
using SQLite;
using UnityEngine.Device;

public static class Data
{
    private static SQLiteConnection _db;

    private static bool _isInitialized => _db != null;
    
    public static void Initialize()
    {
        var settings = GScriptableObject.GetOrAdd<InitializeSettings>();
        var path = $"{Application.dataPath}/{settings.DBPath}";
        
        _db = new SQLiteConnection(path);
        GameLog.Debug("[Data] Initialized");
    }

    public static void CreateTable<T>()
    {
        if (_isInitialized is false)
        {
            return;
        }
        
        _db.CreateTable<T>();
    }

    public static void DropTable<T>()
    {
        if (_isInitialized is false)
        {
            return;
        }
        _db.DropTable<T>();
    }

    public static T Get<T> (object id) where T : new()
    {
        if (_isInitialized is false)
        {
            return default;
        }
        
        var data = _db.Query<T>($"SELECT COUNT(1) FROM {typeof(T).Name} WHERE id = ?", id);

        // var data = _db.Get<T>(id);

        return data[0];
    }
    
    public static bool Exists(string tableName, object id)
    {
        string query = $"SELECT COUNT(1) FROM {tableName} WHERE ID = ?";
        int count = (int)_db.ExecuteScalar<object>(query, id);
        return count > 0;
    }
    
    public static bool TryGetValue<TValue, TKey>(string tableName, TKey id, out TValue result) where TValue : new()
    {
        if (_isInitialized is false)
        {
            result = default;
            return false;
        }
        
        string query = $"SELECT COUNT(1) FROM {tableName} WHERE ID = ?";
        int count = _db.ExecuteScalar<int>(query, id);
        if(count > 0)
        {
            result = Get<TValue>(id);
            return true;
        }
        result = default(TValue);
        return false;
    }

    public static void Insert<T> (T data) where T : new()
    {
        if (_isInitialized is false)
        {
            return;
        }
        
        _db.InsertOrReplace(data);
    }
}
