using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {

    private enum CURRENT_TERRAIN { GRASS, Water, DIRT, STONE, SAND, STONE_TUNNEL, WOOD };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance footsteps;

    private void Update()
    {
        DetermineTerrain();
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        // Originally set at 10.0f, but needs to be set to 0.25 for Robot scenario due to how the level is built.
        hit = Physics.RaycastAll(transform.position, Vector3.down, 0.25f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("GRASS"))
            {
                currentTerrain = CURRENT_TERRAIN.GRASS;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                currentTerrain = CURRENT_TERRAIN.Water;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("DIRT"))
            {
                currentTerrain = CURRENT_TERRAIN.DIRT;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("STONE"))
            {
                currentTerrain = CURRENT_TERRAIN.STONE;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("SAND"))
            {
                currentTerrain = CURRENT_TERRAIN.SAND;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("STONE TUNNEL"))
            {
                currentTerrain = CURRENT_TERRAIN.STONE_TUNNEL;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("WOOD"))
            {
                currentTerrain = CURRENT_TERRAIN.WOOD;
                break;
            }
        }
    }

    public void SelectAndPlayFootstep()
    {     
        switch (currentTerrain)
        {

            case CURRENT_TERRAIN.GRASS:
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.Water:
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.DIRT:
                PlayFootstep(2);
                break;

            case CURRENT_TERRAIN.STONE:
                PlayFootstep(3);
                break;

            case CURRENT_TERRAIN.SAND:
                PlayFootstep(4);
                break;

            case CURRENT_TERRAIN.STONE_TUNNEL:
                PlayFootstep(5);
                break;

            case CURRENT_TERRAIN.WOOD:
                PlayFootstep(6);
                break;

            default:
                PlayFootstep(0);
                break;
        }
    }

    private void PlayFootstep(int terrain)
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        footsteps.setParameterByName("Terrain", terrain);
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footsteps.start();
        footsteps.release();
    }
}