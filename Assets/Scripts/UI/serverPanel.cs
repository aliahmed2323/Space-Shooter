using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;

public class serverPanel : MonoBehaviour
{
    public Text nameText;
    public Text playerText;
    public MainMenu mm;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>connectToMatch());
        mm = UIManager.instance.mainMenu.GetComponent<MainMenu>();
    }
    public async void connectToMatch()
    {
        var nm = GameManager.instance.nm;
        string matchid = nameText.text;
        nm.match = await nm.socket.JoinMatchAsync(matchid);
        UIManager.instance.serverBrowser.gameObject.SetActive(false);
        UIManager.instance.mainMenu.gameObject.SetActive(false);
        foreach(var peps in nm.match.Presences)
        {
            var ph = Instantiate(mm.playerPanel, mm.panelHUD);
            GameManager.instance.username = mm.username.text;
            ph.GetComponentInChildren<Text>().text = mm.username.text;
        }
    }
}
