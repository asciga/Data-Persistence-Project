using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject startMenu;
    public string playerName;

    public string highScoreName;
    public int score;
    public int highScore;
    public static MenuUIHandler Instance;
    public InputField nameInput;
    // Start is called before the first frame update
    private void Awake() 
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerName()
    {
        playerName = nameInput.text;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        startMenu.SetActive(false);
    }

    [System.Serializable]
    class SaveData
    {
        public string savePlayerName;
        public int playerScore;
    }

    public void SaveScore(int scoreToSave)
    {
        SaveData data = new SaveData();
        data.savePlayerName = playerName;
        data.playerScore = scoreToSave;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.savePlayerName;
            highScore = data.playerScore;
        }
    }
}
