using UnityEngine;
public class Funcs : MonoBehaviour
{
    private static Funcs _funcs;
    public static Funcs Get() => _funcs;

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask platformLayerMask;

    private void Awake()
    {
        _funcs = this;
    }
    public bool LayerEqualPlayer(int layer)
    {
        return playerLayerMask == (playerLayerMask | (1 << layer));
    }
}