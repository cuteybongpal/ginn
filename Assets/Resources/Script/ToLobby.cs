using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLobby : MonoBehaviour
{
    public GameObject UI_LobbyConfirmPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        Instantiate(UI_LobbyConfirmPrefab);
    }
}
