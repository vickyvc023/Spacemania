using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour
{
    public static ShipManager instance;

    public int activeShip = 0;

    public GameObject[] ships;

    public GameObject[] shipsUI;

    private void Awake()
    {
        instance = this;
    }

    public void SelectShip(int val) {
        for (int i = 0; i < shipsUI.Length; i ++) {
            shipsUI[i].transform.localScale = new Vector3(1, 1, 1);
            shipsUI[i].GetComponent<Image>().color = Color.gray;
        }

        shipsUI[val].transform.localScale = new Vector3(1.25f, 1.25f, 1);
        shipsUI[val].GetComponent<Image>().color = Color.red;

        activeShip = val;
    }
}
