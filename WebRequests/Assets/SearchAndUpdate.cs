using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class SearchAndUpdate : MonoBehaviour
{
    public TMP_InputField username, score;
    public TextMeshProUGUI otherData;
    public NewDataClass searchclass;
    public string id;

    IEnumerator SearchData(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/searchSaves", json))
        {
            request.SetRequestHeader("content-type", "application/json");
            request.uploadHandler.contentType = "application/json";
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

            yield return request.SendWebRequest();

            // handle response
            var newData = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
            var getRequestData = JsonUtility.FromJson<SaveList>(newData);

            NewDataClass res = getRequestData.saves[0];
            id = res._id;
            otherData.text = $"{res.firstName} {res.lastName} | {res.dateJoined}";
            score.text = res.score.ToString();

            //error check
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("DataObj Posted");
            }
            request.uploadHandler.Dispose();
        }
    }

    public void SearchDB()
    {
        TempClass searchName = new TempClass();
        searchName.variable = username.text;
        string json = JsonUtility.ToJson(searchName);

        StartCoroutine(SearchData(json));
    }

    IEnumerator UpdateScore(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/update", json))
        {
            request.SetRequestHeader("content-type", "application/json");
            request.uploadHandler.contentType = "application/json";
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("DataObj Posted");
            }
            request.uploadHandler.Dispose();
        }
    }

    public void updateSave()
    {
        TempClass updateScore = new TempClass();
        updateScore.id = id;
        updateScore.score = int.Parse(score.text);
        string json = JsonUtility.ToJson(updateScore);

        StartCoroutine(UpdateScore(json));

        SceneManager.LoadScene(1);
    }
}
