using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class egg : MonoBehaviour
{

    int clicks = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "rpgpp_lt_tree_pine_01 (4)")
                {
                    clicks++;
                }
            }
        }

        if (clicks >= 10)
        {
            GameObject temp = GameObject.Find("rpgpp_lt_tree_pine_01 (4)");
            if (temp)
            {
                Destroy(temp.gameObject);
            }
        }
    }
}
