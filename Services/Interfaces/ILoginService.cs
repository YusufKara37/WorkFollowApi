public interface ILoginService
{
    Task<bool> LoginAsync(LoginDto loginDto);
}