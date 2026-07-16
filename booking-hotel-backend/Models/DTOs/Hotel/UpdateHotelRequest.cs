public class UpdateHotelRequest
{
    public long? BrandId { get; set; }

    public long? OwnerId { get; set; }

    public long? CityId { get; set; }
    public string? City { get; set; } 
    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public byte? Star { get; set; }

    public TimeOnly? CheckinTime { get; set; }

    public TimeOnly? CheckoutTime { get; set; }

    public bool? Status { get; set; }

    public List<long>? AmenityIds { get; set; }
}