using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameStateManager : UnitySingleton<GameStateManager>
{
    private const string SERVER_URL = "http://midiapi.espol.edu.ec";
    //private const string SERVER_URL = "http://hidden-wildwood-12729.herokuapp.com";
    private const string API_URL = SERVER_URL + "/api/v1/entrance/AlmacenarDatosController";

    private List<string> jsonList = new List<string>();
    private bool IsConnectedToServer = false;



    // Use this for initialization
    IEnumerator Start()
    {
        print(API_URL);
        if (CheckNet()) yield return StartCoroutine(CheckServer());
        print(Application.persistentDataPath);
        ReadLocalFile();
        StartCoroutine(SyncJsonData());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GoBack();
        //}
    }

    public void LoadScene(string sceneName)
    {
        AudioManager.Instance.StopMusic("BGM");
        AudioManager.Instance.StopAllVoices();
        SceneManager.LoadScene(sceneName);
    }

    //public void GoBack()
    //{
    //    int index = getCurrentSceneIndex();
    //    if (index < 1)
    //    {
    //        Application.Quit();
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene(index - 1);
    //    }
    //}

    public int getCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ReloadCurrentScene()
    {
        LoadScene(getCurrentSceneName());
    }


    public void PrintJsonList()
    {
        foreach (string jsonItem in jsonList)
        {
                print("JSONList["+ jsonList.IndexOf(jsonItem) +"] = "+jsonItem);
        }
    }


    public bool CheckNet()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            return true;
        }
        Debug.Log("Error. Check internet connection!");
        return false;   
    }

    IEnumerator CheckServer()
    {
        WWW www = new WWW(SERVER_URL);
        Debug.Log("CONNECTING TO SERVER...");
        yield return StartCoroutine("StartUpload", www);

        if (www.error != null)
        {
            Debug.Log("THIS IS AN ERROR: " + www.error);
            IsConnectedToServer = false;
        }
        else
        {
            Debug.Log("CONNECTED TO SERVER");
            IsConnectedToServer = true;
        }
    }

    public void PingFinished(Ping p)
    {
        print(p);
    }

    public void AddJsonToList(string json)
    {
        ReadLocalFile();
        jsonList.Add(json);
        StartCoroutine(SyncJsonData());
    }

    public IEnumerator SyncJsonData()
    {
        //List<string> jsonList = ReadLocalFile();
        //Debug.Log("New JSON: " + json);
        PrintJsonList();
        yield return CheckServer();

        if (CheckNet() && IsConnectedToServer)
        {
            
            Debug.Log("READY TO UPLOAD");
            List<string> sendedJsonItems = new List<string>();

            foreach (string jsonItem in jsonList)
            {

                Dictionary<string, string> postHeader = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" }
                };
                byte[] body = Encoding.UTF8.GetBytes(jsonItem);
                WWW www = new WWW(API_URL, body, postHeader);
                Debug.Log("CONNECTING TO SERVER...");
                yield return StartCoroutine("StartUpload", www);

                if (www.error != null)
                {
                    Debug.Log("THIS IS AN ERROR: " + www.error);
                    IsConnectedToServer = false;
                    break;
                }
                else
                {
                    Debug.Log("Data Submitted");
                    sendedJsonItems.Add(jsonItem);
                }
            }

            print(sendedJsonItems.Count + " FILE(S) UPLOADED");

            foreach (string sended in sendedJsonItems)
            {
                jsonList.Remove(sended);
            }

            sendedJsonItems.Clear();

        }
        else
        {
            Debug.Log("CONNECTION NOT ESTABLISHED");
        }

        print(jsonList.Count + " JSON(S) NOT UPLOADED");
        

        SaveLocal();

    }

    //bool UploadItem(string item)
    //{
    //    Dictionary<string, string> postHeader = new Dictionary<string, string>
    //            {
    //                { "Content-Type", "application/json" }
    //            };
    //    byte[] body = Encoding.UTF8.GetBytes(item);
    //    WWW www = new WWW(API_URL, body, postHeader);
    //    yield return StartCoroutine("StartUpload", www);

    //    if (www.error != null)
    //    {
    //        Debug.Log(www.error);
    //        return false;
    //    }
    //    else
    //    {
    //        Debug.Log("Data Submitted");
    //        return true;
    //    }

    //}


    IEnumerator StartUpload(WWW www)
    {
        yield return www;
        //if (www.error != null)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Data Submitted");
        //}


        // convert json string to byte
        //var formData = System.Text.Encoding.UTF8.GetBytes(json);

        //www = new WWW(API_URL, formData, postHeader);
        //StartCoroutine(WaitForRequest(www));
        //return www;



        //UnityWebRequest www = UnityWebRequest.Put(API_URL, json);
        //www.SetRequestHeader("Content-Type", "application/json");
        //yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Upload complete!");
        //}
    }


    public void ReadLocalFile()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.Log("SAVE DATA FOUND");
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                jsonList = (List<string>)bf.Deserialize(file);
                file.Close();
            }
            catch (System.Exception)
            {
                Debug.Log("ERROR READING FILE");
                throw;
            }

        }
        else
        {
            Debug.Log("READING - SAVE DATA NOT FOUND");
        }

    }

    public void SaveLocal()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, jsonList);
            file.Close();

            Debug.Log("Saved Locally");
        }
        catch (System.Exception)
        {
            Debug.Log("ERROR SAVING FILE");
            throw;
        }

    }

    public void DeleteSaveFile()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.Log("SAVE DATA FOUND");
            try
            {
                File.Delete(Application.persistentDataPath + "/gamesave.save");
                Debug.Log("SAVE DATA DELETED");
            }
            catch (System.Exception)
            {
                Debug.Log("ERROR DELETING DATA");
                throw;
            }
        }
        else
        {
            Debug.Log("DELETING - SAVE DATA NOT FOUND");
        }

    }
}

