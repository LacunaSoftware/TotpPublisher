using Microsoft.Extensions.Options;
using OtpNet;
using System.Security.Cryptography;

namespace TotpPublisher {
	
	public class Program {

		public static void Main(string[] args) {

			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddRazorPages();
			builder.Services.Configure<GeneralConfig>(builder.Configuration.GetSection("General"));
			builder.Services.Configure<TotpConfig>(builder.Configuration.GetSection("Totp"));
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

			var generalConfig = app.Services.GetRequiredService<IOptions<GeneralConfig>>().Value;
			var totpConfig = app.Services.GetRequiredService<IOptions<TotpConfig>>().Value;

			// Check that seed is configured, if not abort
			if (string.IsNullOrEmpty(totpConfig.Seed)) {
				var randomSeed = new byte[16]; // 128 bits
				using var rng = RandomNumberGenerator.Create();
				rng.GetBytes(randomSeed);
				throw new Exception($"The seed must be configured with the environment variable Totp__Seed -- Hint: need a random seed? Here's one: {Base32Encoding.ToString(randomSeed).Replace("=", "")}");
			}
			try {
				Base32Encoding.ToBytes(totpConfig.Seed);
			} catch (Exception ex) {
				throw new Exception($"Invalid seed: \"{totpConfig.Seed}\"", ex);
			}
			
			// Log configuration
			var logger = app.Services.GetRequiredService<ILogger<Program>>();
			logger.LogInformation($"Seed: {totpConfig.Seed}\nStep: {totpConfig.Step}\nMode: {totpConfig.Mode}\nSize: {totpConfig.Size}");
			if (generalConfig.Debug) {
				logger.LogWarning("Debug mode is ON! Seed will be visible!");
			}

			// Run
			app.Run();
		}
	}
}
