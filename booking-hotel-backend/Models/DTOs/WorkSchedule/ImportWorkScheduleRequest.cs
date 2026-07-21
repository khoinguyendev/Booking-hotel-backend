public class ImportWorkScheduleRequest
{
    public string EmployeeCode { get; set; } = string.Empty;

    public string ShiftName { get; set; } = string.Empty;

    public DateOnly WorkDate { get; set; }

    public bool IsDayOff { get; set; } = false;
}