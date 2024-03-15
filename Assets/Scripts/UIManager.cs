using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// container struct for UI objects associated with players
public struct UIContainerForPlayer
{

    //TODO - perhaps use properties instead of fields
    public GameObject colorUIObject;
    public GameObject scoreTextUIObject;
    public UIContainerForPlayer(GameObject colorObject, GameObject scoreTextObject)
    {
        colorUIObject = colorObject;
        scoreTextUIObject = scoreTextObject;
    }
}
public class UIManager : MonoSingleton<UIManager>
{
    //prefab objects to use as the basis for player color positions and score positions
    [SerializeField] GameObject colorPrefab;
    [SerializeField] GameObject textPrefab;
    //layout position with step differences
    [SerializeField] float diffPosition = 100f;
    [SerializeField] float diffPositionStep = 100f;
    RectTransform m_rectTransform;
    //todo - builtins for Grid layout?

    //TODO - refactor to use components instead of game objects

    public List<UIContainerForPlayer> listOfUIElements = new List<UIContainerForPlayer>();
    private void Start()
    {
        //iterate through player list
        m_rectTransform = GetComponent<RectTransform>();
        for (int i = 0; i < GameManager._Instance.players.Count; i++)
        {
            //null check
            if (GetComponent<RectTransform>() == null)
            {
                return;
            }
            CreatePlayerUI(GameManager._Instance.players[i]);
            //increment step for positional difference
            diffPosition += diffPositionStep;
        }
    }
    public void CreatePlayerUI(Player player)
    {
        //instantiate clone of color prefab and assign it the color associated with current player iteration
        GameObject newColor = Instantiate(colorPrefab, m_rectTransform);
        if (newColor.GetComponent<RectTransform>() == null)
        {
            return;
        }
        newColor.GetComponent<RectTransform>().position = new Vector3(newColor.GetComponent<RectTransform>().position.x + diffPosition, newColor.GetComponent<RectTransform>().position.y, newColor.GetComponent<RectTransform>().position.z);
        if (newColor.GetComponent<Image>() == null)
        {
            return;
        }
        newColor.GetComponent<Image>().color = player.playerColor;
        //ditto but for text prefab
        GameObject newText = Instantiate(textPrefab, m_rectTransform);
        if (newText.GetComponent<RectTransform>() == null)
        {
            return;
        }
        newText.transform.position = new Vector3(newText.GetComponent<RectTransform>().position.x + diffPosition, newText.GetComponent<RectTransform>().position.y, newText.GetComponent<RectTransform>().position.z);
        if (newText.GetComponent<TMPro.TextMeshProUGUI>() == null)
        {
            return;
        }
        newText.GetComponent<TMPro.TextMeshProUGUI>().text = player.playerScore.ToString();
        listOfUIElements.Add(new UIContainerForPlayer(newColor, newText));
    }
    //use this method to update the UI whenever you are changing the score
    public void UpdatePlayerScores()
    {
        for (int i = 0; i < GameManager._Instance.players.Count; i++)
        {
            listOfUIElements[i].scoreTextUIObject.GetComponent<TMPro.TextMeshProUGUI>().text = GameManager._Instance.players[i].playerScore.ToString();
        }
    }
    //just putting it in Update() to test
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UpdatePlayerScores();
        }
    }
}
