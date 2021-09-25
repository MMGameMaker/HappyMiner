using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mineral : MonoBehaviour
{
    protected int _value;

    protected GameObject target;

    protected bool isFollow;

    protected Vector2 followDirect;

    protected float followDistant;

    public int Value {
        get { return this._value; }
        protected set { this._value = value; }
    }

    protected float _weightRate;
    public float WeightRate { 
        get=>this._weightRate;
        protected set { this._weightRate = value; }
    }

    public virtual void MoveFollow(GameObject target)
    {
        followDirect = target.GetComponent<Rigidbody2D>().velocity.normalized;

        transform.rotation = Quaternion.Euler(0, 0, target.transform.rotation.eulerAngles.z-90);

        transform.position = target.transform.GetChild(0).transform.position - new Vector3(followDirect.x, followDirect.y)* followDistant;

        if (target.GetComponent<Anchor>().CheckFinishBack())
        {
            this.isFollow = false;
            target.transform.parent.transform.parent.GetComponent<Player>().AddMoney(this.Value);
            Destroy(this.gameObject);
        }
    }

}
