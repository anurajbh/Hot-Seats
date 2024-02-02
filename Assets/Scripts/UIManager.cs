using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoSingleton<UIManager>
{
    //prefab objects to use as the basis for player color positions and score positions
    [SerializeField] GameObject colorPrefab;
    [SerializeField] GameObject textPrefab;
    //layout position with step differences
    [SerializeField] float diffPosition = 100f;
    [SerializeField] float diffPositionStep = 100f;
    //todo - builtins for Grid layout?
    private void Start()
    {
        //iterate through player list
        for(int i = 0; i < GameManager._Instance.players.Count; i++)
        {
            //null check
            if(GetComponent<RectTransform>() == null)
            {
                return;
            }
            //instantiate clone of color prefab and assign it the color associated with current player iteration
            GameObject newColor = Instantiate(colorPrefab, GetComponent<RectTransform>());
            if(newColor.GetComponent <RectTransform>() == null)
            {
                return;
            }
            newColor.GetComponent<RectTransform>().position = new Vector3(newColor.GetComponent<RectTransform>().position.x + diffPosition, newColor.GetComponent<RectTransform>().position.y, newColor.GetComponent<RectTransform>().position.z);
            if (newColor.GetComponent<Image>() == null)
            {
                return;
            }
            newColor.GetComponent<Image>().color = GameManager._Instance.players[i].playerColor;
            //ditto but for text prefab
            GameObject newText = Instantiate(textPrefab, GetComponent<RectTransform>());
            if(newText.GetComponent <RectTransform>() == null)
            {
                return;
            }
            newText.transform.position = new Vector3(newText.GetComponent<RectTransform>().position.x + diffPosition, newText.GetComponent<RectTransform>().position.y, newText.GetComponent<RectTransform>().position.z);
            if (newText.GetComponent<TMPro.TextMeshProUGUI>() == null)
            {
                return;
            }
            newText.GetComponent<TMPro.TextMeshProUGUI>().text = GameManager._Instance.players[i].playerScore.ToString();
            //increment step for positional difference
            diffPosition += diffPositionStep;
        }
    }
}
