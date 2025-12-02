using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [SerializeField] private float recoilRecovery;
    private float xRecoil;
    private float yRecoil;
    [SerializeField] private float shotRecoil;
    [SerializeField] private float xBasePosition;
    [SerializeField] private float yBasePosition;
    [SerializeField] private float yRecoilMultiplier;
    [SerializeField] private float maxRecoil;
    [SerializeField] private float recoilRecoveryStopDuration;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (xRecoil > 0 && timer > recoilRecoveryStopDuration) xRecoil -= recoilRecovery * Time.deltaTime;
        if (xRecoil > maxRecoil)
        {
            xRecoil = maxRecoil;
            timer = 1;
        }
        if (yRecoil != 0 && timer > recoilRecoveryStopDuration)
        {
            if (yRecoil > 0) yRecoil -= recoilRecovery * Time.deltaTime;
            else yRecoil += recoilRecovery * Time.deltaTime;
        }
        if (transform.localPosition.y > -0.1f && transform.localPosition.y < 0.1f && timer > recoilRecoveryStopDuration) yRecoil = 0;
        this.transform.localPosition = new Vector2(xBasePosition - xRecoil, yBasePosition - yRecoil);
        timer += Time.deltaTime;
    }

    public void Recoil()
    {
        xRecoil += shotRecoil;
        yRecoil += Random.Range(-shotRecoil * yRecoilMultiplier, shotRecoil * yRecoilMultiplier);
        timer = 0;
    }
}
