using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class CharacterList  {
    private List<PlayerTemplate> m_playerCharacters;
    private List<HostileTemplate> m_HostileCharacters;

    public CharacterList()
    {
        m_playerCharacters = new List<PlayerTemplate>();
        m_HostileCharacters = new List<HostileTemplate>();
    }

    public void AddCharacter(PlayerTemplate templateToAdd)
    {
        m_playerCharacters.Add(templateToAdd);
    }

    //Override
    public void AddCharacter(HostileTemplate templateToAdd)
    {
        m_HostileCharacters.Add(templateToAdd);
    }

    public void RemovePlayerCharacter(int index)
    {
        m_playerCharacters.RemoveAt(index);
    }

    public void RemoveHostileCharacter(int index)
    {
        m_HostileCharacters.RemoveAt(index);
    }

    public int CheckIfCharacter(PlayerTemplate character)
    {
        for (int i = 0; i < m_playerCharacters.Count; i++)
        {
            //Checks if an attribute with the same name exists already
            if (m_playerCharacters[i].Name == character.Name)
            {
                return i;
            }
        }

        return -1;
    }

    //Override
    public int CheckIfCharacter(HostileTemplate character)
    {
        for (int i = 0; i < m_HostileCharacters.Count; i++)
        {
            //Checks if an attribute with the same name exists already
            if (m_HostileCharacters[i].Name == character.Name)
            {
                return i;
            }
        }

        return -1;
    }
}

public class SaveLoadCharacters
{
    private CharacterList m_characterList;

    private string m_filePath = Application.persistentDataPath + "/characters.txt";

    public void Save(AttributesList attributesList)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(m_filePath);
        bf.Serialize(file, attributesList);
        file.Close();
    }

    public CharacterList Load()
    {
        if (File.Exists(m_filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_filePath, FileMode.Open);
            m_characterList = (CharacterList)bf.Deserialize(file);
            file.Close();

            return m_characterList;
        }
        else
        {
            Debug.LogWarning("File not Found");
            return new CharacterList();
        }
    }
}
