using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Item _itemToDrop;
    [SerializeField][Range(0f, 1f)] float _probability;

    public void DropItem()
    {
        if (CalculateProbability())
        {
            InstanceItem();
        }
    }

    private bool CalculateProbability()
    {
        float value = Random.Range(0f, 1f);
        return value <= _probability;
    }

    private void InstanceItem()
    {
        Instantiate(_itemToDrop, transform.position, Quaternion.identity);
    }
}
