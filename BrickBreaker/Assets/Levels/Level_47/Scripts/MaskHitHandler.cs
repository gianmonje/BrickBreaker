using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskHitHandler : MonoBehaviour {
    public GameObject starForcePrefab;
    public Transform parent;

    public List<GameObject> particles;

    private void Awake() {
        particles = new List<GameObject>();
    }

    public void ShowAStar() {
        GameObject gO = Instantiate(starForcePrefab, starForcePrefab.transform.position, starForcePrefab.transform.localRotation, (parent == null) ? transform.parent : parent);
        gO.SetActive(true);
        particles.Add(gO);
    }

    public void OnDisable() {
        for (int i = 0; i < particles.Count; i++) {
            if (particles[i] != null) Destroy(particles[i]);
        }
        particles.Clear();

        MaskParticleDestroyer[] maskParticleDestroyers = transform.GetComponentsInChildren<MaskParticleDestroyer>();
        for (int i = 0; i < maskParticleDestroyers.Length; i++) {
            if (maskParticleDestroyers[i].name == "CoinParticle(Clone)") {
                Destroy(maskParticleDestroyers[i].gameObject);
            }
        }
        for (int i = 0; i < maskParticleDestroyers.Length; i++) {
            if (maskParticleDestroyers[i].name == "MaskParticle(Clone)") {
                Destroy(maskParticleDestroyers[i].gameObject);
            }
        }
    }
}
