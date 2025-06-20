namespace Application.Dtos.Auth
{
    public sealed record TokenResponseDto
    (
        string AccessToken,
        string RefreshToken
    );
}
