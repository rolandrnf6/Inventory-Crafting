using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeItem : MonoBehaviour
{
    [SerializeField] private int axeDamage = 5;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 4f))
            {
                if (hit.collider.GetComponent<TreeHealth>())
                {
                    hit.collider.GetComponent<TreeHealth>().takeDamage(axeDamage, transform.root.gameObject);
                    Debug.Log("hit");
                }
            }
        }
    }
}
