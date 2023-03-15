using Microsoft.Extensions.Options;
using OtpNet;

namespace TotpPublisher {
	
	public class Program {

		public static void Main(string[] args) {

			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.Configure<TotpConfiguration>(builder.Configuration.GetSection("Totp"));
			var app = builder.Build();

			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Error");
				//app.UseHsts();
			}
			//app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.MapRazorPages();

			// Check that seed is configured, if not abort
			var config = app.Services.GetRequiredService<IOptions<TotpConfiguration>>().Value;
			if (string.IsNullOrEmpty(config.Seed)) {
				throw new Exception("The seed must be configured with the environment variable Totp__Seed");
			}
			try {
				Base32Encoding.ToBytes(config.Seed);
			} catch (Exception ex) {
				throw new Exception($"Invalid seed: \"{config.Seed}\"", ex);
			}
			
			// Log configuration
			var logger = app.Services.GetRequiredService<ILogger<Program>>();
			logger.LogInformation($"Seed: {config.Seed}\nStep: {config.Step}\nMode: {config.Mode}\nSize: {config.Size}");
			if (config.Debug) {
				logger.LogWarning("Debug mode is ON! Seed will be visible!");
			}

			// Run
			app.Run();
		}
	}
}
