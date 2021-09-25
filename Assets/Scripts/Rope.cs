using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lR;
    [SerializeField] Anchor anchor;

    private void Awake()
    {
        lR = GetComponent<LineRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lR.SetPosition(0, Vector2.zero);
        lR.SetPosition(1, anchor.transform.localPosition);
    }
}
