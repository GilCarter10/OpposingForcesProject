using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject beam;
    public bool fired = false;

    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        beam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (!fired)
        {
            if (timer >= 2)
            {
                animator.SetTrigger("Visor");
                timer = 0;
                fired = true;
            }
        }
        if (fired) {
            if (timer >= 1)
            {
                beam.SetActive(true);
                timer = 0;
            }
        }
    }
}
