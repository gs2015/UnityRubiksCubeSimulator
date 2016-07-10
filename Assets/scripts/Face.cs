using UnityEngine;
using System.Collections.Generic;


public class Face : MonoBehaviour{

	public string face ;

	private Vector3 mousePos;
	private bool _isRotating;
	private GameObject rubiks;
	private List<GameObject> cubes;

	private Vector3 _mouseOffset;
	private Vector3 _rotation;
	private float _sensitivity;

	private Vector3 center;
	private Vector3 pos;

	float r_sum = 0;
	private GameObject layer;

	void Start(){
		Cube cube = gameObject.transform.parent.gameObject.GetComponent<Cube> ();
		layer = cube.getLayer ();
		rubiks=cube.getRubiks ();
		cubes=cube.getCubes ();
		center = cube.getCenter ();
		pos = cube.getPos ();

		_sensitivity = 0.4f;
		_rotation = Vector3.zero;


	}


	void Update(){
		if (_isRotating) {
			
			_mouseOffset = (Input.mousePosition - mousePos);
			_rotation.x = _mouseOffset.y * _sensitivity;
			Vector3 lastPos = mousePos;
			mousePos = Input.mousePosition;
		


			//思路：把所有需要旋转的块放到一个父物体中，旋转父物体
			layer.transform.position = center;
			layer.transform.parent = rubiks.transform;
			foreach (GameObject o in cubes) {
				Vector3 v = o.GetComponent<Cube> ().getPos ();
				if (v.x == pos.x) {
					o.transform.parent = layer.transform;
				}
			}

			layer.transform.Rotate (_rotation);
			r_sum += _rotation.x;
		}
	}

	void OnMouseDown(){

		_isRotating = true;
		mousePos = Input.mousePosition;


		Debug.Log ("face:"+face);
		
	}

	void OnMouseUp(){

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
            o.transform.parent = rubiks.transform;
        }
		
	}
}

