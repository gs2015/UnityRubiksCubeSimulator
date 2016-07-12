using UnityEngine;
using System.Collections.Generic;


public class Face : MonoBehaviour {

    public enum Direction {
        U,
        F,
        R,
        D,
        L,
        B
    }

    public enum RotateDirection {
        x,
        y,
        z
            
    }

    public Direction face;

    private Vector3 mousePos;
    private bool _isRotating;
    private GameObject rubiks;
    private List<GameObject> cubes;

    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private float _sensitivity;

    private Vector3 center;
    private Vector3 pos;
    private GameObject layer;

    private Direction mouseDirection;

    private bool canJudgeMouseDirection;

    private List<GameObject> cubesInLayer = new List<GameObject> ();
    private GameObject cubePrefab;

    private float r_sum = 0;

    private float f;

    private float deltaX = 0, deltaY = 0;

    //转动的是哪个方向上的层(x,y,z)
    private RotateDirection rotatedDirection;


    void Start () {
        Cube cube = gameObject.transform.parent.gameObject.GetComponent<Cube> ();
        layer = cube.getLayer ();
        rubiks = cube.getRubiks ();
        cubes = cube.getCubes ();

        pos = cube.getPos ();
        f = cube.getF ();
        cubePrefab = cube.getCubePrefab ();
        center = cube.getCenter ();

        _sensitivity = 0.4f;
        _rotation = Vector3.zero;


    }


    void Update () {
        if (_isRotating) {
			
            _mouseOffset = (Input.mousePosition - mousePos);
 
            Vector3 lastPos = mousePos;
            mousePos = Input.mousePosition;

            deltaX = mousePos.x - lastPos.x;
            deltaY = mousePos.y - lastPos.y;

            if (canJudgeMouseDirection && (deltaX != 0 || deltaY != 0)) {

                if (Mathf.Abs (deltaX) > Mathf.Abs (deltaY)) {
                    if (deltaX > 0) {
                        mouseDirection = Direction.R;
                    } else {
                        mouseDirection = Direction.L;
                    }
                } else {
                    if (deltaY > 0) {
                        mouseDirection = Direction.U;
                    } else {
                        mouseDirection = Direction.D;
                    }
                }
           
                canJudgeMouseDirection = false;
               
            }
    

            rotateLayer ();


        }
    }

    void OnMouseDown () {

	
        _isRotating = true;
        mousePos = Input.mousePosition;

        canJudgeMouseDirection = true;

        Debug.Log ("pos:" + pos);
		
    }

