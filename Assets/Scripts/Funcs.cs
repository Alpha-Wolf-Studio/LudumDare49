using UnityEngine;
public class Funcs : MonoBehaviour
{
    private static Funcs _funcs;
    public static Funcs Get() => _funcs;

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask playeplatformLayerMask;
    public bool LayerEqualPlayer(int layer)
    {
        return playerLayerMask == (playerLayerMask | (1 << layer));
    }
}