/*============================================================================== 
 Copyright (c) 2016-2017 PTC Inc. All Rights Reserved.
 
 Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

/// <summary>
/// This class implements the IVirtualButtonEventHandler interface and
/// contains the logic to start animations depending on what 
/// virtual button has been pressed.
/// </summary>
public class VirtualButtonEventHandler : MonoBehaviour,
                                         IVirtualButtonEventHandler
{
    #region PUBLIC_MEMBERS
    public Material m_VirtualButtonMaterial;
    public Material m_VirtualButtonMaterialPressed;
    public int gameplay1_idx;
    public int gameplay2_idx;
    #endregion // PUBLIC_MEMBERS

    #region PRIVATE_MEMBERS
    VirtualButtonBehaviour[] virtualBtnBehaviours;
    #endregion // PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        // Register with the virtual buttons TrackableBehaviour
        virtualBtnBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualBtnBehaviours.Length; ++i)
        {
            virtualBtnBehaviours[i].RegisterEventHandler(this);
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonPressed: " + vb.VirtualButtonName);

        SetVirtualButtonMaterial(m_VirtualButtonMaterialPressed);

        // Add the material corresponding to this virtual button
        // to the active material list:

    }

    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);

        SetVirtualButtonMaterial(m_VirtualButtonMaterial);

        switch (vb.VirtualButtonName)
        {
            case "gameplay1":
                // Load new scene
                SceneManager.LoadScene(gameplay1_idx);                
                break;
            case "gameplay2":
                // Load new scene
                SceneManager.LoadScene(gameplay2_idx);
                break;

            case "exitgame":
                // exit the game
                Application.Quit();
                break;
        }
    }

    #endregion //PUBLIC_METHODS

    #region PRIVATE_METHODS
    void SetVirtualButtonMaterial(Material material)
    {
        // Set the Virtual Button material
        for (int i = 0; i < virtualBtnBehaviours.Length; ++i)
        {
            if (material != null)
            {
                virtualBtnBehaviours[i].GetComponent<MeshRenderer>().sharedMaterial = material;
            }
        }
    }
    #endregion // PRIVATE METHODS
}
