using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private CharacterController player;
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectMask2D mask;

    private float maxTopMask;
    private float initialTopMask;

    private void Start() {
        maxTopMask = barRect.rect.height - mask.padding.y - mask.padding.w;
        initialTopMask = mask.padding.w;
    }

    public void Update() {
        float targetTop = player.Health * maxTopMask / player.MaxHealth;
        float newTopMask = maxTopMask + initialTopMask - targetTop;
        Vector4 padding = mask.padding;
        padding.w = newTopMask;
        mask.padding = padding;
    }
}
