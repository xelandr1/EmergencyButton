namespace EmergencyButton.App.Service
{
    public interface IResumeSupportService
    {
        ResumeSupportStateChanged StateChanged { get; set; }
    }
    public delegate void ResumeSupportStateChanged(IResumeSupportService supportsResume, ResumeSupportState currentState, ResumeSupportState previousState);
    public enum ResumeSupportState
    {
        Closed,
        Paused,
        Stopped,
        Active
    }
}