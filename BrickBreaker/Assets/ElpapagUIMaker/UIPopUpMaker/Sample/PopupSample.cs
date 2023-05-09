using UnityEngine;

public class PopupSample : MonoBehaviour {

    public void TestThis() {
        // UIPopupMaker.Instance.ShowPopup(UIList.PopupTest).Show("I am dora", " Are you sure your dora?").AddButton("Definitely", Click_1).AddButton("Maybe Not", Click_2).AddButton("No Am not BOY", Click_3);
    }

    public void TestThis2() {
       // ScreenTest.Instance.Show();
    }

    private void Click_1() {
        Debug.Log("A");
    }
    private void Click_2() {
        Debug.Log("B");
    }
    private void Click_3() {
        Debug.Log("C");
    }
}
