using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAndActivate : MonoBehaviour {

    public GameObject deactivate;
    public GameObject activate;

    public void DeactivateActivate()
    {
        deactivate.SetActive(false);
        activate.SetActive(true);
    }
}
