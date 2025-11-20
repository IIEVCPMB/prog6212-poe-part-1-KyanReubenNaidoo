using POEProg.Models;

namespace POEProg.Data
{
    public static class ClaimData
    {
        private static List<Claim> _claims = new();
        private static int _nextId = 1;

        public static List<Claim> GetAllClaims() => _claims.ToList();

        public static Claim? GetClaimById(int id) => _claims.FirstOrDefault(c => c.Id == id);

        public static List<Claim> GetClaimsByStatus(ClaimStatus status)
            => _claims.Where(c => c.Status == status).ToList();

        public static void AddClaim(Claim claim)
        {
            claim.Id = _nextId++;
            claim.DateSubmitted = DateTime.UtcNow;
            claim.Status = ClaimStatus.Pending;
            _claims.Add(claim);
        }
    }
}
