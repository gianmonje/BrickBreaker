using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour {

    public float speed = 10.0f;
    public Vector2 target;
    public Vector2 resetPosition;

    [System.Serializable]
    public class CupCake {
        public CupCakeFlavor cupCakeFlavor;
        public Sprite cupcakeSprite;
    }
    [SerializeField]
    public List<CupCake> cupCakesList;//AVAILABLE CUPCAKES TO RANDOM
    [SerializeField]
    public List<CupCakeItem> CupCakeItemList; //ITEM ON CONVEYER

    public CustomLevel7LevelController Level7LevelController { get { return this.transform.parent.GetComponent<CustomLevel7LevelController>(); } }

    [Button("Get Cupcakes"), BoxGroup("NoTitle", false)]
    private void GetCupcakes() {
        CupCakeItemList = GetComponentsInChildren<CupCakeItem>().ToList();
        for (int i = 0; i < CupCakeItemList.Count; i++) {
            for (int x = 0; x < cupCakesList.Count; x++) {
                if (CupCakeItemList[i].cupCakeFlavor == cupCakesList[x].cupCakeFlavor) {
                    CupCakeItemList[i].Cook(CupCakeItemList[i].cupCakeFlavor, cupCakesList[x].cupcakeSprite);
                }
            }
        }
    }

    public void OnEnable() {
        IsOktoMove = false;
        ResetCupcakes();
        StartCoroutine("DelayStart");
    }

    public Sprite GetCupcakeSprite(CupCakeFlavor cupCakeFlavor) {
        for (int i = 0; i < cupCakesList.Count; i++) {
            if (cupCakesList[i].cupCakeFlavor == cupCakeFlavor) {
                return cupCakesList[i].cupcakeSprite;
            }
        }
        return null;
    }

    public void ResetCupcakes() {
        for (int i = 0; i < CupCakeItemList.Count; i++) {
            //CupCake cupCake = RandomCupcake;
            CupCakeItemList[i].gameObject.SetActive(true);
            CupCakeItemList[i].transform.localPosition = new Vector2(CupCakeItemList[i].transform.localPosition.x, 0);
            //CupCakeItemList[i].GetComponent<CupcakeBrick>().ResetBrick();
            //CupCakeItemList[i].Cook(cupCake.cupCakeFlavor, cupCake.cupcakeSprite);
        }
    }

    private CupCake RandomCupcake {
        get {
            int dice = UnityEngine.Random.Range(0, cupCakesList.Count);
            return cupCakesList[dice];
        }
    }

    void Update() {
        if (!IsOktoMove) return;
        float step = speed * Time.deltaTime;
        // move sprite towards the target location
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, step);
        if (Vector2.Distance(transform.localPosition, target) <= 0.1f) {
            transform.localPosition = resetPosition;
            //ChangeCupcakes();
        }
    }

    private bool IsOktoMove = false;
    private IEnumerator DelayStart() {
        yield return new WaitForSeconds(0.2f);
        IsOktoMove = true;
        ResetCupcakes();
    }
}
