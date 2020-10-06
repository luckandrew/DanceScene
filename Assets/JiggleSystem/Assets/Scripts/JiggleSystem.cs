using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleSystem : MonoBehaviour {

    Material mat;

    const float DefaultInitialJiggleStrength = 5f; 
    float CurrentJiggleStrength;

    const float DefaultDecreaseRateOfJiggleStrength = 3.5f; 
    float DecreaseRateOfJiggleStrength;

    const float DefaultJiggleSpeed = 12.5f; 
    const float DefaultRadiusOfInfluence = .25f;
    static Vector3 DefaultDirectionOfJiggle { get { return Vector3.right; } }

    void Update()
    {
        if (mat != null)
        {
            CurrentJiggleStrength =
                Mathf.Clamp(
                    CurrentJiggleStrength - (DecreaseRateOfJiggleStrength * Time.deltaTime
                    ), 0, 10);
            mat.SetFloat("_current_jiggle_strength", CurrentJiggleStrength);
            if (CurrentJiggleStrength == 0f)
            {
                Destroy(this);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    public static void JiggleMaterial(
        GameObject GameObjectOfTheMaterial,
        Material material,
        Vector3 CenterPointOfJiggle,
        float JiggleSpeed,
        float InitialJiggleStrength,
        float DecreaseRateOfJiggleStrength,
        Vector3 DirectionOfJiggle,
        float RadiusOfInfluence)
    {
        if (material == null || GameObjectOfTheMaterial == null)
        {
            Debug.Log("The Material or GameObject that was sent to " +
                "'JiggleMaterial' is null !");
            return;
        }

        JiggleSystem js = GameObjectOfTheMaterial.AddComponent<JiggleSystem>();

        js.mat = material;

        js.SetPointOfJiggle(CenterPointOfJiggle);
        js.SetJiggleSpeed(JiggleSpeed == 0f ? DefaultJiggleSpeed : JiggleSpeed);
        js.SetInitialJiggleStrength(InitialJiggleStrength == 0f ? DefaultInitialJiggleStrength : InitialJiggleStrength);
        js.SetDecreaseRateOfJiggle(DecreaseRateOfJiggleStrength == 0f ? DefaultDecreaseRateOfJiggleStrength : DecreaseRateOfJiggleStrength);
        js.SetDirectionOfJiggle(DirectionOfJiggle == Vector3.zero ? DefaultDirectionOfJiggle : DirectionOfJiggle);
        js.SetRadiusOfInfluence(RadiusOfInfluence == 0f ? DefaultRadiusOfInfluence : RadiusOfInfluence);
    }

    public static void JiggleMaterial(
    Renderer renderer,
    Vector3 CenterPointOfJiggle,
    float JiggleSpeed,
    float InitialJiggleStrength,
    float DecreaseRateOfJiggleStrength,
    Vector3 DirectionOfJiggle,
    float RadiusOfInfluence)
    {
        if (renderer != null)
            if (renderer.material != null && renderer.gameObject != null)
            {
                foreach (Material mater in renderer.materials)
                {
                    JiggleMaterial(
                    renderer.gameObject, mater, CenterPointOfJiggle,
                    JiggleSpeed, InitialJiggleStrength, DecreaseRateOfJiggleStrength,
                    DirectionOfJiggle, RadiusOfInfluence);
                }
            }
            else
            {
                Debug.Log("The material or game object of the renderer that " +
                    "was sent to 'JiggleMaterial' method is null !");
            }
        else
        {
            Debug.Log("The renderer that was sent to 'JiggleMaterial' method is null !");
        }
    }

    public static void JiggleMaterials(
        float RadiusOfOverlapSphere,
        Vector3 CenterPointOfJiggle,
    float JiggleSpeed,
    float InitialJiggleStrength,
    float DecreaseRateOfJiggleStrength,
    Vector3 DirectionOfJiggle,
    float RadiusOfInfluence)
    {
        Collider[] colls =
            Physics.OverlapSphere(CenterPointOfJiggle, RadiusOfOverlapSphere);

        foreach (Collider col in colls)
        {
            if (col.gameObject != null)
            {
                Renderer ren = col.gameObject.GetComponent<Renderer>();
                if (ren != null)
                {
                    JiggleMaterial
                        (
                        ren,
                        CenterPointOfJiggle,
                        JiggleSpeed,
                        InitialJiggleStrength,
                        DecreaseRateOfJiggleStrength,
                        DirectionOfJiggle,
                        RadiusOfInfluence
                        );
                }
            }
        }
    }


    public static void JiggleMaterial(
        GameObject GameObjectOfTheMaterial,
        Material material,
        Vector3 CenterPointOfJiggle)
    {
        if (material == null || GameObjectOfTheMaterial == null)
        {
            Debug.Log("The material or game object that was sent to " +
                "the method 'JiggleMaterial' is null !");
            return;
        }

        JiggleSystem js = GameObjectOfTheMaterial.AddComponent<JiggleSystem>();

        js.mat = material;

        js.SetPointOfJiggle(CenterPointOfJiggle);
        js.SetJiggleSpeed(DefaultJiggleSpeed);
        js.SetInitialJiggleStrength(DefaultInitialJiggleStrength);
        js.SetDecreaseRateOfJiggle(DefaultDecreaseRateOfJiggleStrength);
        js.SetDirectionOfJiggle(DefaultDirectionOfJiggle);
    }

    public static void JiggleMaterial(
    Renderer renderer,
    Vector3 CenterPointOfJiggle)
    {
        if (renderer != null)
            if (renderer.material != null && renderer.gameObject != null)
            {
                foreach (Material mater in renderer.materials)
                {
                    JiggleMaterial(
                       renderer.gameObject, mater, CenterPointOfJiggle);
                }
            }
            else
            {
                Debug.Log("The material or game object of the renderer that " +
                    "was sent to 'JiggleMaterial' method is null !");
            }
        else
        {
            Debug.Log("The renderer that was sent to 'JiggleMaterial' method is null !");
        }
    }

    public static void JiggleMaterials(
        float RadiusOfOverlapSphere,
        Vector3 CenterPointOfJiggle)
    {
        Collider[] colls =
            Physics.OverlapSphere(CenterPointOfJiggle, RadiusOfOverlapSphere);

        foreach (Collider col in colls)
        {
            if (col.gameObject != null)
            {
                Renderer ren = col.gameObject.GetComponent<Renderer>();
                if (ren != null)
                {
                    JiggleMaterial
                        (ren, CenterPointOfJiggle);
                }
            }
        }
    }


    void SetPointOfJiggle(Vector3 PosOfJiggle)
    {
        mat.SetVector("_center_of_jiggle", PosOfJiggle);
    }
    void SetJiggleSpeed(float JiggleSpeed)
    {
        mat.SetFloat("_jiggle_speed", JiggleSpeed);
    }
    void SetInitialJiggleStrength(float InitialJiggleStrength)
    {
        CurrentJiggleStrength = InitialJiggleStrength;
    }
    void SetDecreaseRateOfJiggle(float DecreaseRate)
    {
        DecreaseRateOfJiggleStrength = DecreaseRate;
    }
    void SetDirectionOfJiggle(Vector3 Direction)
    {
        mat.SetVector("_direction_of_jiggle", Direction);
    }
    void SetRadiusOfInfluence(float radius)
    {
        mat.SetFloat("_radius_of_influence", radius);
    }

}
