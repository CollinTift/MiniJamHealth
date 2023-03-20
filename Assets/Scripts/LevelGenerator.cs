using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject SmallPlatformPrefab;
    public GameObject LargePlatformPrefab;

    public GameObject HealthPotionPrefab;

    [SerializeField] private Vector2 HeightTolerance; //maximum variation above current platform
    [SerializeField] private Vector2 WidthTolerance; //max variation adjacent to current platform
    [SerializeField] private Vector2 WorldBounds; //width bounds of world
    [SerializeField] private int MaxPlatforms; //maximum platforms in world at once

    private Vector3 lastPos; //store position of most recently spawned platform

    void Start() {
        BeginGame();
    }

    private void BeginGame() {
        lastPos = Vector3.zero;

        SpawnNextSet();
    }

    public void SpawnNextSet() {
        Instantiate(LargePlatformPrefab, Vector3.zero, Quaternion.identity, transform); //spawn one large base platform

        //spawning max amt of platforms on screen at once
        for (int i = 0; i < MaxPlatforms; i++) {
            switch(Random.Range(0, 3)) {
                case 0:
                case 1:
                    SpawnSmalls(new Vector3(Random.Range(WorldBounds.x + 1.5f, -1.5f), lastPos.y + Random.Range(HeightTolerance.x, HeightTolerance.y), 0),
                                new Vector3(Random.Range(1.5f, WorldBounds.y - 1.5f), lastPos.y + Random.Range(HeightTolerance.x, HeightTolerance.y), 0));
                    break;
                case 2:
                    SpawnLarge(new Vector3(Mathf.Clamp(Random.Range(WorldBounds.x + 5f, WorldBounds.y - 5f), lastPos.x + WidthTolerance.x, lastPos.x + WidthTolerance.y),
                                lastPos.y + Random.Range(HeightTolerance.x, HeightTolerance.y), 0));
                    break;
            }
        }

        //spawn health pack 1 unit above top platform (last pos)
        Instantiate(HealthPotionPrefab, new Vector3(lastPos.x, lastPos.y + 1, 0), Quaternion.identity, transform);
    }

    private void SpawnSmalls(Vector3 positionOne, Vector3 positionTwo) {
        if (positionOne.y > positionTwo.y) {
            lastPos = positionOne;
        } else {
            lastPos = positionTwo;
        }

        Instantiate(SmallPlatformPrefab, positionOne, Quaternion.identity, transform);
        Instantiate(SmallPlatformPrefab, positionTwo, Quaternion.identity, transform);
    }

    private void SpawnLarge(Vector3 position) {
        lastPos = position;
        Instantiate(LargePlatformPrefab, position, Quaternion.identity, transform);
    }
}
