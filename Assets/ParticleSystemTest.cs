using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleSystemTest : MonoBehaviour
{
    public ParticleType particleType;

    public GameObject innerParticlePrefab;
    public GameObject outerParticlePrefab;
    public GameObject smallParticlePrefab;


    [Header("Random Spawn Small Particle On Inner Particle")]
    public Vector3 minRandomValue = new Vector3(-5,-5,-5);
    public Vector3 maxRandomValue = new Vector3(5,5,5);
    public float smallParticleSpawnRate;
    Vector3 randomPlace;

    [Header("Travel Speed for Small Particle")]
    public Vector2 scaleUpSpeedMultiplierSP = new Vector2(.5f,.5f);
    public Vector2 slowInSlowOutTimeSP = new Vector2(.5f, .5f);
    public float smallParticleTranslateLerpTime;
    float sPInitCD;

    [Header("Scale for Small Particle")]
    public Vector3 maxLocalScaleSP = new Vector3(.2f,.2f,.2f);
    public Vector3 minLocalScaleSP = new Vector3(.05f,.05f,.05f);
    public Vector2 slowInSlowOutSpeedSP = new Vector2(.5f, .5f);
    public Vector2 slowInSlowOutTimeScaleSP = new Vector2(.3f,.2f);
    public float lerpScaleSpeedSP = 1f;

    [Header("Inner Particle")]
    public Vector3 maxScaleSize = new Vector3(5f,5f,0f);
    public Vector3 minScaleSize = new Vector3(-5f, -5f, 0f);
    public Vector3 scaleUpSpeedMultiplier = new Vector3(.03f, .03f, 0);
    public float scaleBackSpeed = .1f;

    Transform targetPos;
    Vector3 oriScale; // this game object original local scale

    private void OnEnable()
    {
        sPInitCD = smallParticleSpawnRate;
        oriScale = this.transform.localScale;
    }
    private void Update()
    {
        ParticleSwitches();
    }

    public void ParticleSwitches()
    {
        switch (particleType)
        {
            case ParticleType.InnerParticle:
                InnerParticleFunctions();
                break;

            case ParticleType.OuterParticle:
                OuterParticleFunctions();
                break;

            case ParticleType.SmallParticle:
                SmallParticleFunctions();
                break;
        }
    }

    public void InnerParticleFunctions()
    {
        bool startLerpBack = false;
        Vector3 xyz = new Vector3(1, 1, 1);
        Vector3 thisScale = transform.localScale;
        if (!(thisScale.x >= maxScaleSize.x&& thisScale.y >= maxScaleSize.y&& thisScale.z >= maxScaleSize.z) &&
            !(thisScale.x <= minScaleSize.x && thisScale.y <= minScaleSize.y && thisScale.z <= minScaleSize.z)) {
            if (Input.GetKey(KeyCode.Space))
            {
                xyz += new Vector3(scaleUpSpeedMultiplier.x, scaleUpSpeedMultiplier.y, scaleUpSpeedMultiplier.z);
                this.transform.localScale += new Vector3(.01f * xyz.x, .01f * xyz.y, .01f * xyz.z);

                if (smallParticleSpawnRate > 0)
                {
                    smallParticleSpawnRate -= Time.deltaTime;
                }
                if (smallParticleSpawnRate <= 0)
                {
                    randomPlace =
                        new Vector3(
                                        Random.Range(minRandomValue.x * xyz.x*10, maxRandomValue.x * xyz.x*10),
                                        Random.Range(minRandomValue.y * xyz.y * 10, maxRandomValue.y * xyz.y * 10),
                                        Random.Range(minRandomValue.z, maxRandomValue.z)
                                    );
                    randomPlace += innerParticlePrefab.transform.position;
                    Spawn(smallParticlePrefab, randomPlace, smallParticlePrefab.transform.rotation);
                    smallParticleSpawnRate = sPInitCD;
                }
            }
        }
        if (transform.localScale != oriScale && !Input.GetKey(KeyCode.Space))
        {
            startLerpBack = true;
        }
        if (startLerpBack)
        {
            float sBS = scaleBackSpeed;
            if (transform.localScale == oriScale) { startLerpBack = false; }
            scaleBackSpeed = .5f;
            Invoke("RevertLerp", .5f);
            scaleBackSpeed = sBS;
            
        }
        
    }

    void RevertLerp()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, oriScale, scaleBackSpeed * Time.deltaTime);
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void OuterParticleFunctions()
    {

    }

    public void SmallParticleFunctions()
    {
        transform.localScale = maxLocalScaleSP;
        bool slowIn = true;
        bool slowOut = false;
        float oriSpeed = smallParticleTranslateLerpTime;
        float oriScaleSpeed = lerpScaleSpeedSP;
        Vector3 initPos = transform.localPosition;
        targetPos = GameObject.Find("ParticleEffect").transform;
        if (slowIn)
        {
            smallParticleTranslateLerpTime *= scaleUpSpeedMultiplierSP.x;
            slowIn = false;
            Wait(slowInSlowOutTimeSP.x);
            slowOut = true;
        }
        if (slowOut)
        {
            smallParticleTranslateLerpTime *= scaleUpSpeedMultiplierSP.y;
            slowOut = false;
            Wait(slowInSlowOutTimeSP.y);
        }
        if (!slowIn || !slowOut)
        {
            smallParticleTranslateLerpTime = oriSpeed;
        }
        this.transform.localPosition = Vector3.Lerp(initPos, targetPos.localPosition, smallParticleTranslateLerpTime * Time.deltaTime);
        smallParticleTranslateLerpTime += .15f;
        Invoke("DestroyThis", .8f);
        if (this.transform.localPosition == targetPos.localPosition)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.Lerp(maxLocalScaleSP, minLocalScaleSP, lerpScaleSpeedSP * Time.deltaTime);
            bool sslowIn = true;
            bool sslowOut = false;
            if (sslowIn)
            {
                lerpScaleSpeedSP *= slowInSlowOutSpeedSP.x;
                sslowIn = false;
                Wait(slowInSlowOutTimeScaleSP.x);
                sslowOut = true;
            }
            if (sslowOut)
            {
                lerpScaleSpeedSP *= slowInSlowOutSpeedSP.y;
                sslowOut = false;
                Wait(slowInSlowOutTimeScaleSP.y);
            }
            if (!sslowIn || !sslowOut)
            {
                lerpScaleSpeedSP = oriScaleSpeed;
            }
        
           
        }
    
    } 
    

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void Spawn(GameObject spawningObject, Vector3 parentTransform,Quaternion rotation)
    {
        GameObject clone = Instantiate(spawningObject,parentTransform,rotation);
    }
}
