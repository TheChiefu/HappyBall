using UnityEngine;

/// <summary>
/// Simple music player that can do seqential or random music playback from clip array
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    [Tooltip("Time between one song ending and another starting")]
    [SerializeField] private float songDelay;
    [SerializeField] private bool isRandom = false;
    [SerializeField] private bool loopSong = false;

    private int currentSong = 0;
    private AudioSource _as = null;
    private float timer = 0;
    private float delayTimer = 0;
    private bool songPlaying = false;

    private void Awake()
    {
        //AudioSource Check
        if (_as == null) _as = GetComponent<AudioSource>();

        //Loop Check
        if (loopSong) _as.loop = true;
        else _as.loop = false;

        //Random Check
        if (isRandom) PlayRandomSong();
        else
        {
            if(currentSong != 0)
            {
                _as.clip = songs[currentSong];
                _as.Play();
            }
        }
    }


    /// <summary>
    /// Play random song out of array
    /// </summary>
    private void PlayRandomSong()
    {
        _as.clip = songs[Random.Range(0, songs.Length)];
        _as.Play();
    }

    /// <summary>
    /// Play next song in array from index
    /// </summary>
    private void PlaySequentialSong()
    {
        currentSong++;
        if (currentSong >= songs.Length) currentSong = 0;
        _as.clip = songs[currentSong];
        _as.Play();
    }

    /// <summary>
    /// Time management
    /// </summary>
    private void Update()
    {
        if(currentSong != 0)
        {
            timer += Time.deltaTime;

            //When song is over
            if (timer > _as.clip.length)
            {

                //Song has ended parameters
                timer = 0;
                songPlaying = false;

                //If songs are to be delayed, delay them
                if (songDelay > 0 && !songPlaying)
                {
                    delayTimer += Time.deltaTime;
                    if (delayTimer >= songDelay) songPlaying = true;
                }

                //Otherwise don't
                else
                {
                    if (!loopSong)
                    {
                        if (isRandom) PlayRandomSong();
                        else PlaySequentialSong();
                    }
                }
            }
        }
    }
}
