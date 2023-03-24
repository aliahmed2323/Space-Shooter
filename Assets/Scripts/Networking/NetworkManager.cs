using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Nakama;
using UnityEngine.UI;
using Nakama.TinyJson;
using System.Text;

public class PositionState
{
    public float X;
    public float Y;
    public float Z;
}

public class NetworkManager : MonoBehaviour
{
    private const string scheme = "http";
    private const string host = "bum.crippledbumgames.tech";
    private const int port = 7350;
    private const string serverKey = "defaultkey";

    public IClient client;
    public ISession session;
    public ISocket socket;
    public IMatch match;

    private const string SessionPrefName = "nakama.session";
    private const string DeviceIdentifierPrefName = "nakama.deviceUniqueIdentifier";

    private bool connectedToMatch;

    Dictionary<string, GameObject> players;

     async void Start()
    {
       await Connect();
    }

    public async Task Connect()
    {
        client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);

        var authToken = PlayerPrefs.GetString(SessionPrefName);
        if(!string.IsNullOrEmpty(authToken))
        {
            var Session = Nakama.Session.Restore(authToken);
            if(!Session.IsExpired)
            {
                session = Session;
            }
        }

        if(session == null)
        {
            string deviceId;

            if (PlayerPrefs.HasKey(DeviceIdentifierPrefName))
            {
                deviceId = PlayerPrefs.GetString(DeviceIdentifierPrefName);
            }
            else
            {
                deviceId = SystemInfo.deviceUniqueIdentifier;

                if(deviceId == SystemInfo.unsupportedIdentifier)
                {
                    deviceId = System.Guid.NewGuid().ToString();
                }

                PlayerPrefs.SetString(DeviceIdentifierPrefName, deviceId);
            }
            session = await client.AuthenticateDeviceAsync(deviceId);

            PlayerPrefs.SetString(SessionPrefName, session.AuthToken);
        }
        socket = client.NewSocket();
        await socket.ConnectAsync(session, true);
        Debug.Log(authToken);
        Debug.Log(PlayerPrefs.GetString(DeviceIdentifierPrefName));
    }

}
