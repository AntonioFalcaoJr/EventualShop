namespace Domain.ValueObjects;

public record PictureUri
{
    private readonly Uri _value;

    public PictureUri(Uri pictureUri)
    {
        _value = pictureUri;
    }

    private PictureUri(string pictureUri)
    {
        pictureUri = pictureUri.Trim();
        ArgumentException.ThrowIfNullOrEmpty(pictureUri);

        _value = Uri.TryCreate(pictureUri, UriKind.Absolute, out var uri)
            ? uri
            : throw new ArgumentException("PictureUri must be a valid Uri");
    }

    public static explicit operator PictureUri(Uri pictureUrl) => new(pictureUrl);
    public static explicit operator PictureUri(string pictureUrl) => new(pictureUrl);
    public static implicit operator string(PictureUri pictureUri) => pictureUri._value.ToString();
    public static PictureUri Undefined => new(string.Empty);

    public override string ToString() => _value.ToString();
}