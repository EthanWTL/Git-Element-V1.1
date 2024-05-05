using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    public GameObject weapon;

    public GameObject MeleeCombo1_1st;
    public Transform MeleeCombo1_1stTransform;

    public GameObject MeleeCombo1_2nd;
    public Transform MeleeCombo1_2ndTransform;

    public GameObject MeleeCombo1_3rd;
    public Transform MeleeCombo1_3rdTransform;

    public GameObject MeleeCombo1_3rd_slash;
    public Transform MeleeCombo1_3rd_slashTransform;



    public void playMeleeCombo1_1st()
    {
        Vector3 localPosition = MeleeCombo1_1stTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_1st = Instantiate(MeleeCombo1_1st, worldPosition, MeleeCombo1_1stTransform.rotation);
    }

    public void playMeleeCombo1_2nd()
    {
        Vector3 localPosition = MeleeCombo1_2ndTransform.localPosition;
        Vector3 worldPosition = weapon.transform.TransformPoint(localPosition);
        GameObject combo1_2nd = Instantiate(MeleeCombo1_2nd, worldPosition, MeleeCombo1_2ndTransform.rotation);
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
        GameObject combo1_3rd = Instantiate(MeleeCombo1_3rd_slash, worldPosition, MeleeCombo1_3rd_slashTransform.rotation);
    }


}
