using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CococnutChecker : MonoBehaviour {

    public void OnEnable() {
        GetComponent<Animator>().SetTrigger("stop");
    }
    public void OnDisable() {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "CoconutBasket") {
            transform.parent.GetComponent<LevelController>().RemoveFromChecklist(transform.name);
            GetComponent<Animator>().SetTrigger("start");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "CoconutBasket") {
            transform.parent.GetComponent<LevelController>().RemoveFromChecklist(transform.name);
            GetComponent<Animator>().SetTrigger("start");
        }
    }
}
