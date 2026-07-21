using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
        => int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    public static long GetHotelStaffId(this ClaimsPrincipal user)
        => long.Parse(user.FindFirst("HotelStaffId")!.Value);

    public static long GetHotelId(this ClaimsPrincipal user)
        => long.Parse(user.FindFirst("HotelId")!.Value);
}