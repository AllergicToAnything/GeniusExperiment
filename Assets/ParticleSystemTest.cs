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

    public float smallParticleTranslateLerpTime;
    //public float smallParticleTranslateStartLerpAfter;
    public float smallParticleSpawnRate;
    float sPInitCD;
    Vector3 randomPlace;
    public Vector3 minRandomValue = new Vector3(-5,-5,-5);
    public Vector3 maxRandomValue = new Vector3(5,5,5);
    Transform targetPos;

    private void OnEnable()
    {
        sPInitCD = smallParticleSpawnRate;
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
        Vector3 xyz = new Vector3(1, 1, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            xyz += new Vector3(3f, 3f, 0);
            this.transform.localScale += new Vector3(.01f*xyz.x,.01f*xyz.y,0);
            if (smallParticleSpawnRate > 0)
            {
                smallParticleSpawnRate -= Time.deltaTime;
            }
            if (smallParticleSpawnRate <= 0)
            {
                randomPlace =
                    new Vector3(
                                    Random.Range(minRandomValue.x*xyz.x, maxRandomValue.x * xyz.x),
                                    Random.Range(minRandomValue.y * xyz.y, maxRandomValue.y * xyz.y),
                                    Random.Range(minRandomValue.z, maxRandomValue.z)
                                );
                randomPlace += innerParticlePrefab.transform.position;
                Spawn(smallParticlePrefab, randomPlace, smallParticlePrefab.transform.rotation);
                smallParticleSpawnRate = sPInitCD;
            }
        }
        
    }

    public void OuterParticleFunctions()
    {

    }

    public void SmallParticleFunctions()
    {
        Vector3 initPos = transform.localPosition;
        targetPos = GameObject.Find("ParticleEffect").transform;
        this.transform.localPosition = Vector3.Lerp(initPos,targetPos.localPosition,smallParticleTranslateLerpTime*Time.deltaTime);
        smallParticleTranslateLerpTime+=.15f;
       if(this.transform.localPosition == targetPos.localPosition)
        {
            Destroy(this.gameObject);
        }
        
    }

    public void Spawn(GameObject spawningObject, Vector3 parentTransform,Quaternion rotation)
    {
        GameObject clone = Instantiate(spawningObject,parentTransform,rotation);
    }
}
