﻿using UnityEngine;
using System.Collections;
using System;

public class HealthCollectible : Collectible {

    public const string typeString = "Health";

    public override string type { get { return typeString; } }

    public override void OnCollect()
    {
        InventoryScript.inventory.AddItemToInventory(this);
    }

    public override void OnUse(PlayerCharacter2D player)
    {
        player.health += value;
    }
}
