using UnityEngine;

public class LayeredAudioPlayer : MonoBehaviour
{
    [SerializeField] private MultiSource[] sources;
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
            foreach (var s in source.sources)
            {
                s.Stop ();
            }
        }

        ActivateNext ();
    }

    public void ActivateNext ()
    {
        currentCombo++;
        if (currentCombo >= sources.Length)
            return;

        foreach (var s in sources[currentCombo].sources)
        {
            if (!s.isPlaying)
                s.Play ();
        }    
    }

    [System.Serializable]
    private struct MultiSource
    {
        public AudioSource[] sources;
    }
}