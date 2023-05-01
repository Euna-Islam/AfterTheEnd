using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    public GameObject Collect, Deliver;

    private void Start()
    {
        //Invoke("DisableCollect", 3f);
    }

    public void DisableCollect() {
        Collect.SetActive(false);
        Deliver.SetActive(true);

        Invoke("DisableDeliver", 3f);
    }

    void DisableDeliver()
    {
        Collect.SetActive(false);
        Deliver.SetActive(false);


    }
}
