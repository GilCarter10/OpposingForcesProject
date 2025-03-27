using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    [SerializeField] bool spriteInParent;

    private void OnBecameInvisible()
    {
        if (spriteInParent && GetComponentInParent<Rigidbody2D>() != null)
        {
            Debug.Log(gameObject.name);
            Destroy(GetComponentInParent<Rigidbody2D>().gameObject);
        }
        else Destroy(gameObject);
    }
}
