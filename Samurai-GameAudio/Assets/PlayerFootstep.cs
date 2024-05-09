using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    private enum CURRENT_TERRAIN { GRASS, Water, DIRT, STONE, SAND, STONE_TUNNEL, WOOD };
    private enum MOVE_STATE { RUN, WALK, CROUCH };

    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;
    private MOVE_STATE moveState;

    private FMOD.Studio.EventInstance footsteps;

    private void Update()
    {
        UpdateMoveState();
        DetermineTerrain();
        //SelectAndPlayFootstep();  // Adjust to trigger footstep sound with both terrain and move state
    }

    private void UpdateMoveState()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveState = MOVE_STATE.WALK;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            moveState = MOVE_STATE.CROUCH;
        }
        else
        {
            moveState = MOVE_STATE.RUN;
        }
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, Vector3.down, 10f);

        foreach (RaycastHit rayhit in hit)
        {
            switch (LayerMask.LayerToName(rayhit.transform.gameObject.layer))
            {
                case "GRASS": currentTerrain = CURRENT_TERRAIN.GRASS; break;
                case "Water": currentTerrain = CURRENT_TERRAIN.Water; break;
                case "DIRT": currentTerrain = CURRENT_TERRAIN.DIRT; break;
                case "STONE": currentTerrain = CURRENT_TERRAIN.STONE; break;
                case "SAND": currentTerrain = CURRENT_TERRAIN.SAND; break;
                case "STONE_TUNNEL": currentTerrain = CURRENT_TERRAIN.STONE_TUNNEL; break;
                case "WOOD": currentTerrain = CURRENT_TERRAIN.WOOD; break;
            }
        }
    }

    private void SelectAndPlayFootstep()
    {
        int terrainIndex = (int)currentTerrain;
        int moveStateIndex = (int)moveState;
        PlayFootstep(terrainIndex, moveStateIndex);
    }

    private void PlayFootstep(int terrain, int movestate)
    {
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        footsteps.setParameterByName("MoveState", movestate);
        footsteps.setParameterByName("Terrain", terrain);
        footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footsteps.start();
        footsteps.release();
    }
}