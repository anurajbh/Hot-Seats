using Android.BLE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public ExampleBleInteractor ble;
    // Start is called before the first frame update
    void Awake()
    {
        ble.ScanForDevices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
