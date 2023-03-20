using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollower : MonoBehaviour {
    [SerializeField] private CharacterController player;

    [SerializeField] private Sprite day;
    [SerializeField] private Sprite dusk;
    [SerializeField] private Sprite night;

    private SpriteRenderer spr;

    private void Start() {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (player.HealthCollected == 0) {
            spr.sprite = day;
        } else if (player.HealthCollected == 1) {
            spr.sprite = dusk;
        } else {
            spr.sprite = night;
        }

        transform.position = player.transform.position + new Vector3(0, 0, 10);
    }
}
