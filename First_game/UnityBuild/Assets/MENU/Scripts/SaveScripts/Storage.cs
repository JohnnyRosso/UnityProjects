using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Storage
{
    private string _filePath;
    private BinaryFormatter _binaryFormatter;

    public Storage()
    {
        var _directorty = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(_directorty))
            Directory.CreateDirectory(_directorty);
        _filePath = _directorty + "/GameSave.save";
        InitBinaryFormatter();
    }

    private void InitBinaryFormatter()
    {
        _binaryFormatter = new BinaryFormatter();
    }

    public object Load (object _saveDataByDefault)
    {
        if (!File.Exists(_filePath))
        {
            if (_saveDataByDefault != null)
                Save(_saveDataByDefault);
            return _saveDataByDefault;
        }

        var _file = File.Open(_filePath, FileMode.Open);
        var _savedData = _binaryFormatter.Deserialize(_file);
        _file.Close();
        return _savedData;
    }

    public void Save(object _saveData)
    {
        var _file = File.Create(_filePath);
        _binaryFormatter.Serialize(_file, _saveData);
        _file.Close();
    }
}
