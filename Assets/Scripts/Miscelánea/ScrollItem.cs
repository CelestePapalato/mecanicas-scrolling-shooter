using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollItem : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector2 direction = Vector2.left;

    void FixedUpdate()
    {
        transform.Translate(direction * _speed * Time.fixedDeltaTime);
    }
}
