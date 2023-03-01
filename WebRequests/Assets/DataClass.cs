using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataClass
{
    public int level;
    public float timeElapsed;
    public string name;
}

[Serializable]
public class NewDataClass
{
    // I feel like this should throw an error on the post request but it doesn't
    public string _id;

    public string screenName;
    public string firstName;
    public string lastName;
    public string dateJoined;
    public int score;
}

[Serializable]
public class SaveList
{
    public NewDataClass[] saves;
}

[Serializable]
public class TempClass
{
    public string id;
    public string variable;
    public int score;
}
