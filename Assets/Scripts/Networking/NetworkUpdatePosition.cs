using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using System.Text;
using Nakama.TinyJson;

public class RemotePlayerNetworkData
{
    public string MatchId;
    public IUserPresence User;
}
public class NetworkUpdatePosition : MonoBehaviour
{
    GameObject thisObject;
    RemotePlayerNetworkData NetworkData;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("addListener", 2f);
        thisObject = this.gameObject;
    }
    void addListener()
    {
        GameManager.instance.nm.socket.ReceivedMatchState += OnRecievedMatchState;
    }

    private void OnDestroy()
    {
        GameManager.instance.nm.socket.ReceivedMatchState -= OnRecievedMatchState;
    }

    void OnRecievedMatchState(IMatchState matchState)
    {
        //if(matchState.UserPresence.SessionId != NetworkData.User.SessionId)
        //{
        //    return;
        //}
        if(matchState.OpCode == 1)
        {
            var stateJson = Encoding.UTF8.GetString(matchState.State);
            var positionState = JsonParser.FromJson<statePos>(stateJson);
            UnityMainThreadDispatcher.Instance().Enqueue(() => thisObject.gameObject.GetComponent<Spaceshipcontroller>().movementManager(positionState.pos));
        }
        if (matchState.OpCode == 2)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => thisObject.gameObject.GetComponent<Spaceshipcontroller>().shoot());
        }
    }

}
