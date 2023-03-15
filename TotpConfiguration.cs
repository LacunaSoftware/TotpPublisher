using OtpNet;

namespace TotpPublisher {
	
	public class TotpConfiguration {

		public string Seed { get; set; }

		public OtpHashMode Mode { get; set; } = OtpHashMode.Sha1;

		public int Size { get; set; } = 6;

		public int Step { get; set; } = 30;

		public bool Debug { get; set; }
	}
}
