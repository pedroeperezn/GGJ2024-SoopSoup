using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionCallback
{
    public void CollisionHandler(Collision2D collisionInfo);
    public void TriggerHandler(Collider2D other);
}
