using UnityEngine;
using System.Collections;

public class CoinClicker : MonoBehaviour
{
    public GameManager gameManager;

    public void OnCoinClick()
    {
        gameManager.OnCoinClicked();
    }
}