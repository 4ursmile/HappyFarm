using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManger : MonoBehaviour
{
    public static MainManger Instance;
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
    }
    void SoundPreProces()
    {
        if (isMute == true)
        {
            AudioListener.volume = 0;
            volumimage.sprite = ImageSoundOf;
        } else
        {
            AudioListener.volume = 1;
            volumimage.sprite = ImageSoundOn;
        }
    }
    public void LoadAction()
    {
        string path = Application.persistentDataPath + "/SaveFile.json";
        if (File.Exists(path))
        {
            string Json = File.ReadAllText(path);
            SaveFileUnit saveunit = JsonUtility.FromJson<SaveFileUnit>(Json);
            PlayerController.Instance.CurrentGold = saveunit.CurrentGold;
            isMute = saveunit.isMute;
            SoundPreProces();
        }
    }
    public void SaveFile()
    {
        SaveFileUnit saveFileUnit = new SaveFileUnit();
        saveFileUnit.CurrentGold = PlayerController.Instance.CurrentGold;
        AnimalBase[] a = FindObjectsOfType<AnimalBase>();
        int GoldAdd = 0;
        foreach(var l in a)
        {
            GoldAdd+= l.ValueEachAges[(int)l.AniAge];
        }
        saveFileUnit.CurrentGold = PlayerController.Instance.CurrentGold + GoldAdd;
        saveFileUnit.isMute = isMute;
        string Json = JsonUtility.ToJson(saveFileUnit);
        File.WriteAllText(Application.persistentDataPath + "/SaveFile.json", Json);
    }
    [SerializeField] Sprite ImageSoundOn;
    [SerializeField] Sprite ImageSoundOf;
    [SerializeField] Image volumimage;
    public void MuteButtonPressed()
    {
        if (AudioListener.volume == 0)
        {
            isMute = false;
            AudioListener.volume = 1;
            volumimage.sprite = ImageSoundOn;
        } else
        {
            isMute = true;
            AudioListener.volume = 0;
            volumimage.sprite = ImageSoundOf;
        }
    }
    bool isMute;
    public void MenuButtonPressed()
    {
        SaveFile();
        SceneManager.LoadScene(0);
    }
    public void NewGameButtonPressed()
    {
        isLoad = false;
        SceneManager.LoadScene(1);
    }
    public bool isLoad = false ;
    public void LoadButtonPressed()
    {
        isLoad = true;
        SceneManager.LoadScene(1);
    }
    public void QuitButtonQuit()
    {
        Application.Quit();
    }
}
[Serializable]
public class SaveFileUnit
{
    public int CurrentGold;
    public bool isMute;
}
