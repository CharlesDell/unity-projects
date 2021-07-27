﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defender;
    StarDisplay starDisplay;
    GameObject defenderParent;
    const string DEFENDER_PARENT_NAME = "Defenders";

    private void Start()
    {
        CreateDefenderParent();
        starDisplay = FindObjectOfType<StarDisplay>();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);
        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetDefender(Defender defender)
    {
        this.defender = defender;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        int defenderCost = defender.GetStarCost();

        if (starDisplay.EnoughStars(defenderCost))
        {
            SpawnDefender(gridPos);
            starDisplay.SpendStars(defenderCost);
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 worldPos)
    {
        float newX = Mathf.RoundToInt(worldPos.x);
        float newY = Mathf.RoundToInt(worldPos.y);
        return new Vector2(newX, newY);
    }

    private void SpawnDefender(Vector2 gridPos)
    {
        if (!defender) { return; }
        Defender newDefender = Instantiate(defender, gridPos, Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}