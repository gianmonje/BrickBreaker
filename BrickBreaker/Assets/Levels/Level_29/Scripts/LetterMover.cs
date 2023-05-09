using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMover : MonoBehaviour {

    public float velocity = 10;
    public float stopTime = 1;
    public Vector2 minMaxInterval;
    public float scaleSize;

    private Vector2 initialPos;
    private float dice;
    // Start is called before the first frame update
    private void OnEnable() {
        GetComponent<Animator>().SetTrigger("stop");
        initialPos = transform.position;
        Show();
    }

    private void OnDisable() {
        StopCoroutine("ShowRandomly");
        StopCoroutine("StopTime");
    }

    private void Show() {
        transform.position = initialPos;
        transform.localScale = new Vector2(1, 1);
        dice = Random.Range(minMaxInterval.x, minMaxInterval.y + 1);
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine("ShowRandomly");
    }

    private IEnumerator ShowRandomly() {
        yield return new WaitForSeconds(dice);
        GetComponent<Animator>().SetTrigger("start");
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * velocity;
        StartCoroutine("StopTime");
    }

    private IEnumerator StopTime() {
        yield return new WaitForSeconds(stopTime);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Show();
    }
}
