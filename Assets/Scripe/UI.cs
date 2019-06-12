using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update

    public static int Coin;

    public Text CoinText;

    public Text Health;

    void Start()
    {
        UpdateHealth();
        showCoin();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void showCoin()
    {
        CoinText.text = "Coin X " + Coin;
    }

    public void UpdateCoin()
    {
        Coin += 1;
        CoinText.text = "Coin X " + Coin;
    }

    public void UpdateHealth()
    {
        Health.text = "X " + Player_Control.health;
    }
}
