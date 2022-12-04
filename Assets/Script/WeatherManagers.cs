using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]
public class WeatherManagers : MonoBehaviour {
    public static WeatherManager Weather { get; private set; }
    public static ImagesManager Images { get; private set; }
    private List<IWeatherManager> _startSequence;
    void Awake()
    {
        Weather = GetComponent<WeatherManager>();
        Images = GetComponent<ImagesManager>();
        _startSequence = new List<IWeatherManager>();
        _startSequence.Add(Weather);
        _startSequence.Add(Images);
        StartCoroutine(StartupManagers());
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();
        foreach (IWeatherManager manager in _startSequence)
        {
            manager.Startup(network);
        }
        yield return null;
        int numModules = _startSequence.Count;
        int numReady = 0;
        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;
            foreach (IWeatherManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null;
        }
        Debug.Log("All managers started up!");
    }
}
