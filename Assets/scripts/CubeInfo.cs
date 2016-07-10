using UnityEngine;
using System.Collections;

public class CubeInfo  {
	 
	private Vector3 position;
	private Quaternion rotation;

	public CubeInfo(){
		
	}
	public CubeInfo(Vector3 position,Quaternion rotation){
		this.position = position;
		this.rotation = rotation;

	}



	public Vector3 getPosition(){
		return position;
	}

	public Quaternion getRotation(){
		return rotation;
	}
}
