namespace App.Web.State;

public class AppNotificationState
{
    public string? Message { get; private set; }
    public string? Error { get; private set; }
    public event Action? OnChange;

    public void ShowMessage(string message)
    {
        Message = message; Error = null; Notify();
    }

    public void ShowError(string error)
    {
        Error = error; Message = null; Notify();
    }

    public void Clear()
    {
        Message = null; Error = null; Notify();
    }

    private void Notify() => OnChange?.Invoke();
}
