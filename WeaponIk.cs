using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float weight = 1.0f; 
}
public class WeaponIk : MonoBehaviour
{

    public Transform targetTransform;
    public Transform aimTransform;
    public Transform bone;

    public HumanBone[] humanBones;
    Transform[] boneTransforms;

    public int iterations = 10;
    
    [Range(0 , 1)]
    public float weight = 1.0f;

    

    // Start is called before the first frame update
    void Start()
    {

        Animator animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for (int i = 0; i < boneTransforms.Length; i ++) { 
        }

        
    }

     void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position;
        for(int i = 0 ; i < iterations; i++)
        {
            AimAtTarget(bone, targetPosition , weight);
        }
        
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition , float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }
}