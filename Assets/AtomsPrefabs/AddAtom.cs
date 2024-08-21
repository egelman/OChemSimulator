using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAtom : MonoBehaviour
{
    public string atomType;
    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "Nitrogen")
        {
            atomType = "Nitrogen";
        }
        if (this.tag == "Carbon")
        {
            atomType = "Carbon";
        }
        if (this.tag == "Oxygen")
        {
            atomType = "Oxygen";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
