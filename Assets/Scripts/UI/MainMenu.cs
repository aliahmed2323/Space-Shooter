using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
     GameObject serverBrowser;
    public Transform panelHUD;
    public GameObject playerPanel;
    public InputField username;

    private void Start()
    {
        serverBrowser = UIManager.instance.serverBrowser.gameObject;
    }
    public async void createMatch()
    {
        if(username.text == "")
        {
            return;
        }
        GameManager.instance.nm.match = await GameManager.instance.nm.socket.CreateMatchAsync(GameManager.instance.createRoomID());
        var ph = Instantiate(playerPanel, panelHUD);
        GameManager.instance.username = username.text;
        ph.GetComponentInChildren<Text>().text = username.text;
        this.gameObject.SetActive(false);
    }

    public void openSeverBrowser()
    {
        serverBrowser.SetActive(true);
    }
}
