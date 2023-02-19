using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding
{
    /// <summary>
    /// Sets the destination of an AI to the position of a specified object.
    /// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
    /// This component will then make the AI move towards the <see cref="target"/> set on this component.
    ///
    /// See: <see cref="Pathfinding.IAstarAI.destination"/>
    ///
    /// [Open online documentation to see images]
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
    public class AIDestinationSetter : VersionedMonoBehaviour
    {
        /// <summary>The object that the AI should move to</summary>
        public Transform target;
        private float runDistance = 7f;
        private float wanderDistanceX;
        private float wanderDistanceY;
        private Vector3 distance;
        private float idleTimer = 5f;
        private float currentIdleTimer;

        IAstarAI ai;

        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            distance = transform.position - target.position;
            if (distance.x < runDistance && distance.y < runDistance)
            {
                ai.maxSpeed = 7;
                ai.destination = distance;
            }
            else
            {
                if (currentIdleTimer >= 0)
                {
                    currentIdleTimer -= Time.deltaTime;
                }
                else
                {
                    ai.maxSpeed = 4f;
                    wanderDistanceX = Random.Range(-5, 5);
                    wanderDistanceY = Random.Range(-5, 5);
                    ai.destination = new Vector3((transform.position.x + wanderDistanceX), (transform.position.y + wanderDistanceY), transform.position.z);
                    currentIdleTimer = idleTimer;
                }               
            }
        }
    }
}