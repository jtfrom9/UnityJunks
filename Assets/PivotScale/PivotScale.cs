using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotScale : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [SerializeField]
    public GameObject parent;

    void scale(float scaley, bool pivot=false)
    {
        if (pivot == false)
        {
            var scale = target.transform.localScale;
            target.transform.localScale = new Vector3(scale.x, scaley, scale.z);
        }
        else
        {
            var scale = parent.transform.localScale;
            parent.transform.localScale = new Vector3(scale.x, scaley, scale.z);

            // scaley=2, target.scale.y=1
            // scaley=1, target.scale.y=0
            target.transform.localPosition = new Vector3(0, (scaley - 1)/2/scaley, 0);
        }
    }

    IEnumerator Start()
    {
        bool pivot = true;
        while (true)
        {
            yield return new WaitForSeconds(1);
            scale(3, pivot);
            yield return new WaitForSeconds(1);
            scale(2, pivot);
            yield return new WaitForSeconds(1);
            scale(1, pivot);
        }
    }
}
