using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using TMPro;

public class SaveListJSON : MonoBehaviour
{
    public GameObject displaygroup;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:3000/saveListUnity"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest getRequest = UnityWebRequest.Get(uri))
        {
            yield return getRequest.SendWebRequest();

            var newData = System.Text.Encoding.UTF8.GetString(getRequest.downloadHandler.data);
            var getRequestData = JsonUtility.FromJson<SaveList>(newData);

            foreach(NewDataClass n in getRequestData.saves)
            {
                GameObject newDisplay = displaygroup;

                newDisplay.GetComponentInChildren<TextMeshProUGUI>().text = n.screenName + " | " + n.firstName + " " + n.lastName + " | " +
                    n.dateJoined + " | " + n.score;

                newDisplay.GetComponentInChildren<deleteButton>().id.variable = n._id;

                Instantiate(newDisplay, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            }
        }
    }
}
