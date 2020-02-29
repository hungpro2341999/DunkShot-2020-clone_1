// FX Quest version: 0.3.0
// Author: Gold Experience Team (http://www.ge-team.com/pages)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion

// Flow behavior of cloud 
public enum eCloudFlowBehavior
{
	SwitchLeftRight,
	FlowTheSameWay
}

// ######################################################################
// This class describes cloud information
// ######################################################################

[System.Serializable]
public class Cloud
{
	public float m_MoveSpeed;						// Move speed of cloud
	public GameObject m_Cloud;						// Handle of cloud's gameObject
	public GameObject m_CloudFollower;				// Handle of duplicate of m_Cloud
	public Vector3 m_OriginalLocalPos;				// LocalPosition before first Update() is called
}

// ######################################################################
// This class handles cloud sprite, move them around and pool them to the other side of screen when them move off from the edge ot the orthographic camera view
// ######################################################################

public class EQ_CloudFlow : MonoBehaviour
{
	
	// ######################################################################
	// Variables
	// ######################################################################

	#region Variables

	[HideInInspector]								// Remark this line if you want to see each cloud's details
	public Cloud[] m_CloudList = null;				// Array of cloud

	public bool m_EnableLargeCloudLoop = false;		// True if this cloud is larger or fit the screen size
	public eCloudFlowBehavior m_Behavior = eCloudFlowBehavior.FlowTheSameWay;			// Flow behavior of cloud

	public float m_MinSpeed = 0.05f;				// Amount of slowest cloud
	public float m_MaxSpeed = 0.3f;					// Amount of fastest cloud

	public Camera m_Camera = null;					// Handle Orthographic Camera in the scene

	Vector3 LeftMostOfScreen;						// Vector3 of middle-left most position at the edge of the camera view
	Vector3 RightMostOfScreen;						// Vector3 of middle-right most position at the edge of the camera view

	#endregion
	
	// ######################################################################
	// MonoBehaviour Functions
	// ######################################################################

	#region Monobehavior
	
