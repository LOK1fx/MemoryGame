using UnityEngine;
public class PhotoConfig
{
    public PhotoConfig(ETypePhoto typePhoto, int idPhoto, Texture2D photo, string description, string hiddenText)
    {
        TypePhoto = typePhoto;
        IdPhoto = idPhoto;
        Photo = photo;
        Description = description;
        HiddenText = hiddenText;
    }

    public ETypePhoto TypePhoto;
    public int IdPhoto;
    public Texture2D Photo;
    public string Description;
    public string HiddenText;
}
