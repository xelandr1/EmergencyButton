namespace EmergencyButton.Core.Security
{
    public interface ISecurityRoutines
    {
        byte[] CreateMd5Hash(byte[] bytes);
    }
}