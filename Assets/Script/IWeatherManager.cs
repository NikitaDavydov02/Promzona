using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeatherManager
{
    ManagerStatus status { get; }
    void Startup(NetworkService service);
}
