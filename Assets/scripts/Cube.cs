using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour {


    private Controller controller;

    //当前方块坐标
    private Vector3 pos;

    private float _sensitivity;
    private Vector3 mousePos;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;


    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;


    //所有块的集合
    private List<GameObject> cubes;
    //坐标和最大、最小的块 用于求面的中点坐标
    private GameObject minCube, maxCube;
 
    //魔方中心坐标
    private Vector3 center;

    //旋转时的一层
    private GameObject layer;
    //所有块的父物体
    private GameObject rubkis;

    //魔方的长宽高
    private Vector3 v3;


    public void setPos(Vector3 v) {
        this.pos = v;
    }
    public Vector3 getPos() {
        return this.pos;
    }
    public void setController(Controller controller) {
        this.controller = controller;
    }
	public void setV3(Vector3 v3){
		this.v3 = v3;
	}

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;

         cubes = controller.getCubes();
         rubkis = controller.getRubiks();
         layer =controller.getLayer();
		 center = controller.getCenter ();

    }

    float r_sum = 0;
    void Update()
    {
        if (_isRotating)
        {
            _mouseOffset = (Input.mousePosition - mousePos);
			_rotation.x = _mouseOffset.y * _sensitivity;
			Vector3 lastPos = mousePos;
			mousePos = Input.mousePosition;
	//		Debug.Log ("lastPos:"+lastPos);
	//		Debug.Log ("Pos:"+mousePos);



			//思路：把所有需要旋转的块放到一个父物体中，旋转父物体
            layer.transform.position = center;
            layer.transform.parent= rubkis.transform;
            foreach(GameObject o in cubes){
                Vector3 v=o.GetComponent<Cube>().getPos();
                if (v.x == pos.x) {
                    o.transform.parent = layer.transform;
                }
            }
            
            layer.transform.Rotate(_rotation);
            r_sum+=_rotation.x;
           
            
        }
    }

    void OnMouseDown()
    {
        _isRotating = true;
		mousePos = Input.mousePosition;
 
			

       Debug.Log(pos+".");

    }

    void OnMouseUp()
    {
        _isRotating = false;

		if (r_sum > 0 && r_sum <= 20) {
			layer.transform.Rotate (new Vector3 (-r_sum, 0, 0));
		} else if (r_sum > 20 && r_sum <= 90) {
			layer.transform.Rotate (new Vector3 (90 - r_sum, 0, 0));
		} else if (r_sum > 90 && r_sum <= 180) {
			layer.transform.Rotate (new Vector3 (180 - r_sum, 0, 0));
		} else if (r_sum > -20 && r_sum <= 0) {
			layer.transform.Rotate (new Vector3 (-r_sum, 0, 0));
		} else if(r_sum>-90&&r_sum<-20){
			layer.transform.Rotate(new Vector3(-90-r_sum,0,0));
		} else if(r_sum>-180&&r_sum<=-90){
			layer.transform.Rotate(new Vector3(-180-r_sum,0,0));
		}

        r_sum = 0;
		foreach (GameObject o in cubes){
            o.transform.parent = rubkis.transform;
        }

    }



    public bool isR(){
		return getPos().x==v3.x-1;
    }
    public bool isU(){
        return getPos().y==v3.y-1;
    }
	public bool isB(){
		return getPos().z==v3.z-1;
	}
    public bool isF(){
        return getPos().z==0;
    }
    public bool isL(){
        return getPos().x==0;
    }
    public bool isD(){
        return getPos().y==0;
    }
 
    
}
