using Android.BLE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using EasyWiFi.Core;

public class ExampleBleInteractor : MonoBehaviour
{
    [SerializeField]
    private GameObject _deviceButton;
    [SerializeField]
    private Transform _deviceList;
    public Text distText;
    private bool _isScanning = false;
    public float scanTime = 10f;
    public Button scanButton;
    float scanTimer = 0f;
    //List<BleDevice> devices;
    //List<string> deviceNames = new List<string> { "hot seats red" };
    public EasyWiFiManager wifiMan;


    public Dictionary<string, string> deviceNames = new Dictionary<string, string> { { "hot seats red", "RED" }, { "BlueCharm_20502", "ORANGE"}, { "BlueCharm_10482", "YELLOW" } };
    public Dictionary<string, BleDevice> devices;
    public Dictionary<string, string> deviceDistances;


    public void Awake()
    {
        devices = new Dictionary<string, BleDevice> ();
        deviceDistances = new Dictionary<string, string>();
        scanButton.interactable = false;
        EasyWiFiController.OnScanCommand += new EasyWiFiController.ScanCommandHandler(ScanForDevices);
        wifiMan = GameObject.FindObjectOfType<EasyWiFiManager>();
    }

    

    public void ScanForDevices()
    {
       // distText.text = "";

            //Permission.RequestUserPermissions(new string[] { "android.permission.BLUETOOTH", "android.permission.BLUETOOTH_ADMIN", "android.permission.BLUETOOTH_SCAN", "android.permission.BLUETOOTH_CONNECT", "android.permission.ACCESS_FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION" });

            //Permission.RequestUserPermissions(new string[] { "android.permission.ACCESS_FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION" });

        

        if (!_isScanning)
        {
            Debug.Log("BLE INTERACTOR: SCAN STARTED");
            _isScanning = true;
            scanTimer = scanTime;
            BleManager.Instance.SearchForDevices((int)scanTime * 1000, OnDeviceFound);
            scanButton.interactable = false;
            StartCoroutine(ScanTimeout(scanTime + 18));
            distText.text = "";
        }
    }

    public void updateDistances()
    {

        distText.text = "";

        int closestDist = 100;
        string closestPlayer = "";
        foreach (String s in deviceDistances.Keys)
        {
            if(Mathf.Abs(int.Parse(deviceDistances[s])) < closestDist) {
                closestPlayer = s;
                closestDist = Mathf.Abs(int.Parse(deviceDistances[s]));
             }
            //distText.text += s + ": " + deviceDistances[s] + "dBm\n";

        }

        distText.text = "CLOSEST PLAYER:\n" + closestPlayer;
    }

    public void Update()
    {
        if (_isScanning)
        {
            scanTimer -= Time.deltaTime;

            if(scanTimer <= 0)
            {
                _isScanning = false;
                Debug.Log("END OF SCAN TIMEOUT");
                _isScanning = false;
                //scanButton.interactable = true;
                if (wifiMan != null)
                {
                    Debug.Log("SENDING PLACEHOLDER RESPONSE");
                    wifiMan.sendClientBluetoothData("RESPONSE STRING");
                }
            }
        }
    }


    private void OnDeviceFound(BleDevice device)
    {
        //DeviceButton button = Instantiate(_deviceButton, _deviceList).GetComponent<DeviceButton>();
        //button.Show(device);

        //Debug.Log(device.Name);
        if(deviceNames.ContainsKey(device.Name))
        {
            devices[deviceNames[device.Name]] = device;

            device.GetRssi((_, rsi) => deviceDistances[deviceNames[device.Name]] = rsi.ToString());
            updateDistances();
            //distText.text += "\n" + deviceNames[device.Name] + ": " +rsi + " dBm");
        }
    }
}