	// Use this for initialization
	void Start()
	{
		// init m_CloudList
		m_CloudList = new Cloud[transform.childCount];

		// Random first direction
		int RandomDirection = Random.Range(0,2);

		// Survey all chilren and put them to m_CloudList
		int index = 0;
		foreach(Transform child in transform)
		{
			// Create new Cloud class
			m_CloudList[index] = new Cloud();

			// Random speed
			m_CloudList[index].m_MoveSpeed = Random.Range(m_MinSpeed,m_MaxSpeed);

			// Left/right direction
			if(RandomDirection==0)
			{
				m_CloudList[index].m_MoveSpeed *= -1;
				if(m_Behavior == eCloudFlowBehavior.SwitchLeftRight)
				{
					RandomDirection=1;
				}
			}
			else
			{
				if(m_Behavior == eCloudFlowBehavior.SwitchLeftRight)
				{
					RandomDirection=0;
				}
			}
			
			// Set this gameObject to current m_CloudList.m_Cloud
			m_CloudList[index].m_Cloud = child.gameObject;

			// If this is Large Cloud then duplicate current Cloud, we will use it to make seamless move on the screen
			if(m_EnableLargeCloudLoop==true)
			{
				m_CloudList[index].m_CloudFollower = (GameObject) Instantiate(child.gameObject);
			}

			// Keep original LocalPosition to use later when we have to pool this cloud when it move off the screen edge
			m_CloudList[index].m_OriginalLocalPos = m_CloudList[index].m_Cloud.transform.localPosition;

			// Increase index
			index++;
		}
		
		// Move Duplicated clouds as children of this gameObject
		if(m_EnableLargeCloudLoop==true)
		{
			foreach(Cloud child in m_CloudList)
			{
				child.m_CloudFollower.transform.parent = this.transform;
			}
		}

		// Make sure we have Orthographic Camera
		FindTheOrthographicCamera();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Make sure we have Orthographic Camera
		if(m_Camera==null)
		{
			FindTheOrthographicCamera();
		}
		// Log warning if there is no Orthographic Camera
		if(m_Camera==null)
		{
			Debug.LogWarning("There is no Orthographic camera in the scene.");

			// Update nothing
			return;
		}


		// Update all children cloud
		int index = 0;
		foreach(Cloud child in m_CloudList)
		{
			if(child.m_Cloud.activeSelf==true)
			{
				// Move current cloud
				m_CloudList[index].m_Cloud.transform.localPosition = new Vector3(m_CloudList[index].m_Cloud.transform.localPosition.x + (m_CloudList[index].m_MoveSpeed * Time.deltaTime),
				                                                                 m_CloudList[index].m_Cloud.transform.localPosition.y,
				                                                                 m_CloudList[index].m_Cloud.transform.localPosition.z);


				// If Move Left to Right
				if(m_CloudList[index].m_MoveSpeed>0)
				{
					// Move duplicated cloud
					if(m_CloudList[index].m_CloudFollower!=null)
					{
						m_CloudList[index].m_CloudFollower.transform.localPosition = new Vector3(m_CloudList[index].m_Cloud.transform.localPosition.x-(m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.x),
						                                                                         m_CloudList[index].m_Cloud.transform.localPosition.y,
						                                                                         m_CloudList[index].m_Cloud.transform.localPosition.z);
					}

					// Is this cloud move off right most of the camera edge?
					if(m_CloudList[index].m_Cloud.transform.localPosition.x>RightMostOfScreen.x+m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.x/2)
					{
						if(m_EnableLargeCloudLoop==true)
						{
							// Switch cloud and duplicated cloud
							GameObject TempGO = m_CloudList[index].m_Cloud;
							m_CloudList[index].m_Cloud = m_CloudList[index].m_CloudFollower;
							m_CloudList[index].m_CloudFollower = TempGO;
						}
						else
						{
							// Random new speed
							m_CloudList[index].m_MoveSpeed = Random.Range(m_MinSpeed,m_MaxSpeed);

							// Pool cloud to other side of screen
							m_CloudList[index].m_Cloud.transform.localPosition = new Vector3(LeftMostOfScreen.x-m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.x,
							                                                                 Random.Range(-m_Camera.orthographicSize/2, m_Camera.orthographicSize/2),
							                                                                 m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.z);
						}
					}
				}
				// If Move Right to Left
				else
				{
					// Move duplicated cloud
					if(m_CloudList[index].m_CloudFollower!=null)
					{
						m_CloudList[index].m_CloudFollower.transform.localPosition = new Vector3(m_CloudList[index].m_Cloud.transform.localPosition.x+(m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.x),
						                                                                         m_CloudList[index].m_Cloud.transform.localPosition.y,
						                                                                         m_CloudList[index].m_Cloud.transform.localPosition.z);
					}
					
					// Is this cloud move off left most of the camera edge?
					if(m_CloudList[index].m_Cloud.transform.localPosition.x<LeftMostOfScreen.x-m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.x/2)
					{
						if(m_EnableLargeCloudLoop==true)
						{
							// Switch cloud and duplicated cloud
							GameObject TempGO = m_CloudList[index].m_Cloud;
							m_CloudList[index].m_Cloud = m_CloudList[index].m_CloudFollower;
							m_CloudList[index].m_CloudFollower = TempGO;
						}
						else
						{
							// Random new speed
							m_CloudList[index].m_MoveSpeed = -Random.Range(m_MinSpeed,m_MaxSpeed);

							// Pool cloud to other side of screen
							m_CloudList[index].m_Cloud.transform.localPosition = new Vector3(RightMostOfScreen.x+m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.x,
							                                                                 Random.Range(m_CloudList[index].m_OriginalLocalPos.y-m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.y, m_CloudList[index].m_OriginalLocalPos.y+m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.y),
							                                                                 m_CloudList[index].m_Cloud.GetComponent<Renderer>().bounds.size.z);
						}
					}
				}


			}



			index++;
		}
	}
	
	#endregion
	
	// ######################################################################
	// Functions
	// ######################################################################
	
	#region Functions

	// Find an Orthographic camera in the scene
	void FindTheOrthographicCamera()
	{
		if(m_Camera==null)
		{
			Camera[] CameraList = FindObjectsOfType<Camera>();
			foreach(Camera child in CameraList)
			{
				if(child.orthographic==true)
				{
					// Keep only first Orthographic Camera
					m_Camera = child;
					break;
				}
			}
		}
		
		// Calculate Left/Right most position at the edge of camera view
		if(m_Camera!=null)
		{
			LeftMostOfScreen = m_Camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
			RightMostOfScreen = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
		}
	}
	
	#endregion
}