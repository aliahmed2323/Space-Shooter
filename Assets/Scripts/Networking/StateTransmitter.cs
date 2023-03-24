using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;

public class statePos
{
    public bool pos;
}
public class StateTransmitter : MonoBehaviour
{
  public async void sendPos(bool param)
    {
        var posState = new statePos
        {
            pos = param
        };
        await GameManager.instance.nm.socket.SendMatchStateAsync(GameManager.instance.nm.match.Id, 1, JsonWriter.ToJson(posState));
    }

    public async void sendShoot()
    {
        await GameManager.instance.nm.socket.SendMatchStateAsync(GameManager.instance.nm.match.Id, 2, "");
    }
}
