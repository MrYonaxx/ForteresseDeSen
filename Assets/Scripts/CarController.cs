/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoiceActing
{
    public class CarController: MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */
        [SerializeField]
        Rigidbody rigidbody;
        [SerializeField]
        Camera cam;

        [SerializeField]
        float speedAcceleration = 10;

        [SerializeField]
        float maxAcceleration = 100;

        float currentSpeed = 0;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public void Update()
        {
            if(Input.GetButton("Fire1"))
            {
                Accelerate();
            }
            else
            {
                Deccelerate();
            }
            rigidbody.velocity = cam.transform.forward * currentSpeed;
        }

        public void Accelerate()
        {
            currentSpeed += speedAcceleration;
            currentSpeed = Mathf.Max(0, maxAcceleration);
        }

        public void Deccelerate()
        {
            currentSpeed -= speedAcceleration;
            currentSpeed = Mathf.Max(0, currentSpeed);
        }

        #endregion

    } 

} // #PROJECTNAME# namespace