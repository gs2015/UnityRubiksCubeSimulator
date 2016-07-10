using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour {


	private GameController controller;

	//当前方块坐标
	private Vector3 pos;
	private int num;

	private float _sensitivity;
	private Vector3 mousePos;
	private Vector3 _mouseOffset;
	private Vector3 _rotation;
	private bool _isRotating;


	private Quaternion _lookRotation;
	private Vector3 _direction;


	//所有块的集合
	private List<GameObject> cubes;
 
	//旋转时的一层
	private GameObject layer;
	//所有块的父物体
	private GameObject rubkis;

	//魔方的长宽高
	private Vector3 v3;

	private float f;

	private Vector3 center;



	void Start () {

		layer = controller.getLayer ();
		cubes = controller.getCubes ();
		rubkis = controller.getRubiks ();
		f = controller.getF ();
		center = controller.getCenter ();

	}


	void Update () {

	}



	public Vector3 getCenter () {
		return center;
	}

	public float getF () {
		return f;
	}

	public void setNum (int num) {
		this.num = num;
	}

	public int getNum () {
		return this.num;
	}

	public void setPos (Vector3 v) {
		this.pos = v;
	}

	public Vector3 getPos () {
		return this.pos;
	}

	public void setController (GameController controller) {
		this.controller = controller;
	}

	public void setV3 (Vector3 v3) {
		this.v3 = v3;
	}

	public GameObject getLayer () {
		return layer;
	}

	public GameObject getRubiks () {
		return rubkis;
	}

	public  List<GameObject>  getCubes () {
		return cubes;	
	}




	public bool isR () {
		return getPos ().x == v3.x - 1;
	}

	public bool isU () {
		return getPos ().y == v3.y - 1;
	}

	public bool isB () {
		return getPos ().z == v3.z - 1;
	}

	public bool isF () {
		return getPos ().z == 0;
	}

	public bool isL () {
		return getPos ().x == 0;
	}

	public bool isD () {
		return getPos ().y == 0;
	}
 
    
}
