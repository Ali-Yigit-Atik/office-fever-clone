using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moneyCountDisplay : MonoBehaviour
{

    public TextMeshProUGUI moneyCountText;

    private void Awake()
    {
        PlayerPrefs.DeleteKey("money");
    }
    void Update()
    {
        moneyCountText.text = "$" + PlayerPrefs.GetInt("money");
    }
}
