using UnityEngine;
public class PhotoConfig
{
    public PhotoConfig(Texture2D photo, string description)
    {
        Photo = photo;
        Description = description;
    }

    public Texture2D Photo;
    public string Description;
}
