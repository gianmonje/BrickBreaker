using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudForce : StarForce {
    private Vector3 target;
    private Animator animator { get { return GetComponent<Animator>(); } }

    public override void OnEnable() {

    }

}
