using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour,IClickAction,IHoverAction
{
    public Transform Cam;
    // Use this for initialization
    IEnumerator Start ()
	{
        while (!Display.IsInitialized())
        {
            yield return null;
        }

	    string helloWorld = "Hello World";
	    for (int i = 0; i < helloWorld.Length ; i++)
	    {
	        Cell cell = Display.CellAt(0, 10 + i, 5);
	        cell.SetContent(
	            helloWorld.Substring(i, 1),
	            Color.clear,
	            Color.red);
	    }

        for (int i = 0; i < helloWorld.Length ; i++)
        {
            Cell cell = Display.CellAt(0, 10 + i, 7);
            cell.SetContent(
                helloWorld.Substring(i, 1),
                Color.green,
                Color.red);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }
}
