using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using UnityEngine.Android;
using Android;
using System;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class ExampleBleInteractor : MonoBehaviour
{
    [SerializeField]
    private GameObject _deviceButton;
    [SerializeField]
    private Transform _deviceList;

    [SerializeField]
    private int _scanTime = 10;

    private float _scanTimer = 0f;

    private bool _isScanning = false;


    public Text logText;

    public String deviceToSeek; //this gets replaced by an array later when we have all 8 debices

    private ReadFromCharacteristic _readFromCharacteristic;
    public void ScanForDevices()
    {
        if (!_isScanning)
        {
            _isScanning = true;
            BleManager.Instance.QueueCommand(new DiscoverDevices(OnDeviceFound, _scanTime * 1000));
        }
    }


    public void requestPerms()
    {
        try
        {
            Debug.Log("Trying to request...");

            Permission.RequestUserPermissions(new string[] { "android.permission.BLUETOOTH", "android.permission.BLUETOOTH_ADMIN", "android.permission.BLUETOOTH_SCAN", "android.permission.BLUETOOTH_CONNECT" });
            
            Debug.Log("Reached end of request code...");
        } catch(Exception e)
        {

            Debug.Log("Exception...");
            logText.text = logText.text+ "\n" + e.ToString();
        }
    }


        private void Update()
    {
        if(_isScanning)
        {
            _scanTimer += Time.deltaTime;
            if(_scanTimer > _scanTime)
            {
                _scanTimer = 0f;
                _isScanning = false;
            }
        }
        else
        {

        }
    }

    private void OnDeviceFound(string name, string device)
    {
        //DeviceButton button = Instantiate(_deviceButton, _deviceList).GetComponent<DeviceButton>();
        //button.Show(name, device);

        //ConnectToDevice _connectCommand = new ConnectToDevice(device);
        //BleManager.Instance.QueueCommand(_connectCommand);
        if (device == deviceToSeek){
            logText.text = "Found...";
            //ConnectToDevice _connectCommand = new ConnectToDevice(device);
            //BleManager.Instance.QueueCommand(_connectCommand);
        }
    }


    public void SubscribeToExampleService(string _deviceUuid)
    {
        //Replace these Characteristics with YOUR device's characteristics
        _readFromCharacteristic = new ReadFromCharacteristic(_deviceUuid, "180c", "2a56", (byte[] value) =>
        {
            Debug.Log(Encoding.UTF8.GetString(value));
        });
        BleManager.Instance.QueueCommand(_readFromCharacteristic);
    }
}
