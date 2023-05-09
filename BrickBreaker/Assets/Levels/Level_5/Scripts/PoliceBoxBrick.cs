using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBoxBrick : MonoBehaviour {
    [SerializeField]
    private float recoilStrength, recoilDuration, recoilSpeed;
    private bool isRecoil = false;
    private Vector2 originalPosition;
    private Vector2 targetPosition;

    private void Awake() {
        originalPosition = transform.position;
    }

    private void OnEnable() {
        transform.position = originalPosition;
    }

    private void Update() {
        if (isRecoil) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, recoilSpeed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, recoilSpeed * Time.deltaTime);
        }
    }

    public void DoHitEffetct() {
        targetPosition = new Vector2(transform.position.x, transform.position.y + recoilStrength);
        StopCoroutine("StartRecoil");
        StartCoroutine("StartRecoil");
    }

    private IEnumerator StartRecoil() {
        isRecoil = true;
        yield return new WaitForSeconds(recoilDuration);
        isRecoil = false;
    }
}
