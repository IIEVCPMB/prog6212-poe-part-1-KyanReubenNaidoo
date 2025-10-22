using PROG6212.Models;

namespace PROG6212.Data
{
    public static class ClaimDataStore
    {
        // 🔹 In-memory data store for lecturer claims
        private static List<Claim> _claims = new List<Claim>
        {
            new Claim
            {
                Id = Guid.NewGuid(),
                LecturerName = "John Smith",
                DateSubmitted = DateTime.Now.AddDays(-10),
                Notes = "Extra marking hours for June exams.",
                Status = ClaimStatus.Pending,
                Items = new List<ClaimItem>
                {
                    new ClaimItem
                    {
                        Description = "Marking scripts",
                        Hours = 8,
                        HourlyRate = 350
                    }
                },
                SupportingFiles = new List<string>()
            },
            new Claim
            {
                Id = Guid.NewGuid(),
                LecturerName = "Sarah Williams",
                DateSubmitted = DateTime.Now.AddDays(-7),
                Notes = "Preparation for practical assessments.",
                Status = ClaimStatus.Approved,
                Items = new List<ClaimItem>
                {
                    new ClaimItem
                    {
                        Description = "Practical prep work",
                        Hours = 6,
                        HourlyRate = 300
                    }
                },
                SupportingFiles = new List<string>(),
                Reviews = new List<ClaimReview>
                {
                    new ClaimReview
                    {
                        Id = 1,
                        ClaimId = Guid.NewGuid(),
                        ReviewerName = "Programme Coordinator",
                        ReviewerRole = "Coordinator",
                        ReviewDate = DateTime.Now.AddDays(-6),
                        Decision = ClaimStatus.Approved,
                        Comments = "Approved – meets claim requirements."
                    }
                }
            },
            new Claim
            {
                Id = Guid.NewGuid(),
                LecturerName = "Michael Brown",
                DateSubmitted = DateTime.Now.AddDays(-15),
                Notes = "Extra tutorials conducted.",
                Status = ClaimStatus.Rejected,
                Items = new List<ClaimItem>
                {
                    new ClaimItem
                    {
                        Description = "Tutorials",
                        Hours = 4,
                        HourlyRate = 250
                    }
                },
                SupportingFiles = new List<string>(),
                Reviews = new List<ClaimReview>
                {
                    new ClaimReview
                    {
                        Id = 2,
                        ClaimId = Guid.NewGuid(),
                        ReviewerName = "Academic Manager",
                        ReviewerRole = "Manager",
                        ReviewDate = DateTime.Now.AddDays(-13),
                        Decision = ClaimStatus.Rejected,
                        Comments = "Insufficient evidence provided for tutorial sessions."
                    }
                }
            }
        };

        private static int _nextReviewId = 3;

        // 🧾 GET ALL CLAIMS
        public static List<Claim> GetAllClaims() => _claims.ToList();

        // 🧾 GET CLAIM BY ID
        public static Claim? GetClaimById(Guid id) => _claims.FirstOrDefault(c => c.Id == id);

        // 🔍 FILTER CLAIMS BY STATUS
        public static List<Claim> GetClaimsByStatus(ClaimStatus status)
            => _claims.Where(c => c.Status == status).ToList();

        // ➕ ADD NEW CLAIM
        public static void AddClaim(Claim claim)
        {
            claim.Id = Guid.NewGuid();
            claim.DateSubmitted = DateTime.Now;
            claim.Status = ClaimStatus.Pending;
            _claims.Add(claim);
        }

        // 🔄 UPDATE CLAIM STATUS (Coordinator/Manager)
        public static bool UpdateClaimStatus(Guid id, ClaimStatus newStatus, string reviewer, string reviewerRole, string comments)
        {
            var claim = GetClaimById(id);
            if (claim == null) return false;

            // ✅ Create new review record
            var review = new ClaimReview
            {
                Id = _nextReviewId++,
                ClaimId = id,
                ReviewerName = reviewer,
                ReviewerRole = reviewerRole,
                ReviewDate = DateTime.Now,
                Decision = newStatus,
                Comments = comments
            };

            claim.Reviews.Add(review);
            claim.Status = newStatus;

            return true;
        }

        // 🔢 STATUS COUNTS
        public static int GetPendingCount() => _claims.Count(c => c.Status == ClaimStatus.Pending);
        public static int GetApprovedCount() => _claims.Count(c => c.Status == ClaimStatus.Approved);
        public static int GetRejectedCount() => _claims.Count(c => c.Status == ClaimStatus.Rejected);
    }
}
