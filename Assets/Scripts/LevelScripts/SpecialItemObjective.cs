﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// Win condition: Deliver the special item to your ship before time runs out
/// Loss condition: run out of time
/// </summary>
public class SpecialItemObjective : Objective {

    public float timeLimit = 120; // Countdown time, in seconds
    public float timeRemaining;
    private Text text;
    private PlayerCharacter2D player;
    private ItemManager itemManager;

    public SpecialItemObjective()
    {
        text = GameObject.Find("Timer").GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter2D>();
        itemManager = GameObject.Find("ObjectManager").GetComponent<ItemManager>();
        text.enabled = true;
        timeRemaining = timeLimit;
    }

    public override int NumSpecialItems { get { return 1; } }

    public override bool ObjectiveComplete()
    {
        return (itemManager.GetSpecialItemsRemaining() == 0);
    }

    public override bool ObjectiveFailed()
    {
        timeRemaining = timeLimit - Time.timeSinceLevelLoad + player.extraTime;
        int timeAsInt = (int)timeRemaining;
        text.text = timeAsInt.ToString();

        if (timeRemaining <= 0.0f) {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "Find the golden idol!\nTime remaining: " + (int)timeRemaining;
    }
}
