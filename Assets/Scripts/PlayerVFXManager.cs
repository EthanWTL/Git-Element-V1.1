using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    public GameObject weapon;

    public GameObject MeleeCombo1_spark;
    public Transform MeleeCombo1_sparkTransform;

    public GameObject MeleeCombo1_1st_slashSmoke;
    public Transform MeleeCombo1_1st_slashSmokeTransForm;

    public GameObject MeleeCombo1_1st;
    public Transform MeleeCombo1_1stTransform;

    public GameObject MeleeCombo1_2nd;
    public Transform MeleeCombo1_2ndTransform;

    public GameObject MeleeCombo1_2nd_slashSmoke;
    public Transform MeleeCombo1_2nd_slashSmokeTransForm;

    public GameObject MeleeCombo1_3rd;
    public Transform MeleeCombo1_3rdTransform;

    public GameObject MeleeCombo1_3rd_slash;
    public Transform MeleeCombo1_3rd_slashTransform;

    public GameObject MeleeCombo1_3rd_slashSmoke;
    public Transform MeleeCombo1_3rd_slashSmokeTransForm;

    public GameObject MeleeCombo1_3rd_ground;
    public Transform MeleeCombo1_3rd_groundTransform;



    public void playMeleeCombo1_spark()
    {
        Vector3 localPosition = MeleeCombo1_sparkTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_spark = Instantiate(MeleeCombo1_spark, worldPosition, Quaternion.identity);
    }



    public void playMeleeCombo1_1st()
    {
        Vector3 localPosition = MeleeCombo1_1stTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_1st = Instantiate(MeleeCombo1_1st, worldPosition, MeleeCombo1_1stTransform.rotation);
    }

    public void playMeleeCombo1_1st_slashSmoke()
    {
        Vector3 localPosition = MeleeCombo1_1st_slashSmokeTransForm.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_1st_slashSmoke = Instantiate(MeleeCombo1_1st_slashSmoke, worldPosition, MeleeCombo1_1st_slashSmokeTransForm.rotation);
    }

    public void playMeleeCombo1_2nd()
    {
        Vector3 localPosition = MeleeCombo1_2ndTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_2nd = Instantiate(MeleeCombo1_2nd, worldPosition, MeleeCombo1_2ndTransform.rotation);
    }

    public void playMeleeCombo1_2nd_slashSmoke()
    {
        Vector3 localPosition = MeleeCombo1_2nd_slashSmokeTransForm.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_2nd_slashSmoke = Instantiate(MeleeCombo1_2nd_slashSmoke, worldPosition, MeleeCombo1_2nd_slashSmokeTransForm.rotation);
    }



    public void playMeleeCombo1_3rd()
    {
        Vector3 localPosition = MeleeCombo1_3rdTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_3rd = Instantiate(MeleeCombo1_3rd, worldPosition, Quaternion.identity);
    }

    public void playMeleeCombo1_3rd_slash()
    {
        Vector3 localPosition = MeleeCombo1_3rd_slashTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_3rd_slash = Instantiate(MeleeCombo1_3rd_slash, worldPosition, MeleeCombo1_3rd_slashTransform.rotation);
    }

    public void playMeleeCombo1_3rd_slashSmoke()
    {
        Vector3 localPosition = MeleeCombo1_3rd_slashSmokeTransForm.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_3rd_slash_smoke = Instantiate(MeleeCombo1_3rd_slashSmoke, worldPosition, MeleeCombo1_3rd_slashSmokeTransForm.rotation);
    }

    public void playMeleeCombo1_3rd_gournd()
    {
        Vector3 localPosition = MeleeCombo1_3rd_groundTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_3rd_ground = Instantiate(MeleeCombo1_3rd_ground, worldPosition, Quaternion.identity);
    }


}
