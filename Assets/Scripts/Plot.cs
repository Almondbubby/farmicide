using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public Color defaultColor;
    public Marker closest;
    float dist = Single.MaxValue;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _sprite.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (closest)
        {
            var col = closest.owner.color;
            col.a = defaultColor.a;
            _sprite.color = col;
        }
    }

    public void Recompute()
    {
        closest = null;
        dist = float.MaxValue;
        foreach (var marker in FindObjectsOfType<Marker>())
        {
            var sqm = (marker.transform.position - transform.position).sqrMagnitude;
            if (sqm < dist)
            {
                closest = marker;
                dist = sqm;
            }
        }

        if (closest && Mathf.Pow(dist, 2) >= closest.radius)
        {
            closest = null;
        }
    }
}
