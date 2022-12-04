using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using MiniJSON;

public class WeatherManager : MonoBehaviour,IWeatherManager {
    public ManagerStatus status { get; private set; }
    private NetworkService _network;
    public float cloudValue { get; private set; }

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");
        _network = service;
        StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));
        status = ManagerStatus.Initializing;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnXMLDataLoaded(string data)
    {
        //Debug.Log(data);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;
        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        cloudValue = Convert.ToInt32(value) / 100f;
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
        status = ManagerStatus.Started;
    }
    public void OnJSONDataLoaded(string data)
    {
        Debug.Log(data);
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;
        Dictionary<string, object> clouds = (Dictionary<string, object>)dict["fact"];
        cloudValue = (long)clouds["cloudness"];
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
        status = ManagerStatus.Started;
    }
}
