using UnityEngine;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour {

    public GameController controller;
    private List<GameObject> cubes;
    private List<CubeInfo> originalCubesCoords;
    private GameObject rubiks;



    private float value = 0.1f;

    void Start () {
        cubes = controller.getCubes ();
        originalCubesCoords = controller.getOriginalCubesCoords ();
        rubiks = controller.getRubiks ();
    
    
    }

    public void OnBtnResetClick () {
        for (int i = 0; i < cubes.Count; i++) {
            cubes [i].transform.position = originalCubesCoords [i].getPosition ();
            cubes [i].transform.rotation = originalCubesCoords [i].getRotation ();

        }
    }

    public void PositionXPlus () {
        rubiks.transform.position = rubiks.transform.position + new Vector3 (value, 0, 0);
     
    }

    public void PositionXMinus () {
        rubiks.transform.position = rubiks.transform.position + new Vector3 (-value, 0, 0);
      

    }

    public void PositionYPlus () {
        rubiks.transform.position = rubiks.transform.position + new Vector3 (0, value, 0);
       
    }

    public void PositionYMinus () {
        rubiks.transform.position = rubiks.transform.position + new Vector3 (0, -value, 0);
        
    }

    public void PositionZPlus () {
        rubiks.transform.position = rubiks.transform.position + new Vector3 (0, 0, value);
         
    }

    public void PositionZMinus () {
        rubiks.transform.position = rubiks.transform.position + new Vector3 (0, 0, -value);
         
    }



    public void RotationXPlus () {
        rubiks.transform.Rotate (new Vector3 (value, 0, 0));

    }

    public void RotationXMinus () {
        rubiks.transform.Rotate (new Vector3 (-value, 0, 0));
    }

    public void RotationYPlus () {
        rubiks.transform.Rotate (new Vector3 (0, value, 0));
    }

    public void RotationYMinus () {
        rubiks.transform.Rotate (new Vector3 (0, -value, 0));
    }

    public void RotationZPlus () {
        rubiks.transform.Rotate (new Vector3 (0, 0, value));
    }

    public void RotationZMinus () {
        rubiks.transform.Rotate (new Vector3 (0, 0, -value));
    }



}
