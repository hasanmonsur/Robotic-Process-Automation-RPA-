using Microsoft.Playwright;
using RpaWebApi.Models;

namespace RpaWebApi.Services
{
    public class RpaService
    {
        public async Task<byte[]> LoginAndDownloadFileAsync(LoginRequest request)
        {
            // Initialize Playwright
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            try
            {
                // Navigate to the login page
                // Navigate to the login page
                await page.GotoAsync(request.WebsiteUrl);

                // Perform login (adjust selectors based on the target site)
                await page.FillAsync("input[name='Username']", request.Username); // Example selector
                await page.FillAsync("input[name='Password']", request.Password); // Example selector
                await page.ClickAsync("button[type='submit']"); // Example login button



                // Wait for navigation after login (adjust based on site behavior)
                await page.WaitForURLAsync("**/**"); // Example: wait for dashboard URL

                // Navigate to the download page
                await page.GotoAsync(request.DownloadUrl);

                // Trigger file download
                var download = await page.RunAndWaitForDownloadAsync(async () =>
                {
                    //await page.ClickAsync("a[href*='.pdf']"); // Example: click a PDF download link
                    await page.ClickAsync("button[type='submit']"); // Example login button
                });

                // Get the downloaded file bytes
                var filePath = await download.PathAsync();
                var fileBytes = await File.ReadAllBytesAsync(filePath);

                // Clean up
                await download.DeleteAsync();
                return fileBytes;
            }
            catch (Exception ex)
            {
                throw new Exception($"RPA task failed: {ex.Message}");
            }
        }
    }
}
