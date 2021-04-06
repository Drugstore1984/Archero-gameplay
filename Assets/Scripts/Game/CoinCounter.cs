using System;
using UnityEngine;
using CryoDI;
using UnityEngine.UI;

public class CoinCounter : CryoBehaviour
{
    [Dependency]
    private EventEmitter Events { get; set; }
    private GameObject _coinText;
    [SerializeField] private int _coinCounter = 0;
    private void Start()
    {
        _coinText = GameObject.FindGameObjectWithTag("CoinText");
        Events.OnCoinAdd += Event_OnCoinAdd;
    }
    private void Event_OnCoinAdd(object sender, EventArgs e)
    {
        _coinCounter++;
        _coinText.GetComponent<Text>().text = _coinCounter.ToString();
    }
}
