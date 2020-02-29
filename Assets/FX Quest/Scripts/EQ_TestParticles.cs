// FX Quest version: 0.3.0
// Author: Gold Experience Team (http://www.ge-team.com/pages)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion

/***************
* TestParticles class
* This class handles user key inputs, play and stop all particle effects.
* 
* Up/Down buttons to switch Category
* Left/Right buttons to switch Particle.
**************/

public class EQ_TestParticles : MonoBehaviour
{
	
	#region Variables
	
	// Elements
	public Transform[] m_CategoryList;
	
	// Index of current element
	int m_CurrentCategoryIndex = 0;
	int m_CurrentCategoryIndexOld = -1;
	int m_CurrentCategoryChildCount = 0;

	// Index of current particle
	int m_CurrentParticleIndex = 0;
	int m_CurrentParticleIndexOld = -1;

	ParticleSystem m_CurrentParticle = null;

	string m_CurrentCategoryName = "";
	string m_CurrentParticleName = "";
	
	#endregion
	
	// ######################################################################
	// MonoBehaviour Functions
	// ######################################################################

	#region MonoBehaviour
	
	// Use this for initialization
	void Start ()
	{

		// Check if there is any particle in prefab list
		if(m_CategoryList.Length>0)
		{
			// reset indices of element and particle
			m_CurrentCategoryIndex = 0;
			m_CurrentCategoryIndexOld = -1;
			m_CurrentParticleIndex = 0;
			m_CurrentParticleIndexOld = -1;
		
			// Show particle
			ShowParticle();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

		// User released Up arrow key
		if(Input.GetKeyUp(KeyCode.UpArrow))
		{
			m_CurrentCategoryIndexOld = m_CurrentCategoryIndex;
			m_CurrentCategoryIndex++;
			m_CurrentParticleIndex = 0;
			ShowParticle();
		}
		// User released Down arrow key
		else if(Input.GetKeyUp(KeyCode.DownArrow))
		{
			m_CurrentCategoryIndexOld = m_CurrentCategoryIndex;
			m_CurrentCategoryIndex--;
			m_CurrentParticleIndex = 0;
			ShowParticle();
		}
		// User released Left arrow key
		else if(Input.GetKeyUp(KeyCode.LeftArrow))
		{
			m_CurrentParticleIndexOld = m_CurrentParticleIndex;
			m_CurrentParticleIndex--;
			ShowParticle();
		}
		// User released Right arrow key
		else if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			m_CurrentParticleIndexOld = m_CurrentParticleIndex;
			m_CurrentParticleIndex++;
			ShowParticle();
		}
	}

	// OnGUI is called for rendering and handling GUI events.
	void OnGUI ()
	{
	
		// Show version number
		GUI.Window(1, new Rect((Screen.width-260), 5, 250, 105), AppNameWindow, "FX Quest 0.3.0");

		// Show Scene GUI window
		GUI.Window(2, new Rect((Screen.width-300), Screen.height-150, 290, 60), SceneWindow, "Scenes");
		
		// Show Information GUI window
		GUI.Window(3, new Rect((Screen.width-410), Screen.height-85, 400, 80), ParticleInformationWindow, "Information");
	}
	
	#endregion
	
	// ######################################################################
	// Functions Functions
	// ######################################################################

	#region Functions
	
