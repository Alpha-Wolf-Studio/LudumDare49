using UnityEngine;
public class Platform : MonoBehaviour
{

    [SerializeField] protected PlatformBase basePlatform;
    [SerializeField] protected int scoreOnCorruption = 10;
    protected bool firstCollision = false;
    public void DestroyBase() 
    {
        basePlatform.DestroyPlatform();
    }
}
