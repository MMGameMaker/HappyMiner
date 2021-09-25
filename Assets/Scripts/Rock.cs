using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Mineral
{
    // Start is called before the first frame update
    void Start()
    {
        Value = 3;
        WeightRate = 0.8f;
        followDistant = this.gameObject.GetComponent<Collider2D>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollow)
        {
            MoveFollow(target);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Anchor>().MoveBack(this.WeightRate);
            this.target = collision.gameObject;
            this.isFollow = true;
        }
    }

    public override void MoveFollow(GameObject target)
    {
        base.MoveFollow(target);
    }

}