	// Remove old Particle and do Create new Particle GameObject
	void ShowParticle()
	{
		// Clamp m_CurrentCategoryIndex
		if(m_CurrentCategoryIndex>=m_CategoryList.Length)
		{
			m_CurrentCategoryIndex = 0;
		}
		else if(m_CurrentCategoryIndex<0)
		{
			m_CurrentCategoryIndex = m_CategoryList.Length-1;
		}

		int index = 0;
		if(m_CurrentCategoryIndex!=m_CurrentCategoryIndexOld)
		{
			// Disable all m_CategoryList[m_CurrentCategoryIndexOld]
			if(m_CurrentCategoryIndexOld>=0)
			{
				index = 0;
				foreach(Transform child in m_CategoryList[m_CurrentCategoryIndexOld])
				{
					m_CurrentParticle = child.gameObject.GetComponent<ParticleSystem>();
					if(m_CurrentParticle!=null)
					{
						m_CurrentParticle.Stop();
						m_CurrentParticle.gameObject.SetActive(false);
					}
					
					index++;
				}
			}

			// Disable all m_CategoryList[m_CurrentCategoryIndex]
			if(m_CurrentCategoryIndex>=0)
			{
				index = 0;
				foreach(Transform child in m_CategoryList[m_CurrentCategoryIndex])
				{
					m_CurrentParticle = child.gameObject.GetComponent<ParticleSystem>();
					if(m_CurrentParticle!=null)
					{
						m_CurrentParticle.Stop();
						m_CurrentParticle.gameObject.SetActive(false);
					}
					
					index++;
				}
			}

			if(m_CurrentCategoryIndexOld>=0)
			{
				m_CategoryList[m_CurrentCategoryIndexOld].gameObject.SetActive(false);
			}
			if(m_CurrentCategoryIndex>=0)
			{
				m_CategoryList[m_CurrentCategoryIndex].gameObject.SetActive(true);
			}

			m_CurrentCategoryName = m_CategoryList[m_CurrentCategoryIndex].name;
			m_CurrentCategoryChildCount = m_CategoryList[m_CurrentCategoryIndex].childCount;
		}

		// Clamp m_CurrentParticleIndex
		if(m_CurrentParticleIndex>=m_CurrentCategoryChildCount)
		{
			m_CurrentParticleIndex = 0;
		}
		else if(m_CurrentParticleIndex<0)
		{
			m_CurrentParticleIndex = m_CurrentCategoryChildCount-1;
		}

		// Play ParticleSystem
		if(m_CurrentParticleIndex!=m_CurrentParticleIndexOld || m_CurrentCategoryIndex!=m_CurrentCategoryIndexOld)
		{
			// Disable Old particle
			if(m_CurrentParticle!=null)
			{
				m_CurrentParticle.Stop();
				m_CurrentParticle.gameObject.SetActive(false);
			}
			
			index = 0;
			foreach(Transform child in m_CategoryList[m_CurrentCategoryIndex])
			{
				if(index==m_CurrentParticleIndex)
				{
					m_CurrentParticle = child.gameObject.GetComponent<ParticleSystem>();
					if(m_CurrentParticle!=null)
					{
						m_CurrentParticle.gameObject.SetActive(true);
						m_CurrentParticle.Play();
						
						m_CurrentParticleName = m_CurrentParticle.name;
					}
					break;
				}
				
				index++;
			}
		}
	}
	
	// Show Info window
	void AppNameWindow(int id)
	{
		//GUI.Label(new Rect(15, 25, 240, 20), "www.ge-team.com/pages");
		if(GUI.Button(new Rect(15, 25, 220, 20), "www.ge-team.com"))
		{
			Application.OpenURL("http://ge-team.com/pages/unity-3d/");
		}
		//GUI.Label(new Rect(15, 50, 240, 20), "geteamdev@gmail.com");
		if(GUI.Button(new Rect(15, 50, 220, 20), "geteamdev@gmail.com"))
		{
			Application.OpenURL("mailto:geteamdev@gmail.com");
		}
		//GUI.Label(new Rect(15, 50, 240, 20), "geteamdev@gmail.com");
		if(GUI.Button(new Rect(15, 75, 220, 20), "Tutorial"))
		{
			Application.OpenURL("http://youtu.be/TWpKPCGYEyI");
		}
	}
	
	// Show Scene buttons window
	void SceneWindow(int id)
	{
		if(m_CurrentParticleIndex>=0)
		{
			GUILayout.BeginHorizontal();

				// 2D Demo scene button
				if(Application.loadedLevelName=="2D_Demo")
					GUI.enabled=false;
				else
					GUI.enabled=true;
				if(GUI.Button(new Rect(12, 25, 125, 25), "2D Demo scene"))
				{
					Application.LoadLevel("2D_Demo");
				}

				// 3D Demo scene button
				if(Application.loadedLevelName=="3D_Demo")
					GUI.enabled=false;
				else
					GUI.enabled=true;
				if(GUI.Button(new Rect(155, 25, 125, 25), "3D Demo scene"))
				{
					Application.LoadLevel("3D_Demo");
				}
			GUILayout.EndHorizontal();
		}

	}
	
	// Show Information window
	void ParticleInformationWindow(int id)
	{
		if(m_CurrentParticleIndex>=0)
		{
			GUI.Label(new Rect(12, 25, 400, 20), "Up/Down: Change Type ("+(m_CurrentCategoryIndex+1)+" of "+m_CategoryList.Length+" "+m_CurrentCategoryName+")");
			GUI.Label(new Rect(12, 50, 400, 20), "Left/Right: Change Particle ("+(m_CurrentParticleIndex+1) + " of " + m_CurrentCategoryChildCount +" "+ m_CurrentParticleName+")");
		}
	}
		
	#endregion {Functions}
}
