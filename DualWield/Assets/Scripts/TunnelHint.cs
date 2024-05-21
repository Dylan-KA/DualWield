using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelHint : MonoBehaviour
{
    [SerializeField] GameObject Hint;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Transform>().childCount == 0)
        {
            Hint.transform.position = Player.transform.position;
        }
    }
}
