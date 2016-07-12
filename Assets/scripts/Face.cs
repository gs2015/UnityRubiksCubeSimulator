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

    private float r_sum = 0;

    private float f;

    private float deltaX = 0, deltaY = 0;

    //转动的是哪个方向上的层 只有RFU三个值
    private RotateDirection rotatedDirection;


    void Start () {
        Cube cube = gameObject.transform.parent.gameObject.GetComponent<Cube> ();
        layer = cube.getLayer ();
        rubiks = cube.getRubiks ();
        cubes = cube.getCubes ();
        center = cube.getCenter ();
        pos = cube.getPos ();
        f = cube.getF ();

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
                //		Debug.Log ("Mouse:"+mouseDirection);
            
                canJudgeMouseDirection = false;
               
            }
            if ((deltaX == 0 || deltaY == 0)) {
                Debug.Log ("deltaX:" + deltaX + ",deltaY:" + deltaY);
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
            layer.transform.Rotate (getRotateVector(rotatedDirection,-r_sum));
        } else if (r_sum >= 45 && r_sum <= 90) {
            layer.transform.Rotate (getRotateVector(rotatedDirection,90-r_sum));
        } else if (r_sum > 90 && r_sum <= 135) {
            layer.transform.Rotate (getRotateVector(rotatedDirection,90-r_sum));
        } else if (r_sum > 135 && r_sum <= 180) {
            layer.transform.Rotate (getRotateVector(rotatedDirection,180-r_sum));
        } else if (r_sum > -45 && r_sum <= 0) {
            layer.transform.Rotate (getRotateVector(rotatedDirection,-r_sum));
        } else if (r_sum > -90 && r_sum < -45) {
            layer.transform.Rotate (getRotateVector(rotatedDirection,-90-r_sum));
        } else if (r_sum > -135 && r_sum <= -90) {
            layer.transform.Rotate(getRotateVector(rotatedDirection,-180-r_sum));
        }

        r_sum = 0;
        foreach (GameObject o in cubes) {
            o.transform.parent = rubiks.transform;
        }
        layer.transform.rotation = Quaternion.identity;
		
    }


    private Vector3 getRotateVector(RotateDirection direction,float num){
        Vector3 v=Vector3.zero;
        switch (direction) {
        case RotateDirection.x:
            v = new Vector3 (num,0,0);
            break;
        case RotateDirection.y:
            v = new Vector3 (0,num,0);
            break;
        case RotateDirection.z:
            v = new Vector3 (0,0,num);
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
        //layer.transform.parent = rubiks.transform;

        foreach (GameObject o in cubes) {
            //Cube c = ;
            Vector3 v = o.GetComponent<Cube> ().getPos ();
            //要转动魔方的哪个面
            switch (face) {
            //先看点击的是魔方的哪个面
            case Direction.U:
            case Direction.D:
                switch (mouseDirection) {
                case Direction.U:
                case Direction.D:
                    if (v.x == pos.x) {
                        Debug.Log ("UDUD Rotate:" + v + "--" + pos);
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                case Direction.L:
                case Direction.R:
                    if (v.z == pos.z) {
                        Debug.Log ("UDLR Rotate:" + v + "--" + pos);
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                }
                break;
            case Direction.F: 
            case Direction.B: 
                Debug.Log ("mouseDirection：" + mouseDirection);
                switch (mouseDirection) {
                case Direction.U:
                case Direction.D:
                    if (v.x == pos.x) {
                        Debug.Log ("FBUD Rotate:" + v + "--" + pos);
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                case Direction.L:
                case Direction.R:
                    if (v.y == pos.y) {
                        Debug.Log ("FBLR Rotate:" + v + "--" + pos);
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
                        Debug.Log ("LRUD Rotate:" + v + "--" + pos);
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                case Direction.L:
                case Direction.R:
                    if (v.y == pos.y) {
                        Debug.Log ("LRLR Rotate:" + v + "--" + pos);
                        o.transform.parent = layer.transform;
                        cubesInLayer.Add (o);
                    }
                    break;
                }

                break;

            }
 

        }
        //	Debug.Log ("rotation:"+_rotation.ToString());
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
			sumX += pos.x*f;
			sumY += pos.y*f;
			sumZ += pos.z*f;

		}

	//	Vector3 minPos = cubesInLayer [0].transform.position;
	//	Vector3 maxPos = cubesInLayer [cubesInLayer.Count - 1].transform.position;
	//	float x = (maxPos.x * f + minPos.x * f) / 2;
	//	float y = (maxPos.y * f + minPos.y * f) / 2;
	//	float z = (maxPos.z * f + minPos.z * f) / 2;

		Vector3 center=new Vector3(sumX/cubesInLayer.Count,sumY/cubesInLayer.Count,sumZ/cubesInLayer.Count);
		 
		return center;
	}
	*/

}

