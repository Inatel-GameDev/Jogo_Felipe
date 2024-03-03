using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TMP_Text Coincount;

    // Idealmente não ser por update é sim por notificação de delegate
    void Update()
    {
        Coincount.text = GameController.Instance.coins.ToString();
    }
}
