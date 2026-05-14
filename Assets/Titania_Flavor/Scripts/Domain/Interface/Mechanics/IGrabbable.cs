public interface IGrabbable
{
    bool CanGrab();

    void Grab(PlayerGrabber player);

    void Drop();
}