    void OnMouseUp () {

        _isRotating = false;
        _rotation = Vector3.zero;



        if (r_sum > 0 && r_sum <= 45) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -r_sum));
        } else if (r_sum >= 45 && r_sum <= 90) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 90 - r_sum));
        } else if (r_sum > 90 && r_sum <= 135) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 90 - r_sum));
        } else if (r_sum > 135 && r_sum <= 180) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 180 - r_sum));
        } else if (r_sum > 180 && r_sum <= 225) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 180 - r_sum));
        } else if (r_sum > 225 && r_sum <= 270) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 270 - r_sum));
        } else if (r_sum > 270 && r_sum <= 315) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 270 - r_sum));
        } else if (r_sum > 315 && r_sum <= 360) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, 360 - r_sum));
        
        } else if (r_sum < 0 && r_sum >= -45) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -r_sum));
        } else if (r_sum <= -45 && r_sum >= -90) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -90 - r_sum));
        } else if (r_sum < -90 && r_sum >= -135) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -90 - r_sum));
        } else if (r_sum < -135 && r_sum >= -180) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -180 - r_sum));
        } else if (r_sum < -180 && r_sum >= -225) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -180 - r_sum));
        } else if (r_sum < -225 && r_sum >= -270) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -270 - r_sum));
        } else if (r_sum < -270 && r_sum >= -315) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -270 - r_sum));
        } else if (r_sum < -315 && r_sum >= -360) {
            layer.transform.Rotate (getRotateVector (rotatedDirection, -360 - r_sum));

        }

        

        r_sum = 0;
        foreach (GameObject o in cubes) {
            o.transform.parent = rubiks.transform;
        }
        layer.transform.rotation = Quaternion.identity;
		
    }


    private Vector3 getRotateVector (RotateDirection direction, float num) {
        Vector3 v = Vector3.zero;
        switch (direction) {
        case RotateDirection.x:
            v = new Vector3 (num, 0, 0);
            break;
        case RotateDirection.y:
            v = new Vector3 (0, num, 0);
            break;
        case RotateDirection.z:
            v = new Vector3 (0, 0, num);
            break;

        }
        return v;
        
    }

    private void rotateLayer () {
        if (deltaX == 0 && deltaY == 0) {
            return;
        }

        //根据点到的面 鼠标的方向不同 旋转的方向不同

        //点到前面 上下移动 
        switch (face) {
        case Direction.U:
        case Direction.D:
            switch (mouseDirection) {
            case Direction.U:
            case Direction.D:
                _rotation.x = _mouseOffset.y * _sensitivity;	
                rotatedDirection = RotateDirection.x;
                break;
            case Direction.L:
            case Direction.R:
                _rotation.y = -_mouseOffset.x * _sensitivity;
                rotatedDirection = RotateDirection.z;
                break;
            }
            break;
        case Direction.F: 
        case Direction.B:
            switch (mouseDirection) {
            case Direction.U:
            case Direction.D:
                _rotation.x = _mouseOffset.y * _sensitivity;	
                rotatedDirection = RotateDirection.x;
                break;
            case Direction.L:
            case Direction.R:
                _rotation.y = -_mouseOffset.x * _sensitivity;
                rotatedDirection = RotateDirection.y;
                break;
            }
            break;
        case Direction.L:
        case Direction.R:
            switch (mouseDirection) {
            case Direction.U:
            case Direction.D:
                _rotation.z = _mouseOffset.y * _sensitivity;
                rotatedDirection = RotateDirection.z;
                break;
            case Direction.L:
            case Direction.R:
                _rotation.y = -_mouseOffset.x * _sensitivity;
                rotatedDirection = RotateDirection.y;
                break;
            }
            break;
	 
		
        }
		 
        //思路：把所有需要旋转的块放到一个父物体中，旋转父物体
        layer.transform.position = center;
         
        foreach (GameObject o in cubes) {
            //Cube c = ;
            Vector3 v = o.GetComponent<Cube> ().getPos ();
            switch (face) {
            case Direction.U:
            case Direction.D:
                switch (mouseDirection) {
                case Direction.U:
                case Direction.D:
                    if (v.x == pos.x) {
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                case Direction.L:
                case Direction.R:
                    if (v.z == pos.z) {
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                }
                break;
            case Direction.F: 
            case Direction.B: 
                switch (mouseDirection) {
                case Direction.U:
                case Direction.D:
                    if (v.x == pos.x) {
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                case Direction.L:
                case Direction.R:
                    if (v.y == pos.y) {
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                }
				
                break;

            case Direction.L:
            case Direction.R:

                switch (mouseDirection) {
                case Direction.U:
                case Direction.D:
                    if (v.z == pos.z) {
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                case Direction.L:
                case Direction.R:
                    if (v.y == pos.y) {
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                }

                break;

            }
 

        }
        //	Debug.Log ("rotation:"+_rotation.ToString());
        // center = getCenter ();
        layer.transform.Rotate (_rotation);
        r_sum += _rotation.x == 0 ? (_rotation.y == 0 ? _rotation.z : _rotation.y) : _rotation.x;
    }

    /*
	private Vector3 getCenter(){
		
		float sumX = 0;
		float sumY = 0;
		float sumZ = 0;

		foreach (GameObject o in cubesInLayer) {
			Vector3 pos=o.GetComponent<Cube> ().getPos ();
			sumX += pos.x;
			sumY += pos.y;
			sumZ += pos.z;

		}
        
        Vector3 center=new Vector3(sumX/cubesInLayer.Count,sumY/cubesInLayer.Count,sumZ/cubesInLayer.Count);


		Vector3 minPos = cubesInLayer [0].transform.position;
		Vector3 maxPos = cubesInLayer [cubesInLayer.Count - 1].transform.position;
        float width = 0;// cubePrefab.GetComponentInChildren<Renderer> ().bounds.size.x/2;
        float x = (maxPos.x  + minPos.x ) / 2+width;
        float y = (maxPos.y  + minPos.y ) / 2+width;
        float z = (maxPos.z  + minPos.z ) / 2+width;

        Vector3 center = new Vector3 (x, y, z);
        GameObject ccc = Instantiate (cubePrefab);
        ccc.transform.position = center;
		 
		return center;
	}
	*/

}

