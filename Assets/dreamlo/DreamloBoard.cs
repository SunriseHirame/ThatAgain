using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class DreamloBoard : ScriptableObject
{
    [SerializeField]
    internal string privateKey;
    
    [SerializeField]
    internal string publicKey;

    public UnityEvent Loaded;
    
    public void Load ()
    {
        DreamloLeaderBoard.Instance.LoadScores (this, () => Loaded.Invoke ());
    }
}
