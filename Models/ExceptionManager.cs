namespace ReisProduction.Wincore.Models;
/// <summary>
/// Exception management class for logging, emailing, and handling exceptions.
/// </summary>
public static class ExceptionManager
{
    /// <summary>
    /// Log directory path. Default is Path.Combine(AppPath, "Logs").
    /// </summary>
    public static string LogPath { get; set; } = Path.Combine(AppInfo.BaseDirectory, "Logs");
    /// <summary>
    /// Mail address to send error reports to.
    /// </summary>
    public static string MailTo { get; set; } = "some@example.com";
    #region Public Methods
    /// <summary>
    /// Handle an exception by logging it, prompting the user to send an email, and optionally exiting the application.
    /// </summary>
    public static async Task HandleException(Exception ex, bool createLog = true, bool sendMail = true, bool environmentExit = true)
    {
        if (createLog)
            await LogManager.CreateLogAsync(ex, LogPath);
        if (sendMail && PromptUserToSendEmail() is not DialogResult.OK) return;
        string exBody = ComposeExceptionBody(ex);
        if (!TrySendMailViaDefaultClient(exBody))
            await HandleChromeMailFallbackAsync(exBody);
        if (environmentExit)
            await EnvironmentExit();
    }
    #endregion
    #region Private Helpers
    private static DialogResult PromptUserToSendEmail() =>
        MessageBoxShow(
            "Would you like to notify us via E-Mail?\n\nPlease do not send too much. We will try to fix the error.",
            "internal-EncounteredAnError",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Information
        );
    private static string ComposeExceptionBody(Exception ex)
    {
        string inner = ex.InnerException != null
            ? $"\n--- Inner Exception ---\nType: {ex.InnerException.GetType().FullName}\nMessage: {ex.InnerException.Message}\nStackTrace: {ex.InnerException.StackTrace}\n"
            : string.Empty;
        return $@"
Error Details ({DateTime.Now})
-------------------------------
Type: {ex.GetType().FullName}
Message: {ex.Message}
StackTrace: {ex.StackTrace}
{inner}
Before I encountered the error I was doing the following:
This may be the reason for the error:";
    }
    private static bool TrySendMailViaDefaultClient(string body)
    {
        try
        {
            string mailto = $"mailto:{Uri.EscapeDataString(MailTo)}" +
                            $"?subject={Uri.EscapeDataString($"I encountered an error in {AppInfo.Name}.")}" +
                            $"&body={Uri.EscapeDataString(body)}";
            ProcessManager.Start(mailto, useShell: true);
            return true;
        }
        catch (Exception ex)
        {
            ShowError($"There was an error while sending the error via Email:\n{ex.Message}", "internal-EncounteredAnError");
            return false;
        }
    }
    private static async Task HandleChromeMailFallbackAsync(string body)
    {
        string pathChrome = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google", "Chrome", "Application", "chrome.exe");
        if (!File.Exists(pathChrome) || MessageBoxShow(
            "I can help you send it to you\n\nAre you want?",
            "Can't send email?",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information) is not DialogResult.Yes)
            return;
        string tempFile = Path.Combine(Path.GetTempPath(), TempFileName);
        if (!File.Exists(tempFile))
        {
            body = "If you are using Google Chrome as the MailTo application and your e-mail address is from the Gmail domain:\r\n" +
                   "- Go to Settings -> Privacy and Security -> Site settings -> Additional permissions -> Handlers Enabled\r\n" +
                   "- Go to Gmail and allow this site to send Handlers.\r\n" +
                   "- Links: mail.google.com/mail/u/0/#inbox, chrome://settings/handlers\n\n" + body;
        }
        else
            body = "\nA different time:\n" + body;
        ProcessManager.Start(pathChrome, "mail.google.com/mail/u/0/#inbox");
        await File.AppendAllTextAsync(tempFile, body);
        ProcessManager.Start("notepad", tempFile);
    }
    internal static void ShowError(string message, string caption = "internal-EncounteredAnError") =>
        MessageBoxShow(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    #endregion
}