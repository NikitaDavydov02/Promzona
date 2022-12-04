using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ImagesManager : MonoBehaviour, IWeatherManager{
    public ManagerStatus status { get; private set; }
    private NetworkService _network;
    private Texture2D _webImage;

    public void Startup(NetworkService service)
    {
        Debug.Log("Images manager started...");
        _network = service;
        status = ManagerStatus.Started;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetWebImage(Action<Texture2D> callback)
    {
        if (_webImage == null)
        {
            StartCoroutine(_network.DownloadImage((Texture2D image) =>
            {
                _webImage = image;
                callback(_webImage);
            }));
        }
        else
            callback(_webImage);
    }
}
