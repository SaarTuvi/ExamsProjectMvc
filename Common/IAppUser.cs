namespace Common
{
    public interface IAppUser
    {
        int ID { get; set; }
        string LoginName { get; set; }
        string Name { get; set; }
        string Password { get; set; }
    }
}