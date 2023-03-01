using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;

public class JsonSerializer : MonoBehaviour
{
    public TMP_InputField screenName, firstName, lastName, score;
    public DataClass dataObj;
    public DataClass newDataObj;
    public string filePath;
    
    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "saveData.txt");

        //dataObj = new DataClass();
        //dataObj.level = 1;
        //dataObj.timeElapsed = 4543.928456f;
        //dataObj.name = "Jake";
        //string json = JsonUtility.ToJson(dataObj);
        //Debug.Log(json);
        //File.WriteAllText(filePath, json);

        //StartCoroutine(SendWebData(json));
        //StartCoroutine(GetRequest("http://localhost:3000/SendUnityData"));

        //newDataObj = JsonUtility.FromJson<DataClass>(json);
        //Debug.Log(newDataObj.name);        
    }

    IEnumerator SendWebData(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/newUnitySave", json))
        {
            request.SetRequestHeader("content-type", "application/json");
            request.uploadHandler.contentType = "application/json";
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else {
                Debug.Log("DataObj Posted");
            }
            request.uploadHandler.Dispose();
        }
    }

    public void SendButton()
    {
        var scoreData = int.Parse(score.text);
        score.text = "";

        NewDataClass formData = new NewDataClass();

        formData.screenName = screenName.text;
        screenName.text = "";

        formData.firstName = firstName.text;
        firstName.text = "";

        formData.lastName = lastName.text;
        lastName.text = "";

        formData.dateJoined = System.DateTime.Now.ToString();
        formData.score = scoreData;

        string json = JsonUtility.ToJson(formData);

        StartCoroutine(SendWebData(json));
    }
}
