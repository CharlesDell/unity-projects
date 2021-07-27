using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrefab;

    DefenderSpawner defenderSpawner;

    private void Start()
    {
        defenderSpawner = FindObjectOfType<DefenderSpawner>();

        LabelCost();
    }

    private void LabelCost()
    {
        Text costText = GetComponentInChildren<Text>();
        if (!costText)
        {
            //Debug.LogError(name + " has no cost text.");
            return;
        }
        else
        {
            costText.text = defenderPrefab.GetStarCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach (var button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(75, 75, 75, 255);
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        defenderSpawner.SetDefender(defenderPrefab);
    }
}
