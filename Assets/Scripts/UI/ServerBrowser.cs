using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama; 

public class ServerBrowser : MonoBehaviour
{
    public Transform content;
    public GameObject serverPanel;
    private void OnEnable()
    {
        populateServerBrowser();
    }
    private void OnDisable()
    {
        unPopulateServer();
    }
    public void closePanel()
    {
        gameObject.SetActive(false);
    }

    async void populateServerBrowser()
    {
        NetworkManager nm = GameManager.instance.nm;
        var matches = await  nm.client.ListMatchesAsync(nm.session, 1, 1, 10, false, null, null);
       foreach(var item in matches.Matches)
        {
           var server = Instantiate(serverPanel, content);
            server.GetComponent<serverPanel>().nameText.text = item.MatchId.ToString();
            server.GetComponent<serverPanel>().playerText.text = item.Size.ToString();
        }
    }
    void unPopulateServer()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
