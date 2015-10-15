﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

    public int boxCount = 5;
    public int collectCount = 10;
    public int slowCloudCount = 5;

    public bool allCollected = false;

    public Transform bouncyBox;
    public Transform stickyBox;
    public Transform slowCloud;
    public Transform[] collectibleList;

    private float sideLength = 25f;
    private float closestToEdge = 5f;
    private float[] heights;
    private List<Transform> collectibles;
    private float gravityEffect;
    private InventoryScript inventory;

    // Use this for initialization
    void Start() {
        inventory = GameObject.Find ("Inventory").GetComponent<InventoryScript> ();
        heights = GameObject.FindObjectOfType<TerrainCreator> ().GetHeights();
        sideLength = PersistentTerrainSettings.settings.sideLength - closestToEdge;
        gravityEffect = PersistentTerrainSettings.settings.gravityEffect;

        GameObject newStickyBox = stickyBox.gameObject;
        Rigidbody2D stickyBoxRigidBody = newStickyBox.GetComponent<Rigidbody2D>();
        stickyBoxRigidBody.gravityScale = gravityEffect;

        GameObject newBouncyBox = bouncyBox.gameObject;
        Rigidbody2D bouncyBoxRigidBody = newBouncyBox.GetComponent<Rigidbody2D>();
        bouncyBoxRigidBody.gravityScale = gravityEffect;

        collectibles = new List<Transform> ();
        addCollectibles ();
        addBoxes ();
        addSlowClouds();
        addSticky ();
    }

    void Update() {
        if (!collectibles.Any ()) {
            allCollected = true;
        }
    }

    private void addSlowClouds() {
        for (int i = 0; i < slowCloudCount; ++i) {
            Vector3 position = new Vector3(Random.Range(-sideLength / 2, sideLength / 2), 1);
            Instantiate(slowCloud, position, Quaternion.identity);
        }
    }

    private void addBoxes(){
        for (int i = 0; i < boxCount; ++i) {
            Vector3 position = new Vector3(Random.Range(-sideLength / 2, sideLength / 2), 2);
            Instantiate(bouncyBox, position, Quaternion.identity);
        }
    }
    private void addCollectibles() {
        for (int i = 0; i < collectCount; ++i) {
            float xCoor = Random.Range(0, sideLength);
            float height = heights[(int)xCoor] + Random.Range (0, 3);
            height = Mathf.Max(height, 5);
            Vector3 position = new Vector3(xCoor - sideLength / 2, height);
            Transform collect = collectibleList[Random.Range(0, collectibleList.Length)].transform;
            collectibles.Add (Instantiate(collect, position, Quaternion.identity) as Transform);
        }
    }
    private void addSticky() {
        for (int i = 0; i < boxCount; ++i) {
            Vector3 position = new Vector3(Random.Range(-sideLength / 2, sideLength / 2), 5);
            Instantiate(stickyBox, position, Quaternion.identity);
        }
    }

    public void RemoveFromScene(NonPlayerObject npo) {
        if (collectibles.Contains (npo.transform))
            collectibles.Remove (npo.transform);
    }

    public void AddToInventory(Collectible item) {
        inventory.AddItemToInventory (item);
    }
}
