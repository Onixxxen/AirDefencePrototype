public struct AddBaseHpResultDTO
{
    public bool Success;
    public string Message;

    public AddBaseHpResultDTO(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public AddBaseHpResultDTO(bool success)
    {
        Success = success;
        Message = null;
    }
}