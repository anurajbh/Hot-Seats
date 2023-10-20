using Android.BLE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

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
    //List<BleDevice> devices;
    //List<string> deviceNames = new List<string> { "hot seats red" };


    public Dictionary<string, string> deviceNames = new Dictionary<string, string> { { "hot seats red", "RED" }, { "BlueCharm_20502", "ORANGE"}, { "BlueCharm_10482", "YELLOW" } };
    public Dictionary<string, BleDevice> devices;
    public Dictionary<string, string> deviceDistances;

    public void Awake()
    {
        devices = new Dictionary<string, BleDevice> ();
        deviceDistances = new Dictionary<string, string>();
        ScanForDevices();
    }

    public IEnumerator ScanTimeout(float time)
    {
        yield return new WaitForSeconds(time);
        _isScanning = false;
        scanButton.interactable = true;
    }

    public void ScanForDevices()
    {

       // distText.text = "";
            Debug.Log("Trying to request...");

            Permission.RequestUserPermissions(new string[] { "android.permission.BLUETOOTH", "android.permission.BLUETOOTH_ADMIN", "android.permission.BLUETOOTH_SCAN", "android.permission.BLUETOOTH_CONNECT", "android.permission.ACCESS_FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION" });

            Permission.RequestUserPermissions(new string[] { "android.permission.ACCESS_FINE_LOCATION", "android.permission.ACCESS_COARSE_LOCATION" });

        Debug.Log("Reached end of request code...");
        

        if (!_isScanning)
        {
            _isScanning = true;
            BleManager.Instance.SearchForDevices((int)scanTime * 1000, OnDeviceFound);
            scanButton.interactable = false;
            StartCoroutine(ScanTimeout(scanTime));
        }
    }

    public void updateDistances()
    {

        distText.text = "";
        foreach (String s in deviceDistances.Keys)
        {
            distText.text += s + ": " + deviceDistances[s] + "dBm\n";

        }
    }

    public void Update()
    {
        //if (devices[0] != null)
        //{

        //    devices[0].GetRssi((_, rsi) => distText.text = rsi + " dBm");
        //}
    }


    private void OnDeviceFound(BleDevice device)
    {
        //DeviceButton button = Instantiate(_deviceButton, _deviceList).GetComponent<DeviceButton>();
        //button.Show(device);
        if(deviceNames.ContainsKey(device.Name))
        {
            devices[deviceNames[device.Name]] = device;

            device.GetRssi((_, rsi) => deviceDistances[deviceNames[device.Name]] = rsi.ToString());
            updateDistances();
            //distText.text += "\n" + deviceNames[device.Name] + ": " +rsi + " dBm");
        }
    }
}
