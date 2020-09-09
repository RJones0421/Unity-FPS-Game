using Cinemachine;
using Mirror;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam = null;

    [SerializeField]
    private LayerMask mask;

    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced.");
            this.enabled = false;
        }

        Controls.Player.Shoot.performed += ctx => Shoot();
    }

    [ClientCallback]
    private void OnEnable()
    {
        Controls.Enable();
    }

    [ClientCallback]
    private void OnDisable()
    {
        Controls.Disable();
    }

    [Client]
    private void Shoot()
    {
        Debug.Log("Shooting");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            if (hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string playerID)
    {
        Debug.Log(playerID + " has been shot!");
    }
}
