using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void ToAddSave()
    {
        SceneManager.LoadScene(0);
    }

    public void ToSaveList()
    {
        SceneManager.LoadScene(1);
    }

    public void ToSearch()
    {
        SceneManager.LoadScene(2);
    }
}
