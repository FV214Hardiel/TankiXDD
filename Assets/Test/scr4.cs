using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr4 : MonoBehaviour
{
    List<testClass> testList;
    void Start()
    {
        testList = new();
        testList.Add(new testClass());
        testList.Add(new testInherit());
        testList.Add(new testClass());


        StartCoroutine(CustomUpdate());
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    foreach (var item in testList)
    //    {
    //        if (item.TestMethod(Time.deltaTime) <= 0)
    //        {
    //            testList.Remove(item);
    //            break;
    //        }


    //    }

    //    Debug.Log(testList.Count);
        
    //}

    public IEnumerator CustomUpdate()
    {
        
        while (true)
        {
            Debug.Log(Time.time);
            yield return new WaitForSeconds(2);
        }
    }
}

public class testClass
{
    protected float varA;
    protected float varB;

    public testClass()
    {
        varA = 3;
        varB = varA;
    }

    public virtual float TestMethod(float time)
    {
        varB -= time;
        return varB;
    }

    public override string ToString()
    {
        return (varA.ToString() + " " + varB.ToString() + ";");
    }
}

public class testInherit : testClass
{
    public testInherit()
    {
        varA = 15f;
        varB = 10f;
    }

    public override float TestMethod(float time)
    {
        varB -= time;
        return varB;
    }
}
