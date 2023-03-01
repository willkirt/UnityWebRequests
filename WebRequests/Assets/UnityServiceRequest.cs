using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityServiceRequest : MonoBehaviour
{
    public void SendWebData(string jason)
    {
        var request = UnityWebRequest.Post("http://localhost:3000/unity", jason);
        request.SetRequestHeader("content-type", "application/json");
        request.uploadHandler.contentType = "application/json";
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jason));

        request.SendWebRequest();
    }
}
