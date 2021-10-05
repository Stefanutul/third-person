 using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public CharacterAiming characterAiming;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator rigController;


    public Vector2[] recoilPatttern;
    float verticalRecoil;
    float horizontalRecoil;
    public float duration;

    float time;
    int index;
    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }
    public void Reset()
    {
        index = 0;
    }

    int NextIndex(int index)
    {
        return (index + 1) % recoilPatttern.Length;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;

        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPatttern[index].x;
        verticalRecoil = recoilPatttern[index].y;

        index = NextIndex(index);

        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }
   

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {

            characterAiming.yAxis.Value -= ((verticalRecoil/10) * Time.deltaTime) / duration;
            characterAiming.xAxis.Value -= ((horizontalRecoil / 10) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }

    }
}

