using POEProg.Models;

namespace PROG6212.Data
{
    public static class ClaimDataStore
    {
        // 🔹 In-memory data store for lecturer claims
        private static List<Claim> _claims = new List<Claim>
        {
            new Claim
            {
                Id = 1,
                LecturerName = "James Lious",
                HoursWorked = 100,
                HourlyRate = 250,
                Total = 25000,
                DateSubmitted = DateTime.UtcNow,
                Notes = "Payment for last month.",
                Status = ClaimStatus.Approved,
                Documents = new List<Document>()
            },
            new Claim
            {
                Id = 2,
                LecturerName = "Jacob Lee",
                HoursWorked = 120,
                HourlyRate = 200,
                Total = 24000,
                DateSubmitted = DateTime.UtcNow,
                Notes = "Payment for this month.",
                Status = ClaimStatus.Verified,
                Documents = new List<Document>()
            },
            new Claim
            {
                Id = 3,
                LecturerName = "Lee Liu",
                HoursWorked = 150,
                HourlyRate = 200,
                Total = 30000,
                DateSubmitted = DateTime.UtcNow,
                Notes = "Payment for 2 months ago.",
                Status = ClaimStatus.Pending,
                Documents = new List<Document>()
            },
        };

        private static int _nextId = 4;

        public static List<Claim> GetAllClaims() => _claims.ToList();

        public static Claim? GetClaimById(int id) => _claims.FirstOrDefault(b => b.Id == id);

        public static List<Claim> GetClaimsByStatus(ClaimStatus status)
            => _claims.Where(b => b.Status == status).ToList();

        public static void AddClaims(Claim claim)
        {
            claim.Id = _nextId;
            _nextId++;
            claim.DateSubmitted = DateTime.Now;
            claim.Status = ClaimStatus.Pending;
            _claims.Add(claim);
        }

        public static int GetPendingCount() => _claims.Count(b => b.Status == ClaimStatus.Pending);
        public static int GetVerifiedCount() => _claims.Count(b => b.Status == ClaimStatus.Verified);
        public static int GetApprovedCount() => _claims.Count(b => b.Status == ClaimStatus.Approved);
        public static int GetDeclinedCount() => _claims.Count(b => b.Status == ClaimStatus.Rejected);
    }
}
