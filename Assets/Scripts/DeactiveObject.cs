using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveObject : MonoBehaviour
{
    //Special method for deactivate object by animation event
    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
