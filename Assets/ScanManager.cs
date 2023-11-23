using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{

    public EasyWiFiManager wifiMan;
    public Button scanButton;
    // Start is called before the first frame update
    void Start()
    {
        if(wifiMan == null)
        {
            wifiMan = GameObject.FindObjectOfType<EasyWiFiManager>();
            if(wifiMan == null)
            {
                wifiMan = new EasyWiFiManager();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendScanInstructions()
    {
        Debug.Log("SCAN MANAGER: Sending scan command...");
        wifiMan.sendScanInstruction();
        StartCoroutine(disableScanButton());
    }

    public IEnumerator disableScanButton()
    {
        scanButton.enabled = false;
        yield return new WaitForSeconds(20);
        scanButton.enabled = true;
    }


}
