using UnityEngine;

public class LayeredAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;
    private int currentCombo = -1;

    private void Awake ()
    {
        ActivateNext ();
    }

    public void Reset ()
    {
        currentCombo = -1;
        foreach (var source in sources)
        {
            source.Stop ();
        }
        ActivateNext ();
    }

    public void ActivateNext ()
    {
        currentCombo++;
        if (currentCombo >= sources.Length)
            return;
        if (!sources[currentCombo].isPlaying)
            sources[currentCombo].Play ();
    }
}
