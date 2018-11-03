using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveLoadAttributes {
    public static AttributesList m_attributesList;

    private string m_filePath = Application.persistentDataPath + "/attributes.txt";

    public void Save(AttributesList attributesList)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(m_filePath);
        bf.Serialize(file, attributesList);
        file.Close();
    }

    public AttributesList Load()
    {
        if (File.Exists(m_filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_filePath, FileMode.Open);
            m_attributesList = (AttributesList)bf.Deserialize(file);
            file.Close();

            return m_attributesList;
        }
        else
        {
            Debug.LogWarning("File not Found");
            return new AttributesList();
        }
    }
}
