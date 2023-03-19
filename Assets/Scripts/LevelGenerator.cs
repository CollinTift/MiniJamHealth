using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public GameObject smallPlatformPrefab;
    public GameObject mediumPlatformPrefab;
    public GameObject largePlatformPrefab;

    public float spawnTolerance; //maximum variation away from current platform

    private Vector3 lastPosition; //store position of most recently spawned platform

    void Start() {
        BeginGame();
    }

    void Update() {
        //listen for when next player step on platform is
    }

    private void BeginGame() {
        Instantiate(largePlatformPrefab, Vector3.zero, Quaternion.identity, transform);
    }

    private void SpawnSmall(Vector3 position) {

    }

    private void SpawnMedium(Vector3 position) {

    }

    private void SpawnLarge(Vector3 position) {

    }
}
