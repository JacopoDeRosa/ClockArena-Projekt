using System;

public interface IClickAudio
{
    public ClickTypes ClickType { get; }

    public event Action onAudio;
}
