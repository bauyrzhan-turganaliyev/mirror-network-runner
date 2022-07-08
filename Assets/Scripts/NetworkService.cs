using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace nmRunner
{
    public class NetworkService : NetworkManager
    {
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            PlayerNetwork.ResetPlayerNumbers();
        }
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);
            PlayerNetwork.ResetPlayerNumbers();
        }
    }
}
