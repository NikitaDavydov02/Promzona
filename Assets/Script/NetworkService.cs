using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService : MonoBehaviour {
    private const string xmlApx = "http://api.openweathermap.org/data/2.5/weather?q=Moscow,ru&mode=xml&APPID=1c89072d456b5789cf03d93b49815d81";
    private const string xmlsApx = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,sus&mode=xml&APPID=1c89072d456b5789cf03d93b49815d81";
    private const string jsonApx = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,sus&mode=xml";
    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private bool IsResponseValid(WWW www)
    {
        if (www.error != null)
        {
            Debug.Log("bad connection");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("bad data");
            return false;
        }
        else
        {
            return true;
        }
    }
    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        WWW www = new WWW(url);
        Debug.Log(url);
        Debug.Log(www.error);
        yield return www;
        Debug.Log(www.error);
        if (!IsResponseValid(www))
            yield break;
        callback(www.text);
    }
    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApx, callback);
    }
    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApx, callback);
    }
    public IEnumerator DownloadImage(Action<Texture2D> callBack)
    {
        WWW www = new WWW(webImage);
        Debug.Log(www.error);
        yield return www;
        Debug.Log(www.error);
        callBack(www.texture);
    }
}
