using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] string remoteLayerName = "RemotePlayer";

    private void Start()
    {
        if (!isLocalPlayer)
        {
            AssignRemoteLayer();
        }

        RegisterPlayer();
    }

    private void RegisterPlayer()
    {
        string playerID = "Player " + GetComponent<NetworkIdentity>().netId.ToString();
        transform.name = playerID;
    }

    [Client]
    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
}
