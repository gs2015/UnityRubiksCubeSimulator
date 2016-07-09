using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	
    public  Vector3 v3;

    public GameObject cube;
    //public Text txt;
    public GameObject rubiks;

	private List<GameObject> cubes = new List<GameObject>();

    public GameObject layer;

	private Vector3 center;


    void Start(){
		initV3Cube ();
		calcRubiksCenter ();
        
    }

    void Update(){

    }

    private void initV3Cube(){

        for (int i = 0; i < v3.x; i++){
            for (int j = 0; j < v3.y; j++){
                for (int k = 0; k < v3.z; k++){
                    Vector3 v = new Vector3(i, j, k);

                    if (atSurface(v)){
                        initCube(v);
                    }
                }
            }
        }
    }

	public Vector3 getCenter(){
		return center;
	}

    public List<GameObject> getCubes() {
        return cubes;
    }

    public GameObject getRubiks(){
        return rubiks;
    }

    public GameObject getLayer(){
        return layer;
    }
    
    private    float f = 1.1f;

    public float getF(){
        return f;
    }

    private void initCube(Vector3 v)
    {
		//txt.text = txt.text + " " + v.ToString();
        GameObject cube = Instantiate(this.cube);
        cube.transform.parent=rubiks.transform;
        cube.gameObject.transform.position = new Vector3(v.x*f, v.y*f, v.z*f);
        Cube c=cube.GetComponent<Cube>();
		c.setController(this);
		c.setPos(v);
        c.setV3 (v3);

		if(c.isR()){
			int childCount = cube.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				Transform child=cube.transform.GetChild (i);
				if (child.gameObject.name.Equals ("R")) {
					child.gameObject.GetComponent<Renderer> ().material.color = Color.red;
				}
			}
		}

		if(c.isU()){
			int childCount = cube.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				Transform child=cube.transform.GetChild (i);
				if (child.gameObject.name.Equals ("U")) {
					child.gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
				}
			}
		}

		if(c.isF()){
			int childCount = cube.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				Transform child=cube.transform.GetChild (i);
				if (child.gameObject.name.Equals ("F")) {
					child.gameObject.GetComponent<Renderer> ().material.color = Color.blue;
				}
			}
		}

		if(c.isL()){
			int childCount = cube.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				Transform child=cube.transform.GetChild (i);
				if (child.gameObject.name.Equals ("L")) {
					child.gameObject.GetComponent<Renderer> ().material.color =new Color(1.0f, 0.6f, 0);//orange
				}
			}
		}

		if(c.isB()){
			int childCount = cube.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				Transform child=cube.transform.GetChild (i);
				if (child.gameObject.name.Equals ("B")) {
					child.gameObject.GetComponent<Renderer> ().material.color = Color.green;
				}
			}
		}

		if(c.isD()){
			int childCount = cube.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				Transform child=cube.transform.GetChild (i);
				if (child.gameObject.name.Equals ("D")) {
					child.gameObject.GetComponent<Renderer> ().material.color = Color.white;
				}
			}
		}

       cubes.Add(cube);
    }

    private bool atSurface(Vector3 v)
    {
        bool flag = false;
        if (v.x == 0 || v.y == 0 || v.z == 0)
        {
            flag = true;
        }
        if (v.x == v3.x - 1 || v.y == v3.y - 1 || v.z == v3.z - 1)
        {
            flag = true;
        }
        return flag;
    }


	private void calcRubiksCenter(){

		Vector3 maxPos = cubes [0].GetComponent<Cube>().getPos();
		Vector3 minPos = cubes [cubes.Count - 1].GetComponent<Cube>().getPos();

		center=new Vector3((maxPos.x*f+minPos.x*f)/2,(maxPos.y*f+minPos.y*f)/2,(maxPos.z*f+minPos.z*f)/2);

		 

	}

 
}
