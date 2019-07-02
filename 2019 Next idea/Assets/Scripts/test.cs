using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class test : UnitySingleton<test>
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("commit conflict");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
