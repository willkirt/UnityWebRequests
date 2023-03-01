using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class deleteButton : MonoBehaviour
{
    public TempClass id;

    IEnumerator DeleteSave(string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/deleteSave", json))
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

    public void deleteSave()
    {
        // ToJson only accepts an object and is necessary for a web request
        string json = JsonUtility.ToJson(id);

        StartCoroutine(DeleteSave(json));

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